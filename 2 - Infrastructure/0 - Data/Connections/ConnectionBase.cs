using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace Data.Connections
{
   
    public class ConnectionBase
    {
        private readonly IConfiguration _configuration;
        public ConnectionBase(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IDbConnection Connection =>
            new SqlConnection(_configuration.GetConnectionString("db_connection"));
    }
    
}

