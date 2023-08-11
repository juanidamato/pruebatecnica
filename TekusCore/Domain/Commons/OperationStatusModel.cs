using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TekusCore.Domain.Commons
{
    public class OperationStatusModel
    {
        public OperationResultCodes code { get; set; }
        public string message { get; set; } = string.Empty;
    }
}
