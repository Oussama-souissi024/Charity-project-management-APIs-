using CityProjects.Core;
using CityProjects.Data.SqlServerEF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;



namespace CityProjects.Data.Background_Services
{
    public class MondatService
    {
        private DataContext DB;
        public MondatService()
        {
            DB = new DataContext();
        }


        public async Task<IEnumerable<Mandates>> GetExpiredMandatsAsync(DateTime currentDate)
        {
            // Retrieve expired mandates whose end date is before the current date
            var expiredMandats = await DB.Mandates
                .Where(m => m.EndtDate < currentDate)
                .ToListAsync();

            return expiredMandats;
        }
    }
}
