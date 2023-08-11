using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TekusCore.Domain.Commons
{
    public class OperationResultModel<T>
    {
        public OperationResultCodes code { get; set; }
        public string message { get; set; } = string.Empty;
        public T? payload { get; set; }
    }
}
