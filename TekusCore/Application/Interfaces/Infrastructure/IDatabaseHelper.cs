using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TekusCore.Application.Interfaces.Infrastructure
{
    public interface IDatabaseHelper
    {
        public Task<IEnumerable<T>> GetArrayDataAsync<T, U>(string command, U parameters, string currentUser = "");
        public Task DoCommandAsync<U>(string command, U parameters, string currentUser = "");
    }
}
