using System;
using System.Collections.Generic;
using System.Linq;
using TruckProject.API.Interfaces.Notifications;
using TruckProject.API.Interfaces.Repository;
using TruckProject.API.Interfaces.Services;
using TruckProject.API.Models;

namespace TruckProject.API.Services
{
    public class TruckServices : ServiceBase, ITruckServices
    {
        private readonly ITruckRepository _repository;
        private readonly IModelTruckRepository _modelTruckRepository;

        public TruckServices(ITruckRepository repository,
                             IModelTruckRepository modelTruckRepository,
                             INotifier notifier) : base(notifier)
        {
            _repository = repository;
            _modelTruckRepository = modelTruckRepository;
        }

        public IEnumerable<Truck> GetAll()
        {
            var result = _repository.GetAll();

            if(result is null || !result.Any())
            {
                Notify("Query did not return records");
            }

            return result;
        }

        public Truck GetById(Guid id)
        {
            var result = _repository.GetById(id);

            if (result is null)
            {
                Notify("Query did not return records");
            }

            return result;
        }

        public bool Add(Truck entity)
        {
            var isValid = ExecuteValidations(entity);

            if (!isValid)
                return isValid;

            var result = _repository.Add(entity);

            if (!result)
            {
                Notify("Error when registering Truck.");
                return false;
            }

            return true;
        }

        public bool Update(Truck entity)
        {
            var isValid = ExecuteValidations(entity);

            if (!isValid)
                return isValid;

            var result = _repository.Update(entity);

            if (!result)
            {
                Notify("Error editing record.");
                return false;
            }

            return true;
        }

        public bool Delete(Guid id)
        {
            var entity = _repository.GetById(id);
            if (entity is null)
            {
                Notify("Record not found to delete.");
                return false;
            }

            var result = _repository.Delete(entity);

            if (!result)
            {
                Notify("Error deleting record.");
                return false;
            }

            return true;
        }

        private bool ExecuteValidations(Truck entity)
        {
            var isValid = ValidateModel(entity.ModelTruckId);

            if (!isValid)
            {
                Notify("Model other than FH and FM is not allowed.");
                return false;
            }

            if(entity.FabricationYear != DateTime.Now.Year)
            {
                Notify("Year of manufacture cannot be different from the current one.");
                return false;
            }

            if(entity.ModelYear < DateTime.Now.Year)
            {
                Notify("Model Year must be current or subsequent.");
                return false;
            }

            return true;
        }

        private bool ValidateModel(Guid modelTruckId)
        {
            string[] validIntervals = new string[] { "FH", "FM" };

            var result = _modelTruckRepository.GetById(modelTruckId);

            if (result != null && validIntervals.Contains(result.Model))
                return true;

            return false;
        }
    }
}
