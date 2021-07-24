using BoxTI.Challenge.CovidTracking.CrossCutting.Identity.Base;
using BoxTI.Challenge.CovidTracking.Services.Notifications;
using BoxTI.Challenge.CovidTracking.Services.Notifications.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Linq;

namespace BoxTI.Challenge.CovidTracking.API.Controllers.Base
{
    [Produces("application/json")]
    [ApiController]
    public abstract class BaseController : Controller
    {
        protected readonly INotifier _notifier;
        protected readonly IUser AppUser;

        public BaseController(INotifier notifier, IUser appUser)
        {
            _notifier = notifier;
            AppUser = appUser;
        }

        protected bool ValidOperation()
        {
            return !_notifier.HasNotification();
        }

        protected ActionResult CustomResponse(object result = null)
        {
            if (ValidOperation())
            {
                return Ok(new
                {
                    success = true,
                    data = result
                });
            }

            return BadRequest(new
            {
                success = false,
                errors = _notifier.GetNotification().Select(n => n.Message)
            });
        }

        protected ActionResult CustomResponse(ModelStateDictionary modelState)
        {
            if (!modelState.IsValid) NotifyErrorModelInvalid(modelState);
            return CustomResponse();
        }

        protected void NotifyErrorModelInvalid(ModelStateDictionary modelState)
        {
            var listError = modelState.Values.SelectMany(e => e.Errors);
            foreach (var error in listError)
            {
                var errorMsg = error.Exception == null ? error.ErrorMessage : error.Exception.Message;
                NotifyError(errorMsg);
            }
        }

        protected void NotifyError(string message)
        {
            _notifier.Handle(new Notification(message));
        }

        protected void InsertErrorsIdentity(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                NotifyError(error.Description);
            }
        }
    }
}
