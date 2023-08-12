using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TekusCore.Domain.Entities
{
    public class ProviderServicesEntity
    {
        public int IdProvider { get; set; }
        public int IdService { get; set; }
        public string Name { get; set; } = string.Empty;
        public double HourlyPrice { get; set; }
        public string IdGeography { get; set; } = string.Empty;
    }
}
