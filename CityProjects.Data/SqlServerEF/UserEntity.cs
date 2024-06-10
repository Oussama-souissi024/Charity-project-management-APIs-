using CityProjects.Core;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Security.Claims;

namespace CityProjects.Data.SqlServerEF
{
    public class UserEntity : IDataHelper<Users>
    {
        private DataContext DB;
        private Users _Table;

        public UserEntity()
        {
            DB = new DataContext();
        }
        public int Add(Users table)
        {
            DB = new DataContext();
            if (DB.Database.CanConnect())
            {
                DB.Users.Add(table);
                DB.SaveChanges();
                return 1;
            }
            else
            {
                return 0;
            }
        }

        public int Delete(int ID)
        {
            if (DB.Database.CanConnect())
            {
                _Table = Find(ID);
                _Table.IsActive = false;
                DB.Update(_Table);
                DB.SaveChanges();
                return 1;
            }
            else
                return 0;
        }

        public int Edit(int Id, Users table)
        {
            var NewUser = new Users
            {
                UserId = Id,
                FirstName = table.FirstName,
                LastName = table.LastName,
                Adresse = table.Adresse,
                Phone = table.Phone,
                IsActive = true,
                AuthenticationUserId = table.AuthenticationUserId,
                CityUserRoleId = table.CityUserRoleId,
                RegionId = table.RegionId,
                age = table.age
            };


            DB = new DataContext();
            if (DB.Database.CanConnect())
            {
                DB.Users.Update(NewUser);
                DB.SaveChanges();
                return 1;
            }
            else
                return 0;
        }

        public Users Find(int ID)
        {
            if (DB.Database.CanConnect())
            {
                return DB.Users.FirstOrDefault(x => x.UserId == ID);
            }
            else
                return null;
        }

        public List<Users> GetAllData()
        {
            if (DB.Database.CanConnect())
            {
                return DB.Users.ToList();
            }
            else
                return null;
        }

        public List<Users> Search(string SearchItem)
        {
            if (DB.Database.CanConnect())
            {
                return DB.Users.Where(x => x.UserId.ToString().Contains(SearchItem)
                                           || x.FirstName.Contains(SearchItem)
                                           || x.LastName.Contains(SearchItem)
                                           || x.Adresse.Contains(SearchItem)
                                           || x.Phone.Contains(SearchItem)
                                           || x.CityUserRoleId.ToString().Contains(SearchItem)
                                           || x.IsActive.ToString().Contains(SearchItem)
                                           || x.age.ToString().Contains(SearchItem)                              
                                           ).ToList();

            }
            else
                return null;
        }
        public int Add2(Users table, string S)
        {
            throw new NotImplementedException();
        }

        public int Edit2(int ID, int ID2, Users table, decimal D)
        {
            if(D == 1)
            {
                if (IsPresidentAlreadyExist(table.RegionId))
                {
                    return 0;
                }
            }
            else
            {
                if (IsSECRETARYAlreadyExist(table.RegionId))
                {
                    return 0;
                }
            }
            var NewUser = new Users
            {
                UserId = ID,
                FirstName = table.FirstName,
                LastName = table.LastName,
                Adresse = table.Adresse,
                Phone = table.Phone,
                IsActive = true,
                AuthenticationUserId = table.AuthenticationUserId,
                CityUserRoleId = ID2,
                RegionId = table.RegionId,
                age = table.age
            };


            DB = new DataContext();
            if (DB.Database.CanConnect())
            {
                DB.Users.Update(NewUser);
                DB.SaveChanges();
                return 1;
            }
            else
                return 0;
        }

        public bool IsPresidentAlreadyExist(int RegionID)
        {
            var user = DB.Users.Any(u => u.RegionId == RegionID && u.CityUserRoleId == 1);
            return user;
        }

        public bool IsSECRETARYAlreadyExist(int RegionID)
        {
            var user = DB.Users.Any(u => u.RegionId == RegionID && u.CityUserRoleId == 2);
            return user;
        }

        public Users Find3 (string AuthId)
        {
            return DB.Users.Where(u => u.AuthenticationUserId == AuthId).FirstOrDefault();
        }
    }
}
