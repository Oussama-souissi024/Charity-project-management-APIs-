using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityProjects.Core.Donation_Mapper
{
    public class UpdateDonnationRequest
    {
        public int DonationId { get; set; }
        public decimal Amount { get; set; }
    }
}
