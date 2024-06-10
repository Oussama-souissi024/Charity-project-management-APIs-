using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityProjects.Core.Mapper.User_Mapper
{
    public class CreateUserRequestForUpdatingRole
    {
        public int UserId { get; set; }
        public int CityUserRoleId { get; set; }
    }
}
