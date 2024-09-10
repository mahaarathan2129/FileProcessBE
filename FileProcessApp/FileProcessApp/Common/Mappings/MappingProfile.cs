using AutoMapper;
using FileProcessingApp.Models.Dto.Request;
using FileProcessingApp.Models.Dto.Response;
using FileProcessingApp.Models.Entities;

namespace FileProcessingApp.Common.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<UserRequest, Users>();
            CreateMap<Users, UserResponse>();
        }
    }
}
