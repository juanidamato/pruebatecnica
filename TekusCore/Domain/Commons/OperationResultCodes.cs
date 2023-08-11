using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TekusCore.Domain.Commons
{
    public enum OperationResultCodes
    {
        OK = 200,
        CREATED = 201,
        ACCEPTED = 202,
        BAD_REQUEST = 400,
        NOT_AUTHORIZED = 401,
        FORBIDDEN = 403,
        NOT_FOUND = 404,
        DUPLICATE = 409,
        SERVER_ERROR = 500
    }
}
