using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityProjects.Core.Transportation_Mapper
{
    public class GetTransportaionResponse
    {
        public int TransportationId { get; set; }

        public string Name { get; set; } = null!;

        public string Description { get; set; } = null!;
    }
}
