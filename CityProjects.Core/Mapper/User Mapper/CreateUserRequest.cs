using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityProjects.Core.User_Mapper
{
    public class CreateUserRequest
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Adresse { get; set; }
        public string Phone { get; set; }
        public int age { get; set; }
        public int CityUserRoleId { get; set; }
        public int RegionId { get; set; }
    }
}
