using AutoMapper;
using AvaloniaChat.Backend.Models;
using AvaloniaChat.Backend.Models.UserModels;
using ViewModels;

namespace AvaloniaChat.Backend.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, MainUserViewModel>()
                .ForMember(x => x.Username, opt => opt.MapFrom(x => x.Username))
                .ForMember(x => x.FirstName, opt => opt.MapFrom(x => x.FirstName))
                .ForMember(x => x.LastName, opt => opt.MapFrom(x => x.LastName))
                .ForMember(x => x.Logo, opt => opt.MapFrom(x => x.Logo));
        }
    }
}
 