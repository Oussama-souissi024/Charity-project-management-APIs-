using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityProjects.Core
{
    public partial class Materials
    {
        [Key]
        public int MaterialId { get; set; }

        public string Name { get; set; } = null!;

        public string Description { get; set; } = null!;

        public decimal Cost { get; set; }

        public virtual ICollection<Projects> Projects { get; set; } = new List<Projects>();
    }

}


