using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TekusCore.Domain.Entities;

namespace TekusCore.Application.Interfaces.Repositories
{
    public interface IProviderServicesRepository
    {
        public Task<(bool, List<ProviderServicesEntity>?)> GetServicesByProvider(int IdProvider);
       
    }
}
