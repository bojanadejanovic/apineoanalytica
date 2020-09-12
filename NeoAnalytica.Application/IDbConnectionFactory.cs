using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using static NeoAnalytica.Application.DbConnectionFactory;

namespace NeoAnalytica.Application
{
    public interface IDatabaseConnectionFactory
    {
        IDbConnection GetDbConnection(DatabaseConnectionName name);
    }
}
