using AutoMapper;
using AvaloniaChat.Application.DTO.Message;
using AvaloniaChat.Application.DTO.User;
using AvaloniaChat.Domain.Models;

namespace AvaloniaChat.Application.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserDto>()
                .ForMember(x => x.Username, opt => opt.MapFrom(x => x.Username))
                .ForMember(x => x.FirstName, opt => opt.MapFrom(x => x.FirstName))
                .ForMember(x => x.LastName, opt => opt.MapFrom(x => x.LastName))
                .ForMember(x => x.Logo, opt => opt.MapFrom(x => x.Logo));



            CreateMap<Message, MessageDto>()
                .ForMember(x => x.SendDate, opt => opt.MapFrom(x => x.SendDate))
                .ForMember(x => x.MessageText, opt => opt.MapFrom(x => x.MessageText))
                .ForMember(x => x.Username, opt => opt.MapFrom(x => x.Usergroup.User.Username))
                .ForMember(x => x.SendDate, opt => opt.MapFrom(x => x.SendDate));
        }
    }
}
 