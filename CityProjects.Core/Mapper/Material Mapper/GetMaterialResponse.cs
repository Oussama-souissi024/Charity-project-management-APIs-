using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityProjects.Core.Material_Mapper
{
    public class GetMaterialResponse
    {
        public int MaterialId { get; set; }

        public string Name { get; set; } = null!;

        public string Description { get; set; } = null!;

        public decimal Cost { get; set; }
    }
}
