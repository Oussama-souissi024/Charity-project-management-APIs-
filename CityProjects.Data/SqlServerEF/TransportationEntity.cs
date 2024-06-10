using CityProjects.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityProjects.Data.SqlServerEF
{
    public class TransportationEntity : IDataHelper<Transportations>
    {
        private DataContext DB;
        private Transportations _Table;

        public TransportationEntity()
        {
            DB = new DataContext();
        }
        public int Add(Transportations table)
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

        public int Edit(int ID, Transportations table)
        {
            var Transportation = new Transportations()
            {
                TransportationId = ID,
                Name = table.Name,
                Description = table.Description
            };
            DB = new DataContext();
            if (DB.Database.CanConnect())
            {
                DB.Transportations.Update(Transportation);
                DB.SaveChanges();
                return 1;
            }
            else
                return 0;
        }

        public Transportations Find(int ID)
        {
            if (DB.Database.CanConnect())
            {
                return DB.Transportations.FirstOrDefault(x => x.TransportationId== ID);
            }
            else
                return null;
        }

        public List<Transportations> GetAllData()
        {
            if (DB.Database.CanConnect())
            {
                return DB.Transportations.ToList();
            }
            else
                return null;
        }

        public List<Transportations> Search(string SearchItem)
        {
            if (DB.Database.CanConnect())
            {
                return DB.Transportations.Where(x => x.TransportationId.ToString().Contains(SearchItem)
                                           || x.Description.Contains(SearchItem)
                                           || x.Name.ToString().Contains(SearchItem)
                                      ).ToList();
            }
            else
                return null;
        }
    


        public int Add2(Transportations table, string S)
        {
            throw new NotImplementedException();
        }

        public int Edit2(int ID, int ID2, Transportations table, decimal D)
        {
            throw new NotImplementedException();
        }

        public Transportations Find3(string S)
        {
            throw new NotImplementedException();
        }
    }
}
