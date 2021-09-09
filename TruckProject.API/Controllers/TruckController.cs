using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using TruckProject.API.Interfaces.Notifications;
using TruckProject.API.Interfaces.Services;
using TruckProject.API.Models;
using TruckProject.API.ViewModels;

namespace TruckProject.API.Controllers
{
    [Route("api/[controller]")]
    public class TruckController : MainController
    {
        private readonly ITruckServices _services;
        private readonly IMapper _mapper;

        public TruckController(INotifier notifier,
                               ITruckServices services, 
                               IMapper mapper) : base(notifier)
        {
            _services = services;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return CustomResponse(_mapper.Map<IEnumerable<TruckViewModel>>(_services.GetAll()));
        }

        [HttpGet("{id}")]
        public IActionResult GetById(Guid id)
        {
            return CustomResponse(_mapper.Map<TruckViewModel>(_services.GetById(id)));
        }

        [HttpPost]
        public IActionResult Post([FromBody] TruckViewModel model)
        {
            if (!ModelState.IsValid)
                return CustomResponse("Invalid Parameters.");

            var entity = _mapper.Map<Truck>(model);

            return CustomResponse(_services.Add(entity));
        }

        [HttpPut]
        public IActionResult Put([FromBody] TruckViewModel model)
        {
            if (!ModelState.IsValid)
                return CustomResponse("Invalid Parameters.");

            var entity = _mapper.Map<Truck>(model);

            return CustomResponse(_services.Update(entity));
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            return CustomResponse(_services.Delete(id));
        }
    }
}
