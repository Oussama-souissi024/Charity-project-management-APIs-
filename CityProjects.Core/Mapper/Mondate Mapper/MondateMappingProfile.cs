using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityProjects.Core.Mapper.Mondate_Mapper
{
    public class MondateMappingProfile : Profile
    {
        public MondateMappingProfile()
        {
            CreateMap<CreateMondateRequest, Mandates>()
                .ForMember(dest => dest.President, op => op.Ignore());

            CreateMap<Mandates, GetMondateResponse>()
                .ForMember(dest => dest.PresidentFullName, opt => opt.MapFrom(src => $"{src.President.FirstName}, {src.President.LastName}"));
        }
    }
}
