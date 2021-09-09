using System.Collections.Generic;
using System.Linq;
using TruckProject.API.Interfaces.Notifications;
using TruckProject.API.Interfaces.Repository;
using TruckProject.API.Interfaces.Services;
using TruckProject.API.Models;

namespace TruckProject.API.Services
{
    public class ModelTruckServices : ServiceBase, IModelTruckServices
    {
        private readonly IModelTruckRepository _repository;

        public ModelTruckServices(IModelTruckRepository repository,
                                  INotifier notifier): base(notifier)
        {
            _repository = repository;
        }

        public IEnumerable<ModelTruck> GetAll()
        {
            var result = _repository.GetAll();

            if (result is null || !result.Any())
            {
                Notify("Query did not return records");
            }

            return result;
        }
    }
}
