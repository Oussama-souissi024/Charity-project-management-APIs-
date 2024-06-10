using CityProjects.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityProjects.Data.SqlServerEF
{
    public class CityUserRoleEntity : IDataHelper<CityUserRole>
    {
        private DataContext DB;
        private CityUserRole _Table;

        public CityUserRoleEntity()
        {
            DB = new DataContext();
        }
        public int Add(CityUserRole table)
        {
            if (DB.Database.CanConnect())
            {
                DB.Add(table);
                DB.SaveChangesAsync();
                return 1;
            }
            else
            {
                return 0;
            }
        }

        public int Add2(CityUserRole table, string S)
        {
            throw new NotImplementedException();
        }

        public int Delete(int ID)
        {
            if (DB.Database.CanConnect())
            {
                _Table = Find(ID);
                DB.Remove(_Table);
                DB.SaveChanges();
                return 1;
            }
            else
                return 0;
        }

        public int Edit(int ID, CityUserRole table)
        {
            DB = new DataContext();
            if (DB.Database.CanConnect())
            {
                DB.CityUserRoles.Update(table);
                DB.SaveChanges();
                return 1;
            }
            else
                return 0;
        }

        public int Edit2(int ID, int ID2, CityUserRole table, decimal D)
        {
            throw new NotImplementedException();
        }

        public CityUserRole Find(int ID)
        {
            if (DB.Database.CanConnect())
            {
                return DB.CityUserRoles.FirstOrDefault(x => x.CityId == ID);
            }
            else
                return null;
        }

        public CityUserRole Find3(string S)
        {
            throw new NotImplementedException();
        }

        public List<CityUserRole> GetAllData()
        {
            if (DB.Database.CanConnect())
            {
                return DB.CityUserRoles.ToList();
            }
            else
                return null;
        }

        public List<CityUserRole> Search(string SearchItem)
        {
            if (DB.Database.CanConnect())
            {
                return DB.CityUserRoles.Where(x => x.CityId.ToString().Contains(SearchItem)
                                           || x.RoleName.Contains(SearchItem)
                                      ).ToList();
            }
            else
                return null;
        }
    }
}
