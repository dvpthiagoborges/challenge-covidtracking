using BoxTI.Challenge.CovidTracking.Services.Notifications.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace BoxTI.Challenge.CovidTracking.Services.Notifications
{
    public class Notifier : INotifier
    {
        private readonly List<Notification> _notifications;

        public Notifier()
        {
            _notifications = new List<Notification>();
        }

        public List<Notification> GetNotification()
        {
            return _notifications;
        }

        public void Handle(Notification notification)
        {
            _notifications.Add(notification);
        }

        public bool HasNotification()
        {
            return _notifications.Any();
        }
    }
}
