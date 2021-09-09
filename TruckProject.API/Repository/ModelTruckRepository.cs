using TruckProject.API.Context;
using TruckProject.API.Interfaces.Repository;
using TruckProject.API.Models;

namespace TruckProject.API.Repository
{
    public class ModelTruckRepository : Repository<ModelTruck>, IModelTruckRepository
    {
        public ModelTruckRepository(MyContext db) : base(db)
        {
        }
    }
}
