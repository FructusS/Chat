using AutoMapper;
using AvaloniaChat.Backend.Models;
using AvaloniaChat.Backend.Models.UserModels;

namespace AvaloniaChat.Backend.Configs
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<UpdateUserModel, User>().ReverseMap();


        }
    }
}
