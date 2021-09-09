using System;
using System.Collections.Generic;
using TruckProject.API.Models;

namespace TruckProject.API.Interfaces.Services
{
    public interface ITruckServices
    {
        IEnumerable<Truck> GetAll();
        Truck GetById(Guid id);
        bool Add(Truck entity);
        bool Update(Truck entity);
        bool Delete(Guid id);
    }
}
