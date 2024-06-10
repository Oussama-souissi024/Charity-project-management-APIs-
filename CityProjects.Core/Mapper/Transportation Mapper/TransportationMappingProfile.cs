using AutoMapper;
using CityProjects.Core.Material_Mapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityProjects.Core.Transportation_Mapper
{
    public class TransportationMappingProfile : Profile
    {
        public TransportationMappingProfile()
        {
            CreateMap<CreateTransportationRequest, Transportations>()
                .ForMember(dest => dest.Projects, op => op.Ignore());

            CreateMap<Transportations, GetTransportaionResponse>();
                
        }
    }
}
