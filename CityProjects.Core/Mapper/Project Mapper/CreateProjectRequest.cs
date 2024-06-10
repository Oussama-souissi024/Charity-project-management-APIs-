using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityProjects.Core.Project_Mapper
{
    public class CreateProjectRequest
    {
        public string Name { get; set; } = null!;

        public decimal Budget { get; set; }

        public DateOnly? StartDate { get; set; }

        public DateOnly? EndtDate { get; set; }

        public string? Location { get; set; }

        public string? Description { get; set; }

        public int MaterialProjectID { get; set; }

        public List<int> TransportationIds { get; set; } = new List<int>(); 
    }

}
