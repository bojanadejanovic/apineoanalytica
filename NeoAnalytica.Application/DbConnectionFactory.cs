using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace NeoAnalytica.Application
{
    public class DbConnectionFactory : IDatabaseConnectionFactory
    {
        private readonly IDictionary<DatabaseConnectionName, string> _connectionDict;

        public DbConnectionFactory(IDictionary<DatabaseConnectionName, string> connectionDict)
        {
            _connectionDict = connectionDict;
        }

        public IDbConnection GetDbConnection(DatabaseConnectionName connectionName)
        {
            string connectionString = null;
            if (_connectionDict.TryGetValue(connectionName, out connectionString))
            {
                SqlConnection connection =  new SqlConnection(connectionString);
                connection.Open();
                return connection;
            }

            throw new ArgumentNullException();
        }


        public enum DbConnectionType
        {
            SQL
        }
    }
}
