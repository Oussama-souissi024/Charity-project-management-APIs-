using CityProjects.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityProjects.Data.SqlServerEF
{
    public class RegionEntity : IDataHelper<Region>
    {
        //private DataContext DB;
        //private Region _Table;

        //public RegionEntity()
        //{
        //    DB = new DataContext();
        //}
        //public int Add(Region table)
        //{
        //    if (DB.Database.CanConnect())
        //    {
        //        DB.Add(table);
        //        DB.SaveChangesAsync();
        //        return 1;
        //    }
        //    else
        //    {
        //        return 0;
        //    }
        //}

        //public int Delete(int ID)
        //{
        //    if (DB.Database.CanConnect())
        //    {
        //        _Table = Find(ID);
        //        DB.Remove(_Table);
        //        DB.SaveChanges();
        //        return 1;
        //    }
        //    else
        //        return 0;
        //}

        //public int Edit(int ID, Region table)
        //{
        //    DB = new DataContext();
        //    if (DB.Database.CanConnect())
        //    {
        //        DB.Regions.Update(table);
        //        DB.SaveChanges();
        //        return 1;
        //    }
        //    else
        //        return 0;
        //}

        //public Region Find(int ID)
        //{
        //    if (DB.Database.CanConnect())
        //    {
        //        return DB.Regions.FirstOrDefault(x => x.ID == ID);
        //    }
        //    else
        //        return null;
        //}

        //public List<Region> GetAllData()
        //{
        //    if (DB.Database.CanConnect())
        //    {
        //        return DB.Regions.ToList();
        //    }
        //    else
        //        return null;
        //}

        //public List<Region> Search(string SearchItem)
        //{
        //    if (DB.Database.CanConnect())
        //    {
        //        return DB.Regions.Where(x => x.ID.ToString().Contains(SearchItem)
        //                                   || x.Name.Contains(SearchItem)
        //                              ).ToList();
        //    }
        //    else
        //        return null;
        //}
        public int Add(Region table)
        {
            throw new NotImplementedException();
        }

        public int Add2(Region table, string S)
        {
            throw new NotImplementedException();
        }

        public int Delete(int ID)
        {
            throw new NotImplementedException();
        }

        public int Edit(int ID, Region table)
        {
            throw new NotImplementedException();
        }

        public int Edit2(int ID,int ID2, Region table, decimal D)
        {
            throw new NotImplementedException();
        }

        public Region Find(int ID)
        {
            throw new NotImplementedException();
        }

        public Region Find3(string S)
        {
            throw new NotImplementedException();
        }

        public List<Region> GetAllData()
        {
            throw new NotImplementedException();
        }

        public List<Region> Search(string SearchIthem)
        {
            throw new NotImplementedException();
        }
    }
}
