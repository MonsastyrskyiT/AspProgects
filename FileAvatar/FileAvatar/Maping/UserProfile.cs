using AutoMapper;
using FileAvatar.DTO;
using FileAvatar.Data.Models;

namespace FileAvatar.Maping
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserDTO>()
                .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => $"{src.Name} {src.LastName}"));
        }
    }
}
