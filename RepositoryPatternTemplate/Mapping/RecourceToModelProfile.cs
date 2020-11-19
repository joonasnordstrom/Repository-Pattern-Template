using AutoMapper;
using PikiouAPI.Domain.Models;
using PikiouAPI.Resources;

namespace PikiouAPI.Mapping
{
    /// <summary>
    /// Client to database
    /// </summary>
    public class ResourceToModelProfile : Profile
    {
        public ResourceToModelProfile()
        {
            CreateMap<SaveCourierResource, Courier>();
        }
    }
}
