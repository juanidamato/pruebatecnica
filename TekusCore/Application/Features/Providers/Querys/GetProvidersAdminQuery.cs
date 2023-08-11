using AutoMapper;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TekusCore.Application.Interfaces;
using TekusCore.Domain.Commons;
using TekusCore.Domain.Entities;

namespace TekusCore.Application.Features.Providers.Querys
{
    //Request
    public class GetProvidersAdminQuery :IRequest<GetProvidersAdminQueryResponse>
    {
        public int Page { get; set; }
        public int RecordsPerPage { get; set; }
        public string SortBy { get; set; } = string.Empty;
        public string SortDirection { get; set; } = string.Empty;
    }

    //Request Validator
    public class GetProvidersAdminQueryValidator : AbstractValidator<GetProvidersAdminQuery>
    {
        public GetProvidersAdminQueryValidator()
        {
           
            //todo create a generic validator for SorteablePaginableAdmin
            //to avoid to repeat this
            RuleFor(x => x.Page)
             .NotEmpty().WithMessage("Page number must be supplied")
             .Must(value => value > 0).WithMessage("Invalid Page number");

            //todo create a generic validator for SorteablePaginableAdmin
            //to avoid to repeat this
            RuleFor(x => x.RecordsPerPage)
             .NotEmpty().WithMessage("Records Per Page number must be supplied")
             .Must(value => value > 0 && value <= 500).WithMessage("Records Per Page shoudl be between 1 and 500");

            //todo create a generic validator for SorteablePaginableAdmin
            //to avoid to repeat this
            RuleFor(x => x.SortBy)
                .NotEmpty().WithMessage("Sort By must be supplied")
                .Must(value => value.ToUpper() == "NAME" || value.ToUpper() == "EMAIL").WithMessage("Invalid SortBy value should be NAME or EMAIL");

            //todo create a generic validator for SorteablePaginableAdmin
            //to avoid to repeat this
            RuleFor(x => x.SortDirection)
             .NotEmpty().WithMessage("Sort direction must be supplied")
             .Must(value => value.ToUpper() == "ASC" || value.ToUpper() == "DESC").WithMessage("Invalid sort direction value should be ASC or DESC");


        }
    }

    //ViewModel
    public class GetProvidersAdminQueryViewModel
    {
        public int IdProvider { get; set; }
        public string Name { get; set; } = string.Empty;
        public string InternalCode { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
    }

    //Response
    public class GetProvidersAdminQueryResponse : OperationResultModel<List<GetProvidersAdminQueryViewModel>>
    {

    }

    //Mapper BLL to ViewModel
    public class GetProvidersAdminQueryMapper : Profile
    {
        public GetProvidersAdminQueryMapper()
        {
            //not changes here
            CreateMap<ProviderEntity, GetProvidersAdminQueryViewModel>()
                .ForMember(dest => dest.IdProvider, opt => opt.MapFrom(map => map.IdProvider))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(map => map.Name))
                .ForMember(dest => dest.InternalCode, opt => opt.MapFrom(map => map.InternalCode))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(map => map.Email))
                .ForMember(dest => dest.Phone, opt => opt.MapFrom(map => map.Phone));
        }
    }

    //Handler
    public class GetProvidersAdminQueryHandler:IRequestHandler<GetProvidersAdminQuery, GetProvidersAdminQueryResponse>
    {
        private readonly IMapper _mapper;
        private readonly IProviderAdminManager _providerManager;

        public GetProvidersAdminQueryHandler(
            IMapper mapper,
            IProviderAdminManager providerManager)
        {
            _mapper=mapper;
            _providerManager = providerManager;
        }

        public async Task<GetProvidersAdminQueryResponse> Handle(GetProvidersAdminQuery request, CancellationToken cancellationToken)
        {
            GetProvidersAdminQueryResponse response = new GetProvidersAdminQueryResponse();
            OperationStatusModel operation;
            List<ProviderEntity> list;
            (operation,list ) = await _providerManager.GetProvidersAdmin(request);
            response.code = operation.code;
            response.message = operation.message;
            response.payload = null;
            if (operation.code == OperationResultCodes.OK)
            {
                response.payload = _mapper.Map<List<GetProvidersAdminQueryViewModel>>(list);
            }

            return response;
        }

    }

}
