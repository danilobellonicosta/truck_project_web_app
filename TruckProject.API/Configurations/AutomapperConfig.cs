using AutoMapper;
using TruckProject.API.Models;
using TruckProject.API.ViewModels;

namespace TruckProject.API.Configurations
{
    public class AutomapperConfig : Profile
    {
        public AutomapperConfig()
        {
            CreateMap<Truck, TruckViewModel>().ReverseMap();

            CreateMap<ModelTruck, ModelTruckViewModel>().ReverseMap();
        }
    }
}
