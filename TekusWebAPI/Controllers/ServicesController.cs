using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web.Resource;
using TekusCore.Application.Features.Providers.Querys;
using TekusCore.Application.Features.Services.Querys;
using TekusWebAPI.Utils;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TekusWebAPI.Controllers
{

    [ApiController]
    public class ServicesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ServicesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        //[Authorize]
        //[RequiredScope("api.access")]
        [HttpGet]
        [Route("api/provider/{idEncrypted}/service")]
        public async Task<ActionResult> GetServicesByProviderAdmin(
            string idEncrypted,
            int page,
            int recordsperpage,
            string sortby,
            string sortdirection)
        {
            HttpMapperResultUtil mapperResultUtil = new();
            GetServicesByProviderAdminQuery query = new();
            GetServicesByProviderAdminQueryResponse result;
            
            query.IdEncrypted=idEncrypted;
            query.Page = page;
            query.RecordsPerPage = recordsperpage;
            query.SortBy = sortby;
            query.SortDirection = sortdirection;
            result = await _mediator.Send(query);
            return mapperResultUtil.MapToActionResult(result);
        }

    }
}
