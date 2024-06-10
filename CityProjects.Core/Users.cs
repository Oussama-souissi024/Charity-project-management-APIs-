using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityProjects.Core
{
    public partial class Users
    {
        [Key]
        public int UserId { get; set; }

        public string FirstName { get; set; } = null!;

        public string LastName { get; set; } = null!;

        public string Adresse { get; set; } = null!;

        public string Phone { get; set; } = null!;

        public int age { get; set; } 

        public bool IsActive { get; set; }

        //[DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public string? AuthenticationUserId { get; set; } 


        [Range(1, 4, ErrorMessage = "The CityUserRole must be between 1 and 4.")]
        public int CityUserRoleId { get; set; }

        [Range(1, 4, ErrorMessage = "The Region must be between 1 and 4.")]
        public int RegionId { get; set; }

        public virtual CityUserRole CityUserRole { get; set; } = null!;

        public virtual ICollection<Donations> Donations { get; set; } = new List<Donations>();

        public virtual ICollection<Projects> Projects { get; set; } = new List<Projects>();

        public virtual Mandates? Mandate { get; set; }

        public virtual Region Region { get; set; } = null!;
    }

}


