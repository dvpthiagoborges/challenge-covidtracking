using System.ComponentModel.DataAnnotations;

namespace BoxTI.Challenge.CovidTracking.CrossCutting.Identity.Models.AccountViewModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Field {0} is required")]
        [StringLength(255, ErrorMessage = "Field {0} must have between {2} and {1} characters", MinimumLength = 3)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Field {0} is required")]
        [EmailAddress(ErrorMessage = "Field {0} is in invalid format")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Field {0} is required")]
        [StringLength(100, ErrorMessage = "Field {0} must have between {2} and {1} characters", MinimumLength = 6)]
        public string Password { get; set; }

        [Compare("Password", ErrorMessage = "Passwords do not match.")]
        public string ConfirmPassword { get; set; }
    }
}
