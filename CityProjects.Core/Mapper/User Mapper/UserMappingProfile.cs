using AutoMapper;
using CityProjects.Core.Mapper.User_Mapper;
using CityProjects.Core.User_Mapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityProjects.Core.User_Mapper
{
    public class UserMappingProfile : Profile
    {
        public UserMappingProfile()
        {
            CreateMap<CreateUserRequest, Users>()
                .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => false)) // Set IsActive to false by default
                .ForMember(dest => dest.AuthenticationUserId, opt => opt.Ignore()) // Ignore AuthenticationUserId
                .ForMember(dest => dest.CityUserRole, opt => opt.Ignore()) // Ignore CityUserRole
                .ForMember(dest => dest.Mandate, opt => opt.Ignore()) // Ignore Mandate
                .ForMember(dest => dest.Region, opt => opt.Ignore()); // Ignore Region

            CreateMap<CreateUserRequestForUpdatingRole, Users>()
                .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => true)) // Set IsActive to true by default
                .ForMember(dest => dest.AuthenticationUserId, opt => opt.Ignore()) // Ignore AuthenticationUserId
                .ForMember(dest => dest.CityUserRole, opt => opt.Ignore()) // Ignore CityUserRole
                .ForMember(dest => dest.Mandate, opt => opt.Ignore()) // Ignore Mandate
                .ForMember(dest => dest.Region, opt => opt.Ignore()) // Ignore Region
                .ForMember(dest => dest.FirstName, opt => opt.Ignore()) // Ignore FirstName
                .ForMember(dest => dest.LastName, opt => opt.Ignore()) // Ignore LastName
                .ForMember(dest => dest.Phone, opt => opt.Ignore()) // Ignore Phone
                .ForMember(dest => dest.age, opt => opt.Ignore()) // Ignore age
                .ForMember(dest => dest.Adresse, opt => opt.Ignore()) // Ignore Adresse
                .ForMember(dest => dest.Phone, opt => opt.Ignore()); // Ignore Phone

            CreateMap<Users, GetUserResponse>()
                .ForMember(dest => dest.CityUserRoleName, opt => opt.MapFrom(src => src.CityUserRole.RoleName))
                .ForMember(dest => dest.RegionName, opt => opt.MapFrom(src => src.Region.Name));
        }
    }
}
