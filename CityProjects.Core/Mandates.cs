using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityProjects.Core
{
    public partial class Mandates
    {
        [Key]
        public int MandateId { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndtDate { get; set; }

        public int PresidentId { get; set; }

        public bool IsActive { get; set; }

        public virtual Users President { get; set; } = null!;
    }

}


