using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityProjects.Core.User_Mapper
{
    public class GetUserResponse
    {
        public int UserId { get; set; }

        public string FirstName { get; set; } = null!;

        public string LastName { get; set; } = null!;

        public string Adresse { get; set; } = null!;

        public string Phone { get; set; } = null!;
        public int age { get; set; }

        public bool IsActive { get; set; }

        public string CityUserRoleName { get; set; }

        public string RegionName { get; set; }
    }
}
