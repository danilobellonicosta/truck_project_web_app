using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TruckProject.API.ViewModels
{
    public class ModelTruckViewModel
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "The field 'Model' is mandatory.")]
        public string Model { get; set; }
        
        public IEnumerable<TruckViewModel> Trucks { get; set; }
    }
}
