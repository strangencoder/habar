using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public abstract class DatabaseFacade
    {
        private readonly Type _connectionType;
        protected string ConnectionString { get; }

        protected DatabaseFacade(Type connectionType, string connectionString)
        {
            if (connectionString == null)
                throw new ArgumentNullException(nameof(connectionString));

            _connectionType = connectionType;
            ConnectionString = connectionString;
        }


        public abstract DbConnection CreateConnection();
        public abstract DbParameter CreateParameter();
    }
}
