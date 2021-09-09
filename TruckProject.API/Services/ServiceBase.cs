using TruckProject.API.Interfaces.Notifications;
using TruckProject.API.Notifications;

namespace TruckProject.API.Services
{
    public abstract class ServiceBase
    {
        private readonly INotifier _notifier;

        public ServiceBase(INotifier notifier)
        {
            _notifier = notifier;
        }

        protected void Notify(string mensagem)
        {
            _notifier.Handle(new Notification(mensagem));
        }
    }
}
