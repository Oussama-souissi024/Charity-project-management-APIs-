using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityProjects.Core.Donation_Mapper
{
    public class DonnationMappingProfile : Profile
    {
        public DonnationMappingProfile()
        {
            CreateMap<CreateDonnationRequest, Donations>()
                .ForMember(dest => dest.Member, op => op.Ignore())
                .ForMember(dest => dest.Project, op => op.Ignore());

            CreateMap<UpdateDonnationRequest, Donations>()
                .ForMember(dest => dest.Member, op => op.Ignore())
                .ForMember(dest => dest.Project, op => op.Ignore())
                .ForMember(dest => dest.ProjectId, op => op.Ignore())
                .ForMember(dest => dest.MemberId, op => op.Ignore());


            CreateMap<Donations, GetDonnationResponse>()
                .ForMember(dest => dest.MemberFullName, op => op.MapFrom(src => $"{src.Member.FirstName}, {src.Member.LastName}"))
                .ForMember(dest => dest.ProjectName, op => op.MapFrom(src => src.Project.Name));
        }
    }
}
