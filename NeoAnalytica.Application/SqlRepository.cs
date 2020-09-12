using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using static NeoAnalytica.Application.DbConnectionFactory;

namespace NeoAnalytica.Application
{
    /// <summary>
    /// Concrete implementation of a SQL repository
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public abstract class SqlRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        public IDbConnection DbConnection { get; private set; }

        public SqlRepository(IDatabaseConnectionFactory dbConnectionFactory)
        {
            // Now it's the time to pick the right connection string!
            // Enum is used. No magic string!
            this.DbConnection = dbConnectionFactory.GetDbConnection(DatabaseConnectionName.DefaultConnection);
        }


        public abstract Task DeleteAsync(int Id);
        public abstract Task<IEnumerable<TEntity>> GetAllAsync();
        public abstract Task<TEntity> FindAsync(int Id);
        public abstract Task<int> InsertAsync(TEntity entity);
        public abstract Task UpdateAsync(TEntity entityToUpdate);
    }
}
