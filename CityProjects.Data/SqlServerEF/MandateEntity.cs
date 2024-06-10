using CityProjects.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityProjects.Data.SqlServerEF
{
    public class MandateEntity : IDataHelper<Mandates>
    {
        private DataContext DB;
        private Mandates _Table;

        public MandateEntity()
        {
            DB = new DataContext();
        }


        public Mandates Find(int ID)
        {
            if (DB.Database.CanConnect())
            {
                return DB.Mandates.FirstOrDefault(x => x.MandateId == ID);
            }
            else
                return null;
        }

        public List<Mandates> GetAllData()
        {
            if (DB.Database.CanConnect())
            {
                return DB.Mandates.ToList();
            }
            else
                return null;
        }

        public List<Mandates> Search(string SearchItem)
        {
            if (DB.Database.CanConnect())
            {
                return DB.Mandates.Where(x => x.MandateId.ToString().Contains(SearchItem)
                                           || x.PresidentId.ToString().Contains(SearchItem)
                                           || x.StartDate.ToString().Contains(SearchItem)
                                           || x.EndtDate.ToString().Contains(SearchItem)
                                           || x.IsActive.ToString().Contains(SearchItem)
                                      ).ToList();
            }
            else
                return null;
        }


        public async Task<IEnumerable<Mandates>> GetExpiredMandatsAsync(DateTime currentDate)
        {
            // Recuperate expiry items on the date of the final date on the actual date
            var expiredMandats = await DB.Mandates
                .Where(m => m.EndtDate < currentDate)
                .ToListAsync();

            return expiredMandats;
        }


        public int Add(Mandates table)
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

        public int Add2(Mandates table, string S)
        {
            throw new NotImplementedException();
        }

        public int Delete(int ID)
        {
            throw new NotImplementedException();
        }

        public int Edit(int ID, Mandates table)
        {
            throw new NotImplementedException();
        }

        public int Edit2(int ID, int ID2, Mandates table, decimal D)
        {
            throw new NotImplementedException();
        }

        public Mandates Find3(string S)
        {
            throw new NotImplementedException();
        }
    }
}