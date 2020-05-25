using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace NeoAnalytica.Application
{
    public class DbConnectionFactory
    {
        public static IDbConnection GetDbConnection(DbConnectionType dbType, string connectionString)
        {
            IDbConnection connection = null;

            switch(dbType)
            {
                case DbConnectionType.SQL:
                    connection = new SqlConnection(connectionString);
                    break;
                default:
                    connection = null;
                    break;
            }

            connection.Open();
            return connection;
        }

        public enum DbConnectionType
        {
            SQL
        }
    }
}
