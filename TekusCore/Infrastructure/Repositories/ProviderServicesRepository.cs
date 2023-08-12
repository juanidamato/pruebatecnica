using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TekusCore.Application.Interfaces.Infrastructure;
using TekusCore.Application.Interfaces.Repositories;
using TekusCore.Domain.Entities;

namespace TekusCore.Infrastructure.Repositories
{
    public class ProviderServicesRepository : IProviderServicesRepository
    {
        private readonly ILogger<ProviderServicesRepository> _logger;
        private readonly IDatabaseHelper _db;

        public ProviderServicesRepository(
            ILogger<ProviderServicesRepository> logger,
            IDatabaseHelper db)
        {
            _logger = logger;
            _db = db;
        }

        public async Task<(bool, List<ProviderServicesEntity>?)> GetServicesByProvider(int IdProvider)
        {
            try
            {
                var r = await _db.GetArrayDataAsync<ProviderServicesEntity, dynamic>("Services_Select_ByProvider", new { IdProvider= IdProvider });
                if (r is null)
                {
                    return (true, null);
                }
                else
                {
                    return (true, r.ToList());
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception Error in GetServicesByProvider");
                return (false, null);
            }
        }
    }
}
