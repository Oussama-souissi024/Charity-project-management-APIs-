using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityProjects.Core
{
    public partial class CityUserRole
    {
        public int CityId { get; set; }

        public string RoleName { get; set; } = null!;

        public virtual ICollection<Users> Users { get; set; } = new List<Users>();
    }

}


