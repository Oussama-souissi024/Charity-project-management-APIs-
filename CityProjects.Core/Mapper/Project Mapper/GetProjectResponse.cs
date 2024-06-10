using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityProjects.Core.Project_Mapper
{
    public class GetProjectResponse
    {
        public int ProjectId { get; set; }

        public string Name { get; set; } = null!;

        public string Status { get; set; } = null!;

        public decimal Budget { get; set; }

        public DateOnly? StartDate { get; set; }

        public DateOnly? EndtDate { get; set; }

        public string? Location { get; set; } 

        public string? Description { get; set; }

        public string ProjectManagerFullName { get; set; }

        public string MaterialName { get; set; }

        public List<string> TransportationNames { get; set; } = new List<string>(); 

        public bool PresidentApproval { get; set; }

        public bool SecretaryApproval { get; set; }
    }
}
