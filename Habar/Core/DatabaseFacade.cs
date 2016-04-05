using System;
using System.Collections.Generic;
using System.Data;
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
        public DbDataReader ExecuteReader(DbTransaction transaction, string commandText, CommandType commandType, IEnumerable<DbParameter> parameters)
        {
            if (transaction == null)
                throw new ArgumentNullException(nameof(transaction));

            ValidateConnection(transaction.Connection);

            return ExecuteReaderInternal(transaction.Connection, transaction, commandText, commandType, CommandBehavior.Default, parameters);
        }
        private DbDataReader ExecuteReaderInternal(DbConnection connection, DbTransaction transaction, string commandText, CommandType commantType, CommandBehavior commandBehavior, IEnumerable<DbParameter> parameters)
        {
            var command = BuildCommand(connection, transaction, commandText, commantType, parameters);

            if (connection.State != ConnectionState.Open)
                connection.Open();

            return ExecuteWithHandling(commandText, commantType, parameters, () => command.ExecuteReader(commandBehavior));
        }
        private void AttachParameters(DbCommand command, IEnumerable<DbParameter> parameters)
        {
            if (parameters == null)
                return;

            foreach (DbParameter parameter in parameters)
            {
                if (parameter.Direction != ParameterDirection.Output && parameter.Value == null)
                    parameter.Value = DBNull.Value;

                command.Parameters.Add(parameter);
            }
        }
        private DbCommand BuildCommand(DbConnection connection, DbTransaction transaction, string commandText, CommandType commantType, IEnumerable<DbParameter> parameters)
        {
            var command = connection.CreateCommand();
            command.CommandText = commandText;
            command.CommandType = commantType;
            command.CommandTimeout = connection.ConnectionTimeout;

            if (transaction != null)
                command.Transaction = transaction;

            AttachParameters(command, parameters);

            return command;
        }
        private T ExecuteWithHandling<T>(string commandText, CommandType commandType, IEnumerable<DbParameter> parameters, Func<T> command)
        {
            try
            {
                return command();
            }
            catch (DbException)
            {
                throw;
            }
        }
        private void ValidateConnection(DbConnection connection)
        {
            Type connectionType = connection.GetType();

            if (_connectionType != connectionType)
                throw new ArgumentException($"'{connectionType}' is not supported for this provider, expected '{_connectionType}' connection type", nameof(connectionType));
        }
    }
}
