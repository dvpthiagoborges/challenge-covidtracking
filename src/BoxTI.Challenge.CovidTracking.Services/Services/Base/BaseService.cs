using BoxTI.Challenge.CovidTracking.Services.Notifications;
using BoxTI.Challenge.CovidTracking.Services.Notifications.Interfaces;
using FluentValidation.Results;

namespace BoxTI.Challenge.CovidTracking.Services.Services.Base
{
    public abstract class BaseService
    {
        protected readonly INotifier _notifier;

        protected BaseService(INotifier notifier)
        {
            _notifier = notifier;
        }

        protected void Notify(ValidationResult validationResult)
        {
            foreach (var error in validationResult.Errors)
            {
                Notify(error.ErrorMessage);
            }
        }

        protected void Notify(string message)
        {
            _notifier.Handle(new Notification(message));
        }

        protected void Notify(string message, params object[] parameters)
        {
            _notifier.Handle(new Notification(string.Format(message, parameters)));
        }        
    }
}
