using AutoMapper;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TekusCore.Application.BLL;
using TekusCore.Application.Features.Providers.Querys;
using TekusCore.Application.Interfaces.BLL;
using TekusCore.Domain.Commons;
using TekusCore.Domain.Entities;

namespace TekusCore.Application.Features.Services.Querys
{
    //Request
    public class GetServicesByProviderAdminQuery:IRequest<GetServicesByProviderAdminQueryResponse>
    {
        public string IdEncrypted { get; set; } = string.Empty;
        public int Page { get; set; }
        public int RecordsPerPage { get; set; }
        public string SortBy { get; set; } = string.Empty;
        public string SortDirection { get; set; } = string.Empty;
    }

    //Request Validator
    public class GetServicesByProviderAdminQueryValidator : AbstractValidator<GetServicesByProviderAdminQuery>
    {
        public GetServicesByProviderAdminQueryValidator()
        {
            RuleFor(x => x.IdEncrypted).Cascade(CascadeMode.Stop)
              .NotEmpty().WithMessage("idEncrypted must be supplied");

            //todo create a generic validator for SorteablePaginableAdmin
            //to avoid to repeat this
            RuleFor(x => x.Page).Cascade(CascadeMode.Stop)
             .NotEmpty().WithMessage("Page number must be supplied")
             .Must(value => value > 0).WithMessage("Invalid Page number");

            //todo create a generic validator for SorteablePaginableAdmin
            //to avoid to repeat this
            RuleFor(x => x.RecordsPerPage).Cascade(CascadeMode.Stop)
             .NotEmpty().WithMessage("Records Per Page number must be supplied")
             .Must(value => value > 0 && value <= 500).WithMessage("Records Per Page shoudl be between 1 and 500");

            //todo create a generic validator for SorteablePaginableAdmin
            //to avoid to repeat this
            RuleFor(x => x.SortBy).Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Sort By must be supplied")
                .Must(value => value.ToUpper() == "NAME" || value.ToUpper() == "HOURLYPRICE").WithMessage("Invalid SortBy value should be NAME or HOURLYPRICE");

            //todo create a generic validator for SorteablePaginableAdmin
            //to avoid to repeat this
            RuleFor(x => x.SortDirection).Cascade(CascadeMode.Stop)
             .NotEmpty().WithMessage("Sort direction must be supplied")
             .Must(value => value.ToUpper() == "ASC" || value.ToUpper() == "DESC").WithMessage("Invalid sort direction value should be ASC or DESC");
        }
    }

    //ViewModel
    public class GetServicesByProviderAdminQueryViewModel
    {
        public string IdProviderEncrypted { get; set; } = string.Empty;
        public string IdServiceEncrypted { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public double HourlyPrice { get; set; }
        public string IdGeography { get; set; } = string.Empty;
    }

    //Response
    public class GetServicesByProviderAdminQueryResponse : OperationResultModel<List<GetServicesByProviderAdminQueryViewModel>?>
    {

    }

    //Mapper BLL to ViewModel
    public class GetProvidersAdminQueryMapper : Profile
    {
        public GetProvidersAdminQueryMapper()
        {
            CreateMap<ProviderServicesEntity, GetServicesByProviderAdminQueryViewModel>()
                .ForMember(dest => dest.IdProviderEncrypted, opt => opt.MapFrom(map => ReverseHash.Encode(map.IdProvider)))
                .ForMember(dest => dest.IdServiceEncrypted, opt => opt.MapFrom(map => ReverseHash.Encode(map.IdService)))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(map => map.Name))
                .ForMember(dest => dest.HourlyPrice, opt => opt.MapFrom(map => map.HourlyPrice))
                .ForMember(dest => dest.IdGeography, opt => opt.MapFrom(map => map.IdGeography));
        }
    }


    //Handler
    public class GetServicesByProviderAdminQueryHandler : IRequestHandler<GetServicesByProviderAdminQuery, GetServicesByProviderAdminQueryResponse>
    {
        private readonly IMapper _mapper;
        private readonly IProviderServicesManager _providerServicesManager;

        public GetServicesByProviderAdminQueryHandler(
            IMapper mapper,
            IProviderServicesManager providerServicesManager)
        {
            _mapper = mapper;
            _providerServicesManager = providerServicesManager;
        }

        public async Task<GetServicesByProviderAdminQueryResponse> Handle(GetServicesByProviderAdminQuery request, CancellationToken cancellationToken)
        {
            GetServicesByProviderAdminQueryResponse response = new GetServicesByProviderAdminQueryResponse();
            OperationStatusModel operation;
            List<ProviderServicesEntity>? list;
            (operation, list) = await _providerServicesManager.GetServicesByProviderAdmin(request);
            response.code = operation.code;
            response.message = operation.message;
            response.payload = null;
            if (operation.code == OperationResultCodes.OK)
            {
                if (list is not null)
                {

                    //filter by pages and sort it

                    //todo pending do a generic implementation using reflection for sort
                    //also allow to order descendig
                    //also check pagination boundaries

                    var sublist = list.Select(x => x)
                        .Skip((request.Page - 1) * request.RecordsPerPage)
                        .Take(request.RecordsPerPage);
                    if (!sublist.Any())
                    {
                        response.code = OperationResultCodes.NOT_FOUND;
                        response.message = "Not services provider for page supplied";
                    }
                    else
                    {
                        response.payload = _mapper.Map<List<GetServicesByProviderAdminQueryViewModel>?>(sublist);
                    }

                }
            }

            return response;
        }

    }

}
