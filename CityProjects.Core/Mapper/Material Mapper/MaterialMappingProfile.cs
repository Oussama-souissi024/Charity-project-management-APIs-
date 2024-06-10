using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityProjects.Core.Material_Mapper
{
    public class MaterialMappingProfile : Profile
    {
        public MaterialMappingProfile()
        {
            CreateMap<CreateMaterialRequest, Materials>()
                .ForMember(dest => dest.Projects, opt => opt.Ignore());

            CreateMap<Materials, GetMaterialResponse>();
        }

    }
}
