using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TekusCore.Application.Features.Providers.Querys;
using TekusCore.Application.Features.Services.Querys;
using TekusCore.Application.Interfaces.BLL;
using TekusCore.Application.Interfaces.Repositories;
using TekusCore.Domain.Commons;
using TekusCore.Domain.Entities;

namespace TekusCore.Application.BLL
{
    public class ProviderServicesManager : IProviderServicesManager
    {
        private readonly ILogger<ProviderServicesManager> _logger;
       private readonly IProviderServicesRepository _providerServicesRepo;

        public ProviderServicesManager(
            ILogger<ProviderServicesManager> logger,
            IProviderServicesRepository providerServicesRepo)
        {
            _logger = logger;
            _providerServicesRepo = providerServicesRepo;
        }

        public async Task<(OperationStatusModel, List<ProviderServicesEntity>?)> GetServicesByProviderAdmin(GetServicesByProviderAdminQuery request)
        {
            OperationStatusModel response = new OperationStatusModel();
            List<ProviderServicesEntity>? providerServicesList = new List<ProviderServicesEntity>();
            bool bolR;
            GetServicesByProviderAdminQueryValidator validator = new GetServicesByProviderAdminQueryValidator();
            int decryptedId = 0;

            try
            {
                var validationResult = validator.Validate(request);
                if (!validationResult.IsValid)
                {
                    response.code = OperationResultCodes.BAD_REQUEST;
                    response.message = "Invalid request";
                    if (validationResult.Errors is not null)
                    {
                        //todo create a generic function for this
                        response.message = response.message + ":";
                        for (int i = 0; i <= validationResult.Errors.Count - 1; i++)
                        {
                            response.message = response.message + " ";
                            if (i >= 1)
                            {
                                response.message = response.message + ",";
                            }
                            response.message = response.message + validationResult.Errors.ElementAt(i).ErrorMessage;
                        }
                    }
                    return (response, null);
                }

                //decrypt hash
                try
                {
                    //todo create a generic function for this
                    int[] ids = ReverseHash.Decode(request.IdEncrypted);
                    if (ids is null || ids.Length != 1)
                    {
                        response.code = OperationResultCodes.BAD_REQUEST;
                        response.message = "Invalid IdEncrypted";
                        return (response, null);
                    }
                    decryptedId = ids[0];
                }
                catch (Exception ex)
                {
                    response.code = OperationResultCodes.BAD_REQUEST;
                    response.message = "Invalid IdEncrypted";
                    return (response, null);
                }

                    (bolR, providerServicesList) = await _providerServicesRepo.GetServicesByProvider(decryptedId);
                    if (!bolR)
                    {
                        response.code = OperationResultCodes.SERVER_ERROR;
                        response.message = "Error getting services by provider";
                        return (response, null);
                    }
                    if (providerServicesList is null)
                    {
                        response.code = OperationResultCodes.NOT_FOUND;
                        response.message = "Not services found for supplied provider";
                        return (response, null);
                    }
                    response.code = OperationResultCodes.OK;
                    response.message = "Services found";
                    return (response, providerServicesList);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception GetServicesByProviderAdmin");
                response.code = OperationResultCodes.SERVER_ERROR;
                response.message = "Exception getting services by provider list";
                return (response, null);
            }
        }
    }
}
