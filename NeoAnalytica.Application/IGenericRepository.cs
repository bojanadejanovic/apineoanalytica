﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace NeoAnalytica.Application
{
    public interface IGenericRepository<TEntity>
    {
        IDbConnection GetOpenConnection();
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<TEntity> FindAsync(int Id);
        Task InsertAsync(TEntity entity);
        Task DeleteAsync(int Id);

        Task UpdateAsync(TEntity entityToUpdate);
    }
}
