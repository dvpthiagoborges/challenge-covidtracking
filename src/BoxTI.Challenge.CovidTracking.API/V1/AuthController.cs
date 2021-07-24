using BoxTI.Challenge.CovidTracking.API.Configurations;
using BoxTI.Challenge.CovidTracking.API.Controllers.Base;
using BoxTI.Challenge.CovidTracking.CrossCutting.Identity.Base;
using BoxTI.Challenge.CovidTracking.CrossCutting.Identity.Models;
using BoxTI.Challenge.CovidTracking.CrossCutting.Identity.Models.AccountViewModels;
using BoxTI.Challenge.CovidTracking.Services.Notifications.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BoxTI.Challenge.CovidTracking.API.V1
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/auth")]
    [ApiController]
    public class AuthController : BaseController
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        protected readonly AppSettings _appSettings;

        public AuthController(INotifier notifier,
                              IUser appUser,
                              UserManager<ApplicationUser> userManager,
                              SignInManager<ApplicationUser> signInManager,
                              IOptions<AppSettings> appSettings) : base(notifier, appUser)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _appSettings = appSettings.Value;
        }

        /// <summary>
        /// Application login.
        /// </summary>
        /// <param name="viewmodel">Login View Model.</param>
        /// <returns>Operation result.</returns>
        /// <remarks>
        /// Request example
        /// 
        ///     POST /api/v1/auth/login
        ///     {
        ///         "email": "dev.thiagoborges@gmail.com",
        ///         "password": "123546"
        ///     }
        ///     
        /// </remarks>
        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> SignIn([FromBody] LoginViewModel viewmodel)
        {
            if (!ModelState.IsValid)
                return CustomResponse(ModelState);

            return await Login(viewmodel);
        }

        /// <summary>
        /// Register a new user.
        /// </summary>
        /// <param name="viewmodel">User View Model.</param>
        /// <returns>Operation result.</returns>
        /// <remarks>
        /// Request example
        /// 
        ///     POST /api/v1/auth/register-user
        ///     {
        ///         "name": "Thiago",
        ///         "email": "dev.thiagoborges@gmail.com",
        ///         "password": "123546",
        ///         "confirmPassword": "123456"
        ///     }
        ///     
        /// </remarks>
        [HttpPost("register-user")]
        [AllowAnonymous]
        public async Task<IActionResult> RegisterNewUser([FromBody] RegisterViewModel viewmodel)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            var userRegistered = await RegisterUser(viewmodel);
            if (!userRegistered)
            {
                return CustomResponse(string.Format("It was not possible to create the user {0}.", viewmodel.Email));
            }

            return CustomResponse(new { message = "User registered successfully" });
        }

        private async Task<IActionResult> Login(LoginViewModel viewmodel)
        {
            var user = await _userManager.FindByEmailAsync(viewmodel.Email);
            if (user != null)
            {
                if (user.EmailConfirmed)
                {
                    var result = await _signInManager.PasswordSignInAsync(viewmodel.Email, viewmodel.Password, false, true);
                    if (result.Succeeded)
                    {
                        return CustomResponse(await GenerateJwt(viewmodel.Email));
                    }

                    if (result.IsLockedOut)
                    {
                        NotifyError("User temporarily blocked by invalid attempts.");
                        return CustomResponse(viewmodel);
                    }
                }
                else
                {
                    NotifyError("You need to confirm your email before logging in. If you can't find our link, please check your SPAM box and other email filters.");
                    return CustomResponse(viewmodel);
                }
            }

            NotifyError("Incorrect username or password.");
            return CustomResponse(viewmodel);
        }

        private async Task<bool> RegisterUser(RegisterViewModel viewmodel)
        {
            string userName = viewmodel.Email;
            var user = new ApplicationUser
            {
                UserName = userName,
                NormalizedUserName = userName.ToUpper(),
                EmailConfirmed = true,
                Email = viewmodel.Email,
                NormalizedEmail = viewmodel.Email.ToUpper()
            };

            var result = await _userManager.CreateAsync(user, viewmodel.Password);

            if (!result.Succeeded)
            {
                InsertErrorsIdentity(result);
                return false;
            }

            return true;
        }

        private async Task<LoginResponseViewModel> GenerateJwt(string email)
        {
            var user = await _userManager.FindByNameAsync(email);
            var claims = await _userManager.GetClaimsAsync(user);

            claims.Add(new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()));
            claims.Add(new Claim(JwtRegisteredClaimNames.Email, user.Email));
            claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
            claims.Add(new Claim(JwtRegisteredClaimNames.Nbf, ToUnixEpochDate(DateTime.UtcNow).ToString()));
            claims.Add(new Claim(JwtRegisteredClaimNames.Iat, ToUnixEpochDate(DateTime.UtcNow).ToString(), ClaimValueTypes.Integer64));

            var identityClaims = new ClaimsIdentity();
            identityClaims.AddClaims(claims);

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var token = tokenHandler.CreateToken(new SecurityTokenDescriptor
            {
                Issuer = _appSettings.Issuer,
                Audience = _appSettings.ValidAt,
                Subject = identityClaims,
                Expires = DateTime.UtcNow.AddHours(_appSettings.Expiration),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            });

            var encodedToken = tokenHandler.WriteToken(token);

            var response = new LoginResponseViewModel
            {
                AccessToken = encodedToken
            };

            return response;
        }

        private static long ToUnixEpochDate(DateTime date)
            => (long)Math.Round((date.ToUniversalTime() - new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero)).TotalSeconds);
    }
}
