using System;

namespace TruckProject.API.Models
{
    public class Truck : Entity
    {
        public Guid ModelTruckId { get; set; }
        public int FabricationYear { get; set; }
        public string Chassi { get; set; }
        public int ModelYear { get; set; }

        public ModelTruck ModelTruck { get; set; }
    }
}
