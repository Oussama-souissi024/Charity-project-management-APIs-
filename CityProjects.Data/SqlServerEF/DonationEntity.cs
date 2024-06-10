using CityProjects.Core;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityProjects.Data.SqlServerEF
{
    public class DonationEntity : IDataHelper<Donations>
    {
        private DataContext DB;
        private Donations _Table;

        public DonationEntity()
        {
            DB = new DataContext();
        }
        public int Add2(Donations table, string CurrentUserId)
        {
            if (DB.Database.CanConnect())
            {
                table.MemberId = DB.Users.Where(p => p.AuthenticationUserId == CurrentUserId).Select(s => s.UserId).FirstOrDefault();
                DB.Donations.Add(table);
                var ProjectDonnation = DB.Projects.Find(table.ProjectId);
                ProjectDonnation.Budget += table.Amount;
                DB.Projects.Update(ProjectDonnation);
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
                DB.Remove(_Table);
                DB.SaveChanges();
                return 1;
            }
            else
                return 0;
        }

        public int Edit2(int ID, int ProjectID ,Donations table, decimal LastAmount)
        {
            var Donnation = new Donations()
            {
                DonationId = ID,
                Amount = table.Amount,
                MemberId = table.MemberId,
                ProjectId = ProjectID
            };

            DB = new DataContext();
            if (DB.Database.CanConnect())
            {
                DB.Donations.Update(Donnation);
                var ProjectDonnation = DB.Projects.Find(ProjectID);
                ProjectDonnation.Budget -= LastAmount;
                ProjectDonnation.Budget += table.Amount;
                DB.SaveChanges();
                return 1;
            }
            else
                return 0;
        }

        public Donations Find(int ID)
        {
            if (DB.Database.CanConnect())
            {
                return DB.Donations.FirstOrDefault(x => x.DonationId == ID);
            }
            else
                return null;
        }

        public List<Donations> GetAllData()
        {
            if (DB.Database.CanConnect())
            {
                return DB.Donations.ToList();
            }
            else
                return null;
        }

        public List<Donations> Search(string SearchItem)
        {
            if (DB.Database.CanConnect())
            {
                return DB.Donations.Where(x => x.DonationId.ToString().Contains(SearchItem)
                                           || x.Amount.ToString().Contains(SearchItem)
                                           || x.MemberId.ToString().Contains(SearchItem)
                                           || x.ProjectId.ToString().Contains(SearchItem)
                                      ).ToList();
            }
            else
                return null;
        }

        public int Add(Donations table)
        {
            throw new NotImplementedException();
        }

        public int Edit(int ID, Donations table)
        {
            throw new NotImplementedException();
        }

        public Donations Find3(string S)
        {
            throw new NotImplementedException();
        }
    }
}
