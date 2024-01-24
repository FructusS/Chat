using AutoMapper;
using AvaloniaChat.Application.DTO.Group;
using AvaloniaChat.Application.DTO.Message;
using AvaloniaChat.Application.DTO.User;
using AvaloniaChat.Data.Models;
using AvaloniaChat.Domain.Models;

namespace AvaloniaChat.Business.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {

            #region User

            CreateMap<User, UserDto>()
                .ForMember(x => x.Username, opt => opt.MapFrom(x => x.Username))
                .ForMember(x => x.FirstName, opt => opt.MapFrom(x => x.FirstName))
                .ForMember(x => x.LastName, opt => opt.MapFrom(x => x.LastName))
                .ForMember(x => x.Logo, opt => opt.MapFrom(x => x.Logo));


            CreateMap<UpdateUserDto, User>()
                .ForMember(x => x.FirstName, opt =>
                {
                    opt.PreCondition(x => !string.IsNullOrEmpty(x.FirstName));
                    opt.MapFrom(x => x.FirstName);
                })
                .ForMember(x => x.Username, opt =>
                {
                    opt.PreCondition(x => !string.IsNullOrEmpty(x.Username));
                    opt.MapFrom(x => x.Username);
                })
                .ForMember(x => x.LastName, opt =>
                {
                    opt.PreCondition(x => !string.IsNullOrEmpty(x.LastName));
                    opt.MapFrom(x => x.LastName);
                })
                .ForMember(x => x.Logo, opt =>
                {
                    opt.PreCondition(x => x.Logo != null);
                    opt.MapFrom(x => x.Logo);
                });


            #endregion

            #region Message

            CreateMap<Message, MessageDto>()
                .ForMember(x => x.SendDate, opt => opt.MapFrom(x => x.SendDate))
                .ForMember(x => x.MessageText, opt => opt.MapFrom(x => x.MessageText))
                .ForMember(x => x.Username, opt => opt.MapFrom(x => x.User.Username))
                .ForMember(x => x.GroupId, opt => opt.MapFrom(x => x.GroupId));


            #endregion

            #region Group

            CreateMap<CreateGroupDto, Group>();
            CreateMap<GroupDto, Group>();
            CreateMap<Group, GroupDto>();
            CreateMap<Group, UpdateGroupDto>();

            #endregion

        }
    }
}
 