﻿using Dapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NeoAnalytica.AppCore.Entities;
using NeoAnalytica.AppCore.Models;
using NeoAnalytica.Application;
using NeoAnalytica.Infrastructure.DTOs;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace NeoAnalytica.Infrastructure
{
    public class SurveyService : SqlRepository<SurveyEntity>, ISurveyService
    {
        private readonly ILogger<AuthService> _logger;

        public SurveyService(IDatabaseConnectionFactory dbConnectionFactory)
           : base(dbConnectionFactory)
        {

        }

        public SurveyService(IDatabaseConnectionFactory dbConnectionFactory,
            ILogger<AuthService> logger) : base(dbConnectionFactory)
        {
            _logger = logger;
        }

        public override async Task DeleteAsync(int Id)
        {
            var sql = "UPDATE Survey SET IsDeleted=1 WHERE SurveyID = @Id";
            var parameters = new DynamicParameters();
            parameters.Add("@Id", Id, System.Data.DbType.Int32);
            await base.DbConnection.QueryAsync<SurveyEntity>(sql, parameters);
        }

        public override async Task<SurveyEntity> FindAsync(int Id)
        {
            var sql = "SELECT * FROM Survey WHERE SurveyId = @Id";
            var parameters = new DynamicParameters();
            parameters.Add("@Id", Id, System.Data.DbType.Int32);
            return await base.DbConnection.QueryFirstOrDefaultAsync<SurveyEntity>(sql, parameters);
        }

        public override async Task<IEnumerable<SurveyEntity>> GetAllAsync()
        {
            var sql = "SELECT * FROM Survey";
            return await base.DbConnection.QueryAsync<SurveyEntity>(sql);
        }
        public async Task<IEnumerable<SurveyEntity>> GetAllSurveys(Pager pager, int UserId)
        {
            pager.OrderBy = string.IsNullOrEmpty(pager.OrderBy) ? "SurveyID" : pager.OrderBy;
            var sql = ($"select * from [dbo].[Survey] where UserID = @UserID order by {pager.OrderBy} OFFSET @Offset ROWS FETCH NEXT @Next ROWS ONLY");
            var parameters = new DynamicParameters();
            parameters.Add("@UserID", UserId, DbType.Int32);
            parameters.Add("@Offset", pager.Offset, DbType.Int32);
            parameters.Add("@Next", pager.Next, DbType.Int32);
            var results = await base.DbConnection.QueryAsync<SurveyEntity>(sql, parameters);
            return results;
        }

        public async Task<IEnumerable<SurveyCategoryEntity>> GetAllSurveyCategories()
        {
            var sql = "SELECT SurveyCategoryID, CategoryName, CategoryDescription  FROM SurveyCategory";
            return await base.DbConnection.QueryAsync<SurveyCategoryEntity>(sql);
        }

        public async Task<SurveyEntity> InsertSurveyAsync(SurveyEntity entity)
        {
            var sql = "INSERT INTO dbo.Survey(Name, Description, UserID, SurveyCategoryID)" + "VALUES(@Name, @Description, @UserID, @SurveyCategoryID); SELECT CAST(SCOPE_IDENTITY() as int)";
            var parameters = new DynamicParameters();
            parameters.Add("@Name", entity.Name, System.Data.DbType.String);
            parameters.Add("@Description", entity.Description, System.Data.DbType.String);
            parameters.Add("@UserID", entity.UserId, System.Data.DbType.Int32);
            parameters.Add("@SurveyCategoryID", entity.SurveyCategoryId, System.Data.DbType.Int32);

            var id = await base.DbConnection.ExecuteScalarAsync<int>(sql, parameters);
            entity.SurveyId = id;
            return entity;
        }

        public override async Task UpdateAsync(SurveyEntity entityToUpdate)
        {
            var existingEntity = await FindAsync(entityToUpdate.UserId);

            var sql = "UPDATE Survey "
                + "SET ";

            var parameters = new DynamicParameters();
            if (existingEntity.Name != entityToUpdate.Name)
            {
                sql += "Name=@Name,";
                parameters.Add("@Name", entityToUpdate.Name, DbType.String);
            }

            if (existingEntity.Description != entityToUpdate.Description)
            {
                sql += "Description=@Description,";
                parameters.Add("@Description", entityToUpdate.Description, DbType.String);
            }

            if (existingEntity.UserId != entityToUpdate.UserId)
            {
                sql += "UserID=@UserID,";
                parameters.Add("@UserID", entityToUpdate.UserId, DbType.Int32);
            }

            if (existingEntity.SurveyCategoryId != entityToUpdate.SurveyCategoryId)
            {
                sql += "SurveyCategoryID=@SurveyCategoryID,";
                parameters.Add("@SurveyCategoryID", entityToUpdate.SurveyCategoryId, DbType.Int32);
            }

            sql = sql.TrimEnd(',');

            sql += " WHERE Id=@Id";
            parameters.Add("@Id", entityToUpdate.SurveyId, DbType.Int32);

            await base.DbConnection.QueryAsync(sql, parameters);
        }


        public async Task<SurveyEntity> GetSurveyById(int suveyId)
        {
            return await FindAsync(suveyId);
        }

        public async Task UpdateSurvey(SurveyEntity survey)
        {
            await UpdateAsync(survey);
        }

        public override async Task<int> InsertAsync(SurveyEntity entity)
        {
            var sql = "INSERT INTO Survey(Name, Description, UserID, SurveyCategoryID)" + "VALUES(@Name, @Description, @UserID, @SurveyCategoryID); SELECT CAST(SCOPE_IDENTITY() as int)";
            var parameters = new DynamicParameters();
            parameters.Add("@Name", entity.Name, System.Data.DbType.String);
            parameters.Add("@Description", entity.Description, System.Data.DbType.String);
            parameters.Add("@UserID", entity.UserId, System.Data.DbType.Int32);
            parameters.Add("@SurveyCategoryID", entity.SurveyCategoryId, System.Data.DbType.Int32);

            var id = await base.DbConnection.ExecuteScalarAsync<int>(sql, parameters);
            return id;

        }
    }
}
