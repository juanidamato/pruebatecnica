using AutoMapper;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TekusCore.Application.BLL;
using TekusCore.Application.Interfaces.BLL;
using TekusCore.Domain.Commons;
using TekusCore.Domain.Entities;

namespace TekusCore.Application.Features.Providers.Querys
{
    //Request
    public class GetProviderByIdEncryptedQuery : IRequest<GetProviderByIdEncryptedQueryResponse>
    {
        public string IdEncrypted { get; set; } = string.Empty;
    }

    //Request Validator
    public class GetProviderByIdEncryptedQueryValidator : AbstractValidator<GetProviderByIdEncryptedQuery>
    {
        public GetProviderByIdEncryptedQueryValidator()
        {
            RuleFor(x => x.IdEncrypted).Cascade(CascadeMode.Stop)
              .NotEmpty().WithMessage("idEncrypted must be supplied");
        }
    }

    //ViewModel
    public class GetProviderByIdEncryptedQueryViewModel
    {
        public string IdProviderEncrypted { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string InternalCode { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
    }

    //Response
    public class GetProviderByIdEncryptedQueryResponse : OperationResultModel<GetProviderByIdEncryptedQueryViewModel?>
    {

    }

    //Mapper BLL to ViewModel
    public class GetProviderByIdEncryptedQueryMapper : Profile
    {
        public GetProviderByIdEncryptedQueryMapper()
        {
            CreateMap<ProviderEntity, GetProviderByIdEncryptedQueryViewModel>()
                .ForMember(dest => dest.IdProviderEncrypted, opt => opt.MapFrom(map => ReverseHash.Encode(map.IdProvider)))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(map => map.Name))
                .ForMember(dest => dest.InternalCode, opt => opt.MapFrom(map => map.InternalCode))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(map => map.Email))
                .ForMember(dest => dest.Phone, opt => opt.MapFrom(map => map.Phone));
        }
    }

    //Handler
    public class GetProviderByIdEncryptedQueryHandler : IRequestHandler<GetProviderByIdEncryptedQuery, GetProviderByIdEncryptedQueryResponse>
    {
        private readonly IMapper _mapper;
        private readonly IProviderManager _providerManager;

        public GetProviderByIdEncryptedQueryHandler(
            IMapper mapper,
            IProviderManager providerManager)
        {
            _mapper = mapper;
            _providerManager = providerManager;
        }

        public async Task<GetProviderByIdEncryptedQueryResponse> Handle(GetProviderByIdEncryptedQuery request, CancellationToken cancellationToken)
        {
            GetProviderByIdEncryptedQueryResponse response = new GetProviderByIdEncryptedQueryResponse();
            OperationStatusModel operation;
            ProviderEntity? element;
            (operation, element) = await _providerManager.GetProviderByIdEncrypted(request);
            response.code = operation.code;
            response.message = operation.message;
            response.payload = null;
            if (operation.code == OperationResultCodes.OK)
            {
                if (element is not null)
                {
                    response.payload = _mapper.Map<GetProviderByIdEncryptedQueryViewModel?>(element);
                }
            }
            return response;
        }
    }
}

