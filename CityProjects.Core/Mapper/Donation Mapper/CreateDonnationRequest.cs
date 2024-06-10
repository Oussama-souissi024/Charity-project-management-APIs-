using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityProjects.Core.Donation_Mapper
{
    public class CreateDonnationRequest
    {
        public decimal Amount { get; set; }

        public int ProjectId { get; set; }
    }
}
