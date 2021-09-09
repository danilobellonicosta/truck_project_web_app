using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SharpTestsEx;
using System;
using System.Collections.Generic;
using TruckProject.API.Interfaces.Notifications;
using TruckProject.API.Interfaces.Repository;
using TruckProject.API.Interfaces.Services;
using TruckProject.API.Models;
using TruckProject.API.Services;

namespace TruckProject.API.Tests.Services
{
    [TestClass()]
    public class TruckServicesTests
    {
        private Mock<ITruckRepository> _repository;
        private Mock<IModelTruckRepository> _modelTruckRepository;
        private Mock<INotifier> _notifier;

        private ITruckServices services;

        [TestInitialize]
        public void Initialize()
        {
            _repository = new Mock<ITruckRepository>();
            _modelTruckRepository = new Mock<IModelTruckRepository>();
            _notifier = new Mock<INotifier>();

            services = new TruckServices(
                _repository.Object,
                _modelTruckRepository.Object,
                _notifier.Object
                );
        }

        [TestCleanup]
        public void Cleanup()
        {
            _repository = null;
            _modelTruckRepository = null;
            _notifier = null;
        }

        [TestMethod]
        public void GetAllWhenReturnIsNull()
        {
            //ARRANGE
            _repository.Setup(x => x.GetAll()).Returns((IEnumerable<Truck>)null);
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
            _repository.Setup(x => x.GetAll()).Returns(new List<Truck>());
            _notifier.Setup(x => x.HaveNotification()).Returns(false);

            //ACT
            var result = services.GetAll();
            var notifier = _notifier.Object.HaveNotification();

            //ASSERT
            notifier.Should().Be.False();
            result.Should().Not.Be.Null();
        }

        [TestMethod]
        public void AddValidEntity()
        {
            //ARRANGE
            var truck = new Truck 
            { 
                FabricationYear = 2021,
                ModelYear = 2021
               
            };
            _repository.Setup(x => x.Add(truck)).Returns(true);
            _modelTruckRepository.Setup(x => x.GetById(It.IsAny<Guid>())).Returns(new ModelTruck { Model = "FM"});
            _notifier.Setup(x => x.HaveNotification()).Returns(false);

            //ACT
            var result = services.Add(truck);
            var notifier = _notifier.Object.HaveNotification();

            //ASSERT
            notifier.Should().Be.False();
            result.Should().Be.True();
        }

        [TestMethod]
        public void AddValidEntityError()
        {
            //ARRANGE
            var truck = new Truck
            {
                FabricationYear = 2021,
                ModelYear = 2021
            };

            _repository.Setup(x => x.Add(truck)).Returns(false);
            _modelTruckRepository.Setup(x => x.GetById(It.IsAny<Guid>())).Returns(new ModelTruck { Model = "FM" });
            _notifier.Setup(x => x.HaveNotification()).Returns(true);

            //ACT
            var result = services.Add(truck);
            var notifier = _notifier.Object.HaveNotification();

            //ASSERT
            notifier.Should().Be.True();
            result.Should().Be.False();
        }

        [TestMethod]
        public void AddInValidFabricationYearEntity()
        {
            //ARRANGE
            var truck = new Truck
            {
                FabricationYear = 2020,
                ModelYear = 2021
            };

            _repository.Setup(x => x.Add(truck)).Returns(true);
            _modelTruckRepository.Setup(x => x.GetById(It.IsAny<Guid>())).Returns(new ModelTruck { Model = "FM" });
            _notifier.Setup(x => x.HaveNotification()).Returns(true);

            //ACT
            var result = services.Add(truck);
            var notifier = _notifier.Object.HaveNotification();

            //ASSERT
            notifier.Should().Be.True();
            result.Should().Be.False();
        }

        [TestMethod]
        public void AddInValidModelYearEntity()
        {
            //ARRANGE
            var truck = new Truck
            {
                FabricationYear = 2021,
                ModelYear = 2020
            };

            _repository.Setup(x => x.Add(truck)).Returns(true);
            _modelTruckRepository.Setup(x => x.GetById(It.IsAny<Guid>())).Returns(new ModelTruck { Model = "FM" });
            _notifier.Setup(x => x.HaveNotification()).Returns(true);

            //ACT
            var result = services.Add(truck);
            var notifier = _notifier.Object.HaveNotification();

            //ASSERT
            notifier.Should().Be.True();
            result.Should().Be.False();
        }

