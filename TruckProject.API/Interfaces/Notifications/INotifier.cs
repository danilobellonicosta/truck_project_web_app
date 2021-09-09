using System.Collections.Generic;
using TruckProject.API.Notifications;

namespace TruckProject.API.Interfaces.Notifications
{
    public interface INotifier
    {
        bool HaveNotification();
        List<Notification> GetNotifications();
        void Handle(Notification notificacao);
    }
}
