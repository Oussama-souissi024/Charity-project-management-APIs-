using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityProjects.Core
{
    public partial class Donations
    {
        [Key]
        public int DonationId { get; set; }

        public decimal Amount { get; set; }

        public int MemberId { get; set; }

        public int ProjectId { get; set; }

        public virtual Users Member { get; set; } = null!;

        public virtual Projects Project { get; set; } = null!;
    }

}


