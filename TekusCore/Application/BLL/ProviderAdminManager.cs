using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TekusCore.Application.Features.Providers.Querys;
using TekusCore.Application.Interfaces;
using TekusCore.Domain.Commons;
using TekusCore.Domain.Entities;

namespace TekusCore.Application.BLL
{
    public class ProviderAdminManager : IProviderAdminManager
    {
        public async Task<(OperationStatusModel, List<ProviderEntity>)> GetProvidersAdmin(GetProvidersAdminQuery request)
        {
            OperationStatusModel response = new OperationStatusModel();
            List<ProviderEntity> providerList = new List<ProviderEntity>();

            providerList.Add(new ProviderEntity { 
                IdProvider = 1, 
                Name = "prov1", 
                Email = "email1", 
                InternalCode = "123", 
                Phone = "317" });

            providerList.Add(new ProviderEntity
            {
                IdProvider = 2,
                Name = "prov2",
                Email = "email2",
                InternalCode = "456",
                Phone = "320"
            });
            response.code = OperationResultCodes.OK;
            response.message = "Providers found";
            
            return (response, providerList);

         
        }
    }
}
