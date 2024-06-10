using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityProjects.Core
{
    public partial class Transportations
    {
        [Key]
        public int TransportationId { get; set; }

        public string Name { get; set; } = null!;

        public string Description { get; set; } = null!;

        public virtual ICollection<Projects> Projects { get; set; } = new List<Projects>();
    }
}



