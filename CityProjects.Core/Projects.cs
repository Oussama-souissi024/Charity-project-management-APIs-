using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityProjects.Core
{
    public partial class Projects
    {
        [Key]
        public int ProjectId { get; set; }

        public string? Name { get; set; } 

        public string? Status { get; set; } 

        public decimal? Budget { get; set; }

        public DateOnly? StartDate { get; set; }

        public DateOnly? EndtDate { get; set; }

        public string? Location { get; set; }

        [MaxLength(200)]
        public string? Description { get; set; } 

        [ForeignKey("User")]
        public int ProjectManagerId { get; set; }

        [ForeignKey("Material")]
        public int MaterialProjectID{ get; set; }

        public bool PresidentApproval { get; set; }

        public bool SecretaryApproval { get; set; }

        public virtual Users? User { get; set; } 

        public virtual ICollection<Donations> Donations { get; set; } = new List<Donations>();

        public virtual Materials Material { get; set; }

        public virtual ICollection<Transportations> Transportations { get; set; } = new List<Transportations>();
    }
}



