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
        private string _connectionString;
        private DbConnectionType _dbType;

        public SqlRepository(string connectionString)
        {
            _dbType = DbConnectionType.SQL;
            _connectionString = connectionString;
        }

        public IDbConnection GetOpenConnection()
        {
            return DbConnectionFactory.GetDbConnection(_dbType, _connectionString);
        }

        public abstract Task DeleteAsync(int Id);
        public abstract Task<IEnumerable<TEntity>> GetAllAsync();
        public abstract Task<TEntity> FindAsync(int Id);
        public abstract Task InsertAsync(TEntity entity);
        public abstract Task UpdateAsync(TEntity entityToUpdate);
    }
}
