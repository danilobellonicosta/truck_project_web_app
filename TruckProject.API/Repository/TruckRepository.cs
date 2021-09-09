using TruckProject.API.Context;
using TruckProject.API.Interfaces.Repository;
using TruckProject.API.Models;

namespace TruckProject.API.Repository
{
    public class TruckRepository : Repository<Truck>, ITruckRepository
    {
        public TruckRepository(MyContext db) : base(db)
        {
        }
    }
}
