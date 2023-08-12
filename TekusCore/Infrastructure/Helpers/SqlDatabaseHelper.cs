using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TekusCore.Application.Interfaces.Infrastructure;

namespace TekusCore.Infrastructure.Helpers
{
    public class SqlDatabaseHelper:IDatabaseHelper
    {
        private readonly IConfiguration _config;

        public SqlDatabaseHelper(IConfiguration config)
        {
            _config = config;
        }

        public async Task<IEnumerable<T>> GetArrayDataAsync<T, U>(string command, U parameters, string currentUser = "")
        {
            using (IDbConnection db = new SqlConnection(_config.GetConnectionString("Default")))
            {
                if (!string.IsNullOrWhiteSpace(currentUser))
                {
                    await db.ExecuteAsync("sys.sp_set_session_context", new { key = "CurrentUser", value = currentUser }, commandType: CommandType.StoredProcedure);
                }
                return await db.QueryAsync<T>(command, parameters, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task DoCommandAsync<U>(string command, U parameters, string currentUser = "")
        {
            using (IDbConnection db = new SqlConnection(_config.GetConnectionString("Default")))
            {
                if (!string.IsNullOrWhiteSpace(currentUser))
                {
                    await db.ExecuteAsync("sys.sp_set_session_context", new { key = "CurrentUser", value = currentUser }, commandType: CommandType.StoredProcedure);
                }
                await db.ExecuteAsync(command, parameters, commandType: CommandType.StoredProcedure);
            }
        }

    }
}
