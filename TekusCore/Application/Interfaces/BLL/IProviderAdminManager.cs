using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TekusCore.Application.Features.Providers.Querys;
using TekusCore.Domain.Commons;
using TekusCore.Domain.Entities;

namespace TekusCore.Application.Interfaces.BLL
{
    public interface IProviderAdminManager
    {
        Task<(OperationStatusModel, List<ProviderEntity>?)> GetProvidersAdmin(GetProvidersAdminQuery request);
    }
}
