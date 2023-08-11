using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TekusCore.Domain.Commons;

namespace TekusWebAPI.Utils
{
    public class HttpMapperResultUtil
    {
        public class GenericObjectResult : ObjectResult
        {
            public GenericObjectResult(int code, object? value) : base(value)
            {
                StatusCode = code;
            }
        }

        public ActionResult MapToActionResult(dynamic result)
        {
            dynamic result2 = new { code = result.code, message = result.message, payload = result.payload };
            switch (result.code)
            {
                case OperationResultCodes.OK:
                    return new GenericObjectResult(200, result2);
                case OperationResultCodes.CREATED:
                    return new GenericObjectResult(201, result2);
                case OperationResultCodes.ACCEPTED:
                    return new GenericObjectResult(202, result2);
                case OperationResultCodes.BAD_REQUEST:
                    return new GenericObjectResult(400, result2);
                case OperationResultCodes.NOT_AUTHORIZED:
                    return new GenericObjectResult(401, result2);
                case OperationResultCodes.FORBIDDEN:
                    return new GenericObjectResult(403, result2);
                case OperationResultCodes.NOT_FOUND:
                    return new GenericObjectResult(404, result2);
                case OperationResultCodes.DUPLICATE:
                    return new GenericObjectResult(409, result2);
                case OperationResultCodes.SERVER_ERROR:
                    return new GenericObjectResult(500, result2);
                default:
                    return new GenericObjectResult(500, result2);
            }
        }
    }
}
