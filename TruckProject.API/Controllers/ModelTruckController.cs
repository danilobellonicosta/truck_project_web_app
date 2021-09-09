using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using TruckProject.API.Interfaces.Notifications;
using TruckProject.API.Interfaces.Services;
using TruckProject.API.ViewModels;

namespace TruckProject.API.Controllers
{
    [Route("api/[controller]")]
    public class ModelTruckController : MainController
    {
        private readonly IModelTruckServices _services;
        private readonly IMapper _mapper;

        public ModelTruckController(INotifier notifier,
                                    IModelTruckServices services, 
                                    IMapper mapper) : base(notifier)
        {
            _services = services;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var test = _services.GetAll();
            var count = _mapper.Map<IEnumerable<ModelTruckViewModel>>(test);
            return CustomResponse(_mapper.Map<IEnumerable<ModelTruckViewModel>>(_services.GetAll()));
        }
    }
}
