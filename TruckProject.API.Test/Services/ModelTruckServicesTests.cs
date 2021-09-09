using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SharpTestsEx;
using System;
using System.Collections.Generic;
using System.Linq;
using TruckProject.API.Interfaces.Notifications;
using TruckProject.API.Interfaces.Repository;
using TruckProject.API.Interfaces.Services;
using TruckProject.API.Models;
using TruckProject.API.Services;

namespace TruckProject.API.Tests.Services
{
    [TestClass()]
    public class ModelTruckServicesTests
    {
        private Mock<IModelTruckRepository> _repository;
        private Mock<INotifier> _notifier;

        private IModelTruckServices services;

        [TestInitialize]
        public void Initialize()
        {
            _repository = new Mock<IModelTruckRepository>();
            _notifier = new Mock<INotifier>();

            services = new ModelTruckServices(
                _repository.Object,
                _notifier.Object
                );
        }

        [TestCleanup]
        public void Cleanup()
        {
            _repository = null;
            _notifier = null;
        }

        [TestMethod]
        public void GetAllWhenReturnIsNull()
        {
            //ARRANGE
            _repository.Setup(x => x.GetAll()).Returns((IEnumerable<ModelTruck>)null);
            _notifier.Setup(x => x.HaveNotification()).Returns(true);

            //ACT
            var result = services.GetAll();
            var notifier = _notifier.Object.HaveNotification();

            //ASSERT
            notifier.Should().Be.True();
            result.Should().Be.Null();
        }

        [TestMethod]
        public void GetAllWhenReturnIsNotNull()
        {
            //ARRANGE
            _repository.Setup(x => x.GetAll()).Returns(ModelTrucksMock());
            _notifier.Setup(x => x.HaveNotification()).Returns(false);

            //ACT
            var result = services.GetAll();
            var notifier = _notifier.Object.HaveNotification();

            //ASSERT
            notifier.Should().Be.False();
            result.Should().Not.Be.Null();
            Assert.AreEqual(ModelTrucksMock().Count, result.Count());
        }

        public List<ModelTruck> ModelTrucksMock()
        {
            List<ModelTruck> ModelTrucks = new();

            ModelTrucks.Add(new ModelTruck
            {
                Id = Guid.NewGuid(),
                Model = "FA"
            });
            ModelTrucks.Add(new ModelTruck
            {
                Id = Guid.NewGuid(),
                Model = "FB"
            });
            ModelTrucks.Add(new ModelTruck
            {
                Id = Guid.NewGuid(),
                Model = "FC"
            });
            ModelTrucks.Add(new ModelTruck
            {
                Id = Guid.NewGuid(),
                Model = "FD"
            });

            return ModelTrucks;
        }
    }
}