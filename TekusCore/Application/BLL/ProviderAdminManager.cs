using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TekusCore.Application.Features.Providers.Querys;
using TekusCore.Application.Interfaces.BLL;
using TekusCore.Application.Interfaces.Repositories;
using TekusCore.Domain.Commons;
using TekusCore.Domain.Entities;

namespace TekusCore.Application.BLL
{
    public class ProviderAdminManager : IProviderAdminManager
    {
        private readonly ILogger<ProviderAdminManager> _logger;
        private readonly IProviderRepository _providerRepo;

        public ProviderAdminManager(ILogger<ProviderAdminManager> logger, IProviderRepository providerRepo)
        {
            _logger = logger;
            _providerRepo = providerRepo;
        }


        public async Task<(OperationStatusModel, List<ProviderEntity>?)> GetProvidersAdmin(GetProvidersAdminQuery request)
        {
            OperationStatusModel response = new OperationStatusModel();
            List<ProviderEntity>? providerList = new List<ProviderEntity>();
            bool bolR;
            GetProvidersAdminQueryValidator   validator = new GetProvidersAdminQueryValidator();
            
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
                        response.message = response.message+":";
                        for (int i=0;i<= validationResult.Errors.Count - 1; i++)
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

                (bolR, providerList) = await _providerRepo.GetAllProviders();
                if (!bolR)
                {
                    response.code = OperationResultCodes.SERVER_ERROR;
                    response.message = "Error getting all providers";
                    return (response, null);
                }
                if (providerList is null)
                {
                    response.code = OperationResultCodes.NOT_FOUND;
                    response.message = "Providers not found";
                    return (response, null);
                }
                response.code = OperationResultCodes.OK;
                response.message = "Providers found";
                return (response, providerList);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception GetProvidersAdmin");
                response.code = OperationResultCodes.SERVER_ERROR;
                response.message = "Exception getting providers list";
                return (response, null);
            }
        }
    }
}
