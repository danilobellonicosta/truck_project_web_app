using System.Collections.Generic;
using TruckProject.API.Models;

namespace TruckProject.API.Interfaces.Services
{
    public interface IModelTruckServices
    {
        IEnumerable<ModelTruck> GetAll();
    }
}
