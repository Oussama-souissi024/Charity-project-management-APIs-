using AutoMapper;
using CityProjects.Core.Mapper.Project_Mapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityProjects.Core.Project_Mapper
{
    public class ProjectMappingProfile : Profile
    {
        public ProjectMappingProfile()
        {
            CreateMap<CreateProjectRequest, Projects>()
                .ForMember(dest => dest.PresidentApproval, op => op.Ignore())
                .ForMember(dest => dest.SecretaryApproval, op => op.Ignore())
                .ForMember(dest => dest.Donations, op => op.Ignore())
                .ForMember(dest => dest.Status, op => op.Ignore())
                 .ForMember(dest => dest.Transportations,
                       op => op.MapFrom((src, dest) =>
                       src.TransportationIds.Select(id => new Transportations { TransportationId = id }).ToList()));


            CreateMap<Projects, GetProjectResponse>()
                .ForMember(dest => dest.ProjectManagerFullName, opt => opt.MapFrom(src => $"{src.User.FirstName}, {src.User.LastName}"))
                .ForMember(dest => dest.MaterialName, op => op.MapFrom(src => src.Material.Name))
                .ForMember(dest => dest.TransportationNames, op => op.MapFrom(src => src.Transportations.Select(t => t.Name).ToList()));

            CreateMap<UpdateProjectApprovalScretary, Projects>()
                .ForMember(dest => dest.Name, op => op.Ignore())
                .ForMember(dest => dest.Status, op => op.Ignore())
                .ForMember(dest => dest.Budget, op => op.Ignore())
                .ForMember(dest => dest.StartDate, op => op.Ignore())
                .ForMember(dest => dest.EndtDate, op => op.Ignore())
                .ForMember(dest => dest.Location, op => op.Ignore())
                .ForMember(dest => dest.Description, op => op.Ignore())
                .ForMember(dest => dest.ProjectManagerId, op => op.Ignore())
                .ForMember(dest => dest.Material, op => op.Ignore())
                .ForMember(dest => dest.PresidentApproval, op => op.Ignore())
                .ForMember(dest => dest.SecretaryApproval, op => op.Ignore())
                .ForMember(dest => dest.MaterialProjectID, op => op.Ignore())
                .ForMember(dest => dest.Donations, op => op.Ignore())
                .ForMember(dest => dest.Transportations, op => op.Ignore());

            CreateMap<UpdateProjectApprovalPresident, Projects>()
                .ForMember(dest => dest.Name, op => op.Ignore())
                .ForMember(dest => dest.Status, op => op.Ignore())
                .ForMember(dest => dest.Budget, op => op.Ignore())
                .ForMember(dest => dest.StartDate, op => op.Ignore())
                .ForMember(dest => dest.EndtDate, op => op.Ignore())
                .ForMember(dest => dest.Location, op => op.Ignore())
                .ForMember(dest => dest.Description, op => op.Ignore())
                .ForMember(dest => dest.ProjectManagerId, op => op.Ignore())
                .ForMember(dest => dest.Material, op => op.Ignore())
                .ForMember(dest => dest.PresidentApproval, op => op.Ignore())
                .ForMember(dest => dest.SecretaryApproval, op => op.Ignore())
                .ForMember(dest => dest.MaterialProjectID, op => op.Ignore())
                .ForMember(dest => dest.Donations, op => op.Ignore())
                .ForMember(dest => dest.Transportations, op => op.Ignore());
        }

    }
}
