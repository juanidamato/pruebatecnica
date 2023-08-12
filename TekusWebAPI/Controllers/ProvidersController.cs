using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web.Resource;
using TekusCore.Application.Features.Providers.Querys;
using TekusCore.Domain.Commons;
using TekusWebAPI.Utils;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TekusWebAPI.Controllers
{
   
    [ApiController]
    public class ProvidersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProvidersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Get providers basic information for admin purposes.
        /// 
        /// </summary>
        /// <param name="page">Current Page</param>
        /// <param name="recordsperpage">Number of records to return</param>
        /// <param name="sortby">Order field</param>
        /// <param name="sortdirection">Sort direction ASC or DESC</param>
        /// <returns></returns>
        //[Authorize]
        //[RequiredScope("api.access")]
        [HttpGet]
        [Route("api/provider")]
        public async  Task<ActionResult> GetProvidersAdmin(
           int page,
            int recordsperpage,
            string sortby,
            string sortdirection)
        {
            HttpMapperResultUtil mapperResultUtil = new();
            GetProvidersAdminQuery query = new();
            GetProvidersAdminQueryResponse result;

            query.Page = page;
            query.RecordsPerPage = recordsperpage;
            query.SortBy = sortby;
            query.SortDirection = sortdirection;
            result = await _mediator.Send(query);
            return mapperResultUtil.MapToActionResult(result);
        }

      

    }
}
