using System.Collections.Generic;

namespace BoxTI.Challenge.CovidTracking.Services.Notifications.Interfaces
{
    public interface INotifier
    {
        bool HasNotification();
        List<Notification> GetNotification();
        void Handle(Notification notification);
    }
}
