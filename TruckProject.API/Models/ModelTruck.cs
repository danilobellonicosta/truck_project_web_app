using System.Collections.Generic;

namespace TruckProject.API.Models
{
    public class ModelTruck : Entity
    {
        public string Model { get; set; }

        public IEnumerable<Truck> Trucks { get; set; }
    }
}
