using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SharpTestsEx;
using System;
using System.Collections.Generic;
using System.Linq;
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
    public class TruckControllerTests
    {
        private Mock<ITruckServices> _services;
        private Mock<IMapper> _mapper;
        private Mock<INotifier> _notifier;

        private TruckController _controller;

        public TruckControllerTests()
        {
            _services = new Mock<ITruckServices>();
            _mapper = new Mock<IMapper>();
            _notifier = new Mock<INotifier>();

            _controller = new TruckController(
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
            _services.Setup(x => x.GetAll()).Returns((IEnumerable<Truck>)null);
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
            var listModel = new List<Truck>
            {
                new Truck(),
                new Truck()
            };
            var listViewModel = new List<TruckViewModel>
            {
                new TruckViewModel(),
                new TruckViewModel()
            };

            _services.Setup(x => x.GetAll()).Returns(listModel);
            _mapper.Setup(x => x.Map<IEnumerable<TruckViewModel>>(listModel)).Returns(listViewModel);
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

        [TestMethod()]
        public void PostInvalidModel()
        {
            //ARRANGE
            string errorMessage = "Invalid Parameters.";
            _controller.ModelState.AddModelError(errorMessage, "Error");
            _notifier.Setup(x => x.HaveNotification()).Returns(true);
            _notifier.Setup(x => x.Handle(It.IsAny<Notification>()));
            _notifier.Setup(x => x.GetNotifications()).Returns(new List<Notification> { new Notification(errorMessage) });

            //ACT
            var result = _controller.Post(It.IsAny<TruckViewModel>());
            var notifier = _notifier.Object.HaveNotification();
            var statusCode = (result as ObjectResult).StatusCode;
            var error = _notifier.Object.GetNotifications().Select(x => x.Message.Equals(errorMessage)).Any();

            //ASSERT
            notifier.Should().Be.True();
            result.Should().Not.Be.Null();
            Assert.AreEqual(statusCode, (int)HttpStatusCode.BadRequest);
            error.Should().Be.True();
        }

        [TestMethod()]
        public void PostValidModel()
        {
            //ARRANGE
            string errorMessage = "Invalid Parameters.";
            
            _mapper.Setup(x => x.Map<Truck>(It.IsAny<TruckViewModel>())).Returns(It.IsAny<Truck>());
            _services.Setup(x => x.Add(It.IsAny<Truck>())).Returns(true);
            _notifier.Setup(x => x.HaveNotification()).Returns(false);

            //ACT
            var result = _controller.Post(It.IsAny<TruckViewModel>());
            var notifier = _notifier.Object.HaveNotification();
            var statusCode = (result as ObjectResult).StatusCode;
            var error = _notifier.Object.GetNotifications()?.Select(x => x.Message?.Equals(errorMessage))?.Any();

            //ASSERT
            notifier.Should().Be.False();
            result.Should().Not.Be.Null();
            Assert.AreEqual(statusCode, (int)HttpStatusCode.OK);
            Assert.IsNull(error);
        }

        [TestMethod()]
        public void PutInvalidModel()
        {
            //ARRANGE
            string errorMessage = "Invalid Parameters.";
            _controller.ModelState.AddModelError(errorMessage, "Error");
            _notifier.Setup(x => x.HaveNotification()).Returns(true);
            _notifier.Setup(x => x.Handle(It.IsAny<Notification>()));
            _notifier.Setup(x => x.GetNotifications()).Returns(new List<Notification> { new Notification(errorMessage) });

            //ACT
            var result = _controller.Put(It.IsAny<TruckViewModel>());
            var notifier = _notifier.Object.HaveNotification();
            var statusCode = (result as ObjectResult).StatusCode;
            var error = _notifier.Object.GetNotifications().Select(x => x.Message.Equals(errorMessage)).Any();

            //ASSERT
            notifier.Should().Be.True();
            result.Should().Not.Be.Null();
            Assert.AreEqual(statusCode, (int)HttpStatusCode.BadRequest);
            error.Should().Be.True();
        }

        [TestMethod()]
        public void PutValidModel()
        {
            //ARRANGE
            string errorMessage = "Invalid Parameters.";
            _mapper.Setup(x => x.Map<Truck>(It.IsAny<TruckViewModel>())).Returns(It.IsAny<Truck>());
            _services.Setup(x => x.Update(It.IsAny<Truck>())).Returns(true);
            _notifier.Setup(x => x.HaveNotification()).Returns(false);

            //ACT
            var result = _controller.Put(It.IsAny<TruckViewModel>());
            var notifier = _notifier.Object.HaveNotification();
            var statusCode = (result as ObjectResult).StatusCode;
            var error = _notifier.Object.GetNotifications()?.Select(x => x.Message?.Equals(errorMessage))?.Any();

            //ASSERT
            notifier.Should().Be.False();
            result.Should().Not.Be.Null();
            Assert.AreEqual(statusCode, (int)HttpStatusCode.OK);
            Assert.IsNull(error);
        }

        [TestMethod()]
        public void DeleteInvalidModel()
        {
            //ARRANGE
            string errorMessage = "Invalid Parameters.";
            _notifier.Setup(x => x.HaveNotification()).Returns(true);
            _notifier.Setup(x => x.Handle(It.IsAny<Notification>()));
            _notifier.Setup(x => x.GetNotifications()).Returns(new List<Notification> { new Notification(errorMessage) });

            //ACT
            var result = _controller.Delete(It.IsAny<Guid>());
            var notifier = _notifier.Object.HaveNotification();
            var statusCode = (result as ObjectResult).StatusCode;
            var error = _notifier.Object.GetNotifications().Select(x => x.Message.Equals(errorMessage)).Any();

            //ASSERT
            notifier.Should().Be.True();
            result.Should().Not.Be.Null();
            Assert.AreEqual(statusCode, (int)HttpStatusCode.BadRequest);
            error.Should().Be.True();
        }

        [TestMethod()]
        public void DeleteValidModel()
        {
            //ARRANGE
            string errorMessage = "Invalid Parameters.";
            _mapper.Setup(x => x.Map<Truck>(It.IsAny<TruckViewModel>())).Returns(It.IsAny<Truck>());
            _services.Setup(x => x.Delete(It.IsAny<Guid>())).Returns(true);
            _notifier.Setup(x => x.HaveNotification()).Returns(false);
            
            //ACT
            var result = _controller.Delete(It.IsAny<Guid>());
            var notifier = _notifier.Object.HaveNotification();
            var statusCode = (result as ObjectResult).StatusCode;
            var error = _notifier.Object.GetNotifications()?.Select(x => x.Message?.Equals(errorMessage))?.Any();

            //ASSERT
            notifier.Should().Be.False();
            result.Should().Not.Be.Null();
            Assert.AreEqual(statusCode, (int)HttpStatusCode.OK);
            Assert.IsNull(error);
        }
    }
}