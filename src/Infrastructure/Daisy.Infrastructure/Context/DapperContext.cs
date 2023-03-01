using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Daisy.Infrastructure.Context
{
    public class DapperContext
    {
        private readonly IConfiguration configuration;
        private readonly string? connectionString;
        public DapperContext(IConfiguration Configuration)
        {
            configuration = Configuration;
            connectionString = configuration.GetConnectionString("Daisy");
        }
        public IDbConnection CreateConnection()
            => new SqlConnection(connectionString);
    }
}
