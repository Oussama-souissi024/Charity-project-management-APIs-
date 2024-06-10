using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityProjects.Core.Mapper.Mondate_Mapper
{
    public class GetMondateResponse
    {
        public int MandateId { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndtDate { get; set; }

        public string PresidentFullName { get; set; }
    }
}
