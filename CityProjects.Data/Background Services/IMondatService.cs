using CityProjects.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityProjects.Data.Background_Services
{
    public interface IMondatService
    {
        public Task<IEnumerable<Mandates>> GetExpiredMandatsAsync(DateTime currentDate);
    }
}
