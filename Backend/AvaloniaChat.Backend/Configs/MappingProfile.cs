using AutoMapper;
using AvaloniaChat.Backend.Models;
using AvaloniaChat.Backend.Models.UserModels;

namespace AvaloniaChat.Backend.Configs
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, User>()
                .ForMember("Logo",opt => opt.MapFrom(c=>c.Logo))
                .ForMember("FirstName", opt => opt.MapFrom(c => c.FirstName))
                .ForMember("LastName", opt => opt.MapFrom(c => c.LastName))
                .ForMember("Email", opt => opt.MapFrom(c => c.Email))
                .ReverseMap();
            


        }
    }
}
