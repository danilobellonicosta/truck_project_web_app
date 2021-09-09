using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using TruckProject.API.Interfaces.Notifications;

namespace TruckProject.API.Controllers
{
    [ApiController]
    public class MainController : Controller
    {
        private readonly INotifier _notifier;

        public MainController(INotifier notifier)
        {
            _notifier = notifier;
        }

        protected ICollection<string> Errors = new List<string>();

        protected IActionResult CustomResponse(object result = null)
        {
            if (ValidOperation())
                return Ok(result);

            return BadRequest(new
            {
                success = false,
                errors = _notifier.GetNotifications().Select(x => x.Message)
            });
        }

        protected bool ValidOperation() => !_notifier.HaveNotification();
    }
}
