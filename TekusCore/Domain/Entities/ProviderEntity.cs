using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TekusCore.Domain.Entities
{
    public class ProviderEntity
    {
        public int IdProvider { get; set; }
        public string Name { get; set; } = string.Empty;
        public string InternalCode { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
    }
}
