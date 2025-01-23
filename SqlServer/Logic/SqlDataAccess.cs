using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Dapper;
using Microsoft.Extensions.Logging;

namespace SqlServer
{
    public class SqlDataAccess : ISqlDataAccess
    {
        private readonly ILogger<SqlDataAccess> _logger;
        private readonly IConfiguration _config;

        public SqlDataAccess(ILogger<SqlDataAccess> logger, IConfiguration config)
        {
            _logger = logger;
            _config = config;
        }

        // Executes a query. Return list of type T.
        public List<T> Query<T>(string sql, object parameters = null)
        {
            using (IDbConnection connection = CreateConnection())
            {
                DefaultTypeMap.MatchNamesWithUnderscores = true;
                var output = connection.Query<T>(sql, parameters);
                return output.ToList();
            }
        }

        // Execute parameterized SQL. Return the number of rows affected.
        public int Execute(string sql, object parameters = null)
        {
            using (IDbConnection connection = CreateConnection())
            {
                DefaultTypeMap.MatchNamesWithUnderscores = true;
                var output = connection.Execute(sql, parameters);
                return output;
            }
        }

        // Execute parameterized SQL that selects a single value. Return the first cell returned, as T.
        public T ExecuteScalar<T>(string sql, object parameters = null)
        {
            using (IDbConnection connection = CreateConnection())
            {
                DefaultTypeMap.MatchNamesWithUnderscores = true;
                var output = connection.ExecuteScalar<T>(sql, parameters);
                return output;
            }
        }

        // Create connection to database.
        private IDbConnection CreateConnection()
        {
            return new SqlConnection(_config.GetConnectionString("mssql"));
        }
    }
}