        [TestMethod]
        public void UpdateValidEntity()
        {
            //ARRANGE
            var truck = new Truck
            {
                FabricationYear = 2021,
                ModelYear = 2021

            };
            _repository.Setup(x => x.Update(truck)).Returns(true);
            _modelTruckRepository.Setup(x => x.GetById(It.IsAny<Guid>())).Returns(new ModelTruck { Model = "FM" });
            _notifier.Setup(x => x.HaveNotification()).Returns(false);

            //ACT
            var result = services.Update(truck);
            var notifier = _notifier.Object.HaveNotification();

            //ASSERT
            notifier.Should().Be.False();
            result.Should().Be.True();
        }

        [TestMethod]
        public void UpdateValidEntityError()
        {
            //ARRANGE
            var truck = new Truck
            {
                FabricationYear = 2021,
                ModelYear = 2021
            };

            _repository.Setup(x => x.Update(truck)).Returns(false);
            _modelTruckRepository.Setup(x => x.GetById(It.IsAny<Guid>())).Returns(new ModelTruck { Model = "FM" });
            _notifier.Setup(x => x.HaveNotification()).Returns(true);

            //ACT
            var result = services.Update(truck);
            var notifier = _notifier.Object.HaveNotification();

            //ASSERT
            notifier.Should().Be.True();
            result.Should().Be.False();
        }

        [TestMethod]
        public void UpdateInValidFabricationYearEntity()
        {
            //ARRANGE
            var truck = new Truck
            {
                FabricationYear = 2020,
                ModelYear = 2021
            };

            _repository.Setup(x => x.Update(truck)).Returns(true);
            _modelTruckRepository.Setup(x => x.GetById(It.IsAny<Guid>())).Returns(new ModelTruck { Model = "FM" });
            _notifier.Setup(x => x.HaveNotification()).Returns(true);

            //ACT
            var result = services.Update(truck);
            var notifier = _notifier.Object.HaveNotification();

            //ASSERT
            notifier.Should().Be.True();
            result.Should().Be.False();
        }

        [TestMethod]
        public void UpdateInValidModelYearEntity()
        {
            //ARRANGE
            var truck = new Truck
            {
                FabricationYear = 2021,
                ModelYear = 2020
            };

            _repository.Setup(x => x.Update(truck)).Returns(true);
            _modelTruckRepository.Setup(x => x.GetById(It.IsAny<Guid>())).Returns(new ModelTruck { Model = "FM" });
            _notifier.Setup(x => x.HaveNotification()).Returns(true);

            //ACT
            var result = services.Update(truck);
            var notifier = _notifier.Object.HaveNotification();

            //ASSERT
            notifier.Should().Be.True();
            result.Should().Be.False();
        }


        [TestMethod]
        public void DeleteValidEntity()
        {
            //ARRANGE
            var id = Guid.NewGuid();
            var truck = new Truck();
            _repository.Setup(x => x.GetById(id)).Returns(truck);
            _repository.Setup(x => x.Delete(truck)).Returns(true);
            _notifier.Setup(x => x.HaveNotification()).Returns(false);

            //ACT
            var result = services.Delete(id);
            var notifier = _notifier.Object.HaveNotification();

            //ASSERT
            notifier.Should().Be.False();
            result.Should().Be.True();
        }

        [TestMethod]
        public void DeleteNullEntity()
        {
            //ARRANGE
            var id = Guid.NewGuid();
            var truck = new Truck();
            _repository.Setup(x => x.GetById(id)).Returns((Truck)null);
            _notifier.Setup(x => x.HaveNotification()).Returns(true);

            //ACT
            var result = services.Delete(id);
            var notifier = _notifier.Object.HaveNotification();

            //ASSERT
            notifier.Should().Be.True();
            result.Should().Be.False();
        }
    }
}