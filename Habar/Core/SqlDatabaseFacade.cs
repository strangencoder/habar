using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public sealed class SqlDatabaseFacade : DatabaseFacade
    {
        public SqlDatabaseFacade(string connectionString)
            : base(typeof(SqlConnection), connectionString)
        {
        }

        public override DbConnection CreateConnection()
            => new SqlConnection();

        public override DbParameter CreateParameter()
            => new SqlParameter();
    }
}
