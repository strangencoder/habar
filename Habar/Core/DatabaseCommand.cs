using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public class DatabaseCommand
    {
        private readonly string _commandText;
        private readonly DatabaseFacade _databaseFacade;
        private readonly List<DbParameter> _parameters;
        private CommandType _commandType;

        public DatabaseCommand(DatabaseFacade databaseFacade, string commandText)
        {
            _databaseFacade = databaseFacade;
            _commandText = commandText;
            _commandType = CommandType.Text;
            _parameters = new List<DbParameter>();
        }

        public DatabaseCommand AsStoredProcedure()
        {
            _commandType = CommandType.StoredProcedure;

            return this;
        }

        public DatabaseCommand AddParameter(DbParameter parameter)
        {
            if (parameter == null)
                throw new ArgumentNullException(nameof(parameter));

            _parameters.Add(parameter);

            return this;
        }

        public DatabaseCommand AddInputParameter(string parameterName, object parameterValue)
        {
            var parameter = _databaseFacade.CreateParameter();
            parameter.Direction = ParameterDirection.Input;
            parameter.ParameterName = parameterName;
            parameter.Value = parameterValue ?? DBNull.Value;

            _parameters.Add(parameter);
            return this;
        }
        public DatabaseCommand AddInputParameter(string parameterName, DbType dbType, int size, object parameterValue)
        {
            var parameter = _databaseFacade.CreateParameter();
            parameter.Direction = ParameterDirection.Input;
            parameter.DbType = dbType;
            parameter.Size = size;
            parameter.Value = parameterValue ?? DBNull.Value;

            _parameters.Add(parameter);
            return this;
        }
        public DatabaseCommand AddInputOutputParameter(string parameterName, DbType dbType, object parameterValue)
        {
            var parameter = _databaseFacade.CreateParameter();
            parameter.Direction = ParameterDirection.InputOutput;
            parameter.DbType = dbType;
            parameter.ParameterName = parameterName;
            parameter.Value = parameterValue ?? DBNull.Value;

            _parameters.Add(parameter);
            return this;
        }
        public DatabaseCommand AddInputOutputParameter(string parameterName, DbType dbType, int size, object parameterValue)
        {
            var parameter = _databaseFacade.CreateParameter();
            parameter.Direction = ParameterDirection.InputOutput;
            parameter.DbType = dbType;
            parameter.ParameterName = parameterName;
            parameter.Size = size;
            parameter.Value = parameterValue ?? DBNull.Value;

            _parameters.Add(parameter);
            return this;
        }
        public DatabaseCommand AddOutputParameter(string parameterName, DbType dbType)
        {
            var parameter = _databaseFacade.CreateParameter();
            parameter.Direction = ParameterDirection.InputOutput;
            parameter.DbType = dbType;
            parameter.ParameterName = parameterName;

            _parameters.Add(parameter);
            return this;
        }
        public DatabaseCommand AddOutputParameter(string parameterName, DbType dbType, int size)
        {
            var parameter = _databaseFacade.CreateParameter();
            parameter.Direction = ParameterDirection.InputOutput;
            parameter.DbType = dbType;
            parameter.ParameterName = parameterName;
            parameter.Size = size;

            _parameters.Add(parameter);
            return this;
        }
    }
}
