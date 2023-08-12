using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TekusCore.Domain.Entities;

namespace TekusCore.Application.Interfaces.Repositories
{
    public interface IProviderRepository
    {
        public Task<(bool, List<ProviderEntity>?)> GetAllProviders();
    }
}
