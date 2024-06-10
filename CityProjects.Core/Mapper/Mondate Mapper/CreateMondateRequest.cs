using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityProjects.Core.Mapper.Mondate_Mapper
{
    public class CreateMondateRequest
    {
        public DateOnly StartDate { get; set; }

        public DateOnly EndtDate { get; set; }

        public int PresidentId { get; set; }

        public bool IsActive { get; set; }
    }
}
