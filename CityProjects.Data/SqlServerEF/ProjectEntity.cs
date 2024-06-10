using CityProjects.Data;
using CityProjects.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CityProjects.Data.SqlServerEF;
using Microsoft.EntityFrameworkCore;

namespace CityProjectss.Data.SqlServerEF
{
    public class ProjectEntity : IDataHelper<Projects>
    {
        private readonly DataContext dbContext;

        public ProjectEntity(DataContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public int Add2(Projects table, string currentUserId)
        {
            if (dbContext.Database.CanConnect())
            {
                table.ProjectManagerId = dbContext.Users.Where(p => p.AuthenticationUserId == currentUserId)
                                                         .Select(s => s.UserId)
                                                         .FirstOrDefault();
                table.Status = "En attente de confirmation";
                table.PresidentApproval = false;
                table.SecretaryApproval = false;
                dbContext.Add(table);
                dbContext.SaveChanges();
                return 1;
            }
            else
            {
                return 0;
            }
        }

        public int Delete(int id)
        {
            if (dbContext.Database.CanConnect())
            {
                var table = Find(id);
                if (table != null)
                {
                    table.Status = "Annulé (ce projet a été supprimé)";
                    dbContext.Update(table);
                    dbContext.SaveChanges();
                    return 1;
                }
            }
            return 0;
        }

        public int Edit(int id, Projects table)
        {
            if (dbContext.Database.CanConnect())
            {
                var existingProject = dbContext.Projects.Include(p => p.Transportations).FirstOrDefault(p => p.ProjectId == id);
                if (existingProject != null)
                {
                    existingProject.Name = table.Name;
                    existingProject.Status = table.Status;
                    existingProject.Budget = table.Budget;
                    existingProject.StartDate = table.StartDate;
                    existingProject.EndtDate = table.EndtDate;
                    existingProject.Location = table.Location;
                    existingProject.Description = table.Description;
                    existingProject.ProjectManagerId = table.ProjectManagerId;
                    existingProject.MaterialProjectID = table.MaterialProjectID;
                    existingProject.PresidentApproval = table.PresidentApproval;
                    existingProject.SecretaryApproval = table.SecretaryApproval;

                    // Mettre à jour les transportations
                    existingProject.Transportations.Clear();
                    foreach (var transportation in table.Transportations)
                    {
                        existingProject.Transportations.Add(transportation);
                    }

                    dbContext.Projects.Update(existingProject);
                    dbContext.SaveChanges();
                    return 1;
                }
            }
            return 0;
        }

        public Projects Find(int id)
        {
            if (dbContext.Database.CanConnect())
            {
                return dbContext.Projects.Include(p => p.Transportations).FirstOrDefault(x => x.ProjectId == id);
            }
            else
            {
                return null;
            }
        }

        public List<Projects> GetAllData()
        {
            if (dbContext.Database.CanConnect())
            {
                return dbContext.Projects.Include(p => p.Transportations).ToList();
            }
            else
            {
                return null;
            }
        }

        public List<Projects> Search(string searchItem)
        {
            if (dbContext.Database.CanConnect())
            {
                return dbContext.Projects
                    .Where(x => x.ProjectId.ToString().Contains(searchItem)
                             || x.Name.Contains(searchItem)
                             || x.Status.Contains(searchItem)
                             || x.StartDate.ToString().Contains(searchItem)
                             || x.EndtDate.ToString().Contains(searchItem)
                             || x.Location.Contains(searchItem)
                             || x.PresidentApproval.ToString().Contains(searchItem)
                             || x.SecretaryApproval.ToString().Contains(searchItem)
                             || x.Budget.ToString().Contains(searchItem)
                             || x.ProjectManagerId.ToString().Contains(searchItem))
                    .Include(p => p.Transportations)
                    .ToList();
            }
            else
            {
                return null;
            }
        }

        public int Add(Projects table)
        {
            throw new NotImplementedException();
        }

        public int Edit2(int id, int id2, Projects table, decimal d)
        {
            throw new NotImplementedException();
        }

        public Projects Find3(string s)
        {
            throw new NotImplementedException();
        }
    }

}
