using AutoMapper;
using Lextatico.Application.Dtos.User;
using Lextatico.Domain.Models;

namespace Lextatico.Application.AutoMapper.Profiles
{
    public class ApplicationUserProfile : Profile
    {
        public ApplicationUserProfile()
        {
            // DTO TO MODEL
            CreateMap<UserSignInDto, ApplicationUser>()
                .ForMember(applicationUser => applicationUser.UserName,
                options => options.MapFrom(userSignin => userSignin.Email));

            // MODEL TO DTO
            CreateMap<ApplicationUser, UserDetailDto>();
        }
    }
}
