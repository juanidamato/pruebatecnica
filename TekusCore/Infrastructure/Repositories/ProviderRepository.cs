using Dapper;
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
    public class ProviderRepository : IProviderRepository
    {
        private readonly ILogger<ProviderRepository> _logger;
        private readonly IDatabaseHelper _db;

        public ProviderRepository(
            ILogger<ProviderRepository> logger,
            IDatabaseHelper db)
        {
            _logger = logger;
            _db = db;
        }

        public async Task<(bool, List<ProviderEntity>?)> GetAllProviders()
        {
            try
            {
                var r = await _db.GetArrayDataAsync<ProviderEntity, dynamic>("Providers_Select_All", new {  });
                if (r is  null)
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
                _logger.LogError(ex, "Exception Error in GetAllProviders");
                return (false, null);
            }
        }

        public async Task<(bool, ProviderEntity?)> GetProviderById(int id)
        {
            try
            {
                var r = await _db.GetArrayDataAsync<ProviderEntity, dynamic>("Provider_Select_ByPK", new { IdProvider = id });
                if (r is null)
                {
                    return (true, null);
                }
                else
                {
                    return (true, r.FirstOrDefault<ProviderEntity?>());
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception Error in GetProviderById");
                return (false, null);
            }
        }
    }
}
