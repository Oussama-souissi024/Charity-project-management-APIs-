using CityProjects.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityProjects.Data.SqlServerEF
{
    public class MaterialEntity : IDataHelper<Materials>
    {
        private DataContext DB;
        private Materials _Table;

        public MaterialEntity()
        {
            DB = new DataContext();
        }
        public int Add(Materials table)
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

        public int Edit(int ID, Materials table)
        {
            var NewMaterial = new Materials()
            {
                MaterialId = ID,
                Name = table.Name,
                Description = table.Description,
                Cost = table.Cost
            };
            DB = new DataContext();
            if (DB.Database.CanConnect())
            {
                DB.Materials.Update(NewMaterial);
                DB.SaveChanges();
                return 1;
            }
            else
                return 0;
        }



        public Materials Find(int ID)
        {
            if (DB.Database.CanConnect())
            {
                return DB.Materials.FirstOrDefault(x => x.MaterialId == ID);
            }
            else
                return null;
        }

        public List<Materials> GetAllData()
        {

            if (DB.Database.CanConnect())
            {
                return DB.Materials.ToList();
            }
            else
                return null;
        }

        public List<Materials> Search(string SearchItem)
        {
            if (DB.Database.CanConnect())
            {
                return DB.Materials.Where(x => x.MaterialId.ToString().Contains(SearchItem)
                                           || x.Name.Contains(SearchItem)
                                           || x.Description.Contains(SearchItem)
                                           || x.Cost.ToString().Contains(SearchItem)
                                      ).ToList();
            }
            else
                return null;
        }

        public int Add2(Materials table, string S)
        {
            throw new NotImplementedException();
        }
        public int Edit2(int ID1, int ID2, Materials table, decimal D)
        {
            throw new NotImplementedException();
        }

        public Materials Find3(string S)
        {
            throw new NotImplementedException();
        }
    }
}