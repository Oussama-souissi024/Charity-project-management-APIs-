using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityProjects.Data.Background_Services
{
    public class MyBackgroundService : BackgroundService
    {
        private readonly ILogger<MyBackgroundService> logger;
        private readonly IMondatService mondatService;
        private readonly IUserRoleService userRoleService;

        public MyBackgroundService(ILogger<MyBackgroundService> logger,
                                   IMondatService mondatService,
                                   IUserRoleService userRoleService)
        {
            this.logger = logger;
            this.mondatService = mondatService;
            this.userRoleService = userRoleService;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                logger.LogInformation("MyBackgroundService is running.");

                var currentDate = DateTime.Now;

                var expiredMandats = await mondatService.GetExpiredMandatsAsync(currentDate);

                foreach (var mandat in expiredMandats)
                {
                    // Mettez à jour les rôles et les mandats
                    await userRoleService.UpdateRolesAndMandatsAsync(mandat);
                }

                await Task.Delay(TimeSpan.FromDays(1), stoppingToken); // Attendre 1 jour
            }
        }
    }
}
