using System;
using System.ComponentModel.DataAnnotations;

namespace TruckProject.API.ViewModels
{
    public class TruckViewModel
    {
        public Guid? Id { get; set; }
        public Guid ModelTruckId { get; set; }
        [Required(ErrorMessage = "Fabrication Year cannot be null or empty.")]
        public int FabricationYear { get; set; }
        public string Chassi { get; set; }
        [Required(ErrorMessage = "Model Year cannot be null or empty.")]
        public int ModelYear { get; set; }
        public ModelTruckViewModel ModelTruck { get; set; }

}
}
