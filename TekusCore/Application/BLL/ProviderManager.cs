using FluentValidation;
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
    public class ProviderManager : IProviderManager
    {
        private readonly ILogger<ProviderManager> _logger;
        private readonly IProviderRepository _providerRepo;

        public ProviderManager(ILogger<ProviderManager> logger, IProviderRepository providerRepo)
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

        //todo do another method GetProviderById and called from here
        public async Task<(OperationStatusModel, ProviderEntity?)> GetProviderByIdEncrypted(GetProviderByIdEncryptedQuery request)
        {
            OperationStatusModel response = new OperationStatusModel();
            ProviderEntity? provider;
            bool bolR;
            GetProviderByIdEncryptedQueryValidator  validator = new GetProviderByIdEncryptedQueryValidator();
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
                    int[] ids=  ReverseHash.Decode(request.IdEncrypted);
                    if (ids is null || ids.Length !=1)
                    {
                        response.code = OperationResultCodes.BAD_REQUEST;
                        response.message = "Invalid IdEncrypted";
                        return (response, null);
                    }
                    decryptedId = ids[0];
                }
                catch(Exception ex)
                {
                    response.code = OperationResultCodes.BAD_REQUEST;
                    response.message = "Invalid IdEncrypted";
                    return (response, null);
                }


                (bolR, provider) = await _providerRepo.GetProviderById(decryptedId);
                if (!bolR)
                {
                    response.code = OperationResultCodes.SERVER_ERROR;
                    response.message = "Error getting provider by id";
                    return (response, null);
                }
                if (provider is null)
                {
                    response.code = OperationResultCodes.NOT_FOUND;
                    response.message = "Provider not found";
                    return (response, null);
                }
                response.code = OperationResultCodes.OK;
                response.message = "Provider found";
                return (response, provider);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception GetProviderByIdEncrypted");
                response.code = OperationResultCodes.SERVER_ERROR;
                response.message = "Exception getting provider by IdEncrypted";
                return (response, null);
            }
        }
    
    
    }
}
