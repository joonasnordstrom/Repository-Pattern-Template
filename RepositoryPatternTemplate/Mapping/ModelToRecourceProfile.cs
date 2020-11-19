using AutoMapper;
using PikiouAPI.Domain.Models;
using PikiouAPI.Resources;

namespace PikiouAPI.Mapping
{
    /// <summary>
    /// Database to client
    /// </summary>
    public class ModelToRecourceProfile : Profile
    {
        public ModelToRecourceProfile()
        {
            CreateMap<Courier, CourierResource>();
            CreateMap<Order, OrderResource>();
        }
    }
}
