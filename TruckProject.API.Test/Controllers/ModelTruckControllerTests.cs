using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SharpTestsEx;
using System.Collections.Generic;
using System.Net;
using TruckProject.API.Controllers;
using TruckProject.API.Interfaces.Notifications;
using TruckProject.API.Interfaces.Services;
using TruckProject.API.Models;
using TruckProject.API.Notifications;
using TruckProject.API.ViewModels;

namespace TruckProject.API.Tests.Controllers
{
    [TestClass()]
    public class ModelTruckControllerTests
    {
        private Mock<IModelTruckServices> _services;
        private Mock<IMapper> _mapper;
        private Mock<INotifier> _notifier;

        private ModelTruckController _controller;

        public ModelTruckControllerTests()
        {
            _services = new Mock<IModelTruckServices>();
            _mapper = new Mock<IMapper>();
            _notifier = new Mock<INotifier>();

            _controller = new ModelTruckController(
                _notifier.Object,
                _services.Object,
                _mapper.Object);
        }

        [TestCleanup]
        public void Cleanup()
        {
            _services = null;
            _mapper = null;
            _notifier = null;
        }

        [TestMethod()]
        public void GetAllWhenReturnIsNull()
        {
            //ARRANGE
            _services.Setup(x => x.GetAll()).Returns((IEnumerable<ModelTruck>)null);
            _notifier.Setup(x => x.HaveNotification()).Returns(true);
            _notifier.Setup(x => x.Handle(It.IsAny<Notification>()));
            _notifier.Setup(x => x.GetNotifications()).Returns(new List<Notification>());

            //ACT
            var result = _controller.Get();
            var notifier = _notifier.Object.HaveNotification();
            var statusCode = (result as ObjectResult).StatusCode;

            //ASSERT
            notifier.Should().Be.True();
            result.Should().Not.Be.Null();
            Assert.AreEqual(statusCode, (int)HttpStatusCode.BadRequest);
        }

        [TestMethod()]
        public void GetAll()
        {
            //ARRANGE
            var listModel = new List<ModelTruck>
            {
                new ModelTruck(),
                new ModelTruck()
            };
            var listViewModel = new List<ModelTruckViewModel>
            {
                new ModelTruckViewModel(),
                new ModelTruckViewModel()
            };

            _services.Setup(x => x.GetAll()).Returns(listModel);
            _mapper.Setup(x => x.Map<IEnumerable<ModelTruckViewModel>>(listModel)).Returns(listViewModel);
            _notifier.Setup(x => x.HaveNotification()).Returns(false);

            //ACT
            var result = _controller.Get();
            var notifier = _notifier.Object.HaveNotification();
            var statusCode = (result as ObjectResult).StatusCode;

            //ASSERT
            notifier.Should().Be.False();
            result.Should().Not.Be.Null();
            Assert.AreEqual(statusCode, (int)HttpStatusCode.OK);
        }
    }
}