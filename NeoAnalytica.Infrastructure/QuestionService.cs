using Dapper;
using Microsoft.Extensions.Logging;
using NeoAnalytica.AppCore.Entities;
using NeoAnalytica.Application;
using NeoAnalytica.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NeoAnalytica.Infrastructure
{
    public class QuestionService : SqlRepository<QuestionEntity>, IQuestionService
    {
        private readonly ILogger<AuthService> _logger;

        public QuestionService(IDatabaseConnectionFactory dbConnectionFactory)
           : base(dbConnectionFactory)
        {

        }

        public QuestionService(IDatabaseConnectionFactory dbConnectionFactory,
            ILogger<AuthService> logger) : base(dbConnectionFactory)
        {
            _logger = logger;
        }

        public override Task DeleteAsync(int Id)
        {
            throw new NotImplementedException();
        }

        public override async Task<QuestionEntity> FindAsync(int Id)
        {
            var sql = "SELECT * FROM Question WHERE QuestionID = @Id";
            var parameters = new DynamicParameters();
            parameters.Add("@Id", Id, System.Data.DbType.Int32);
            return await base.DbConnection.QueryFirstOrDefaultAsync<QuestionEntity>(sql, parameters);
        }

        public override Task<IEnumerable<QuestionEntity>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<QuestionEntity> GetQuestionById(int questionId)
        {
            return await FindAsync(questionId);
        }

        public override async Task<int> InsertAsync(QuestionEntity entity)
        {
            var sql = "INSERT INTO dbo.Question(QuestionID, QuestionТypeID, QuestionText, AnswerOptional, SurveyID)" +
                "VALUES(@QuestionID, @QuestionТypeID, @QuestionText, @AnswerOptional, @SurveyID); SELECT CAST(SCOPE_IDENTITY() as int)";

            var parameters = new DynamicParameters();
            parameters.Add("@QuestionID", entity.Id, System.Data.DbType.Int32);
            parameters.Add("@QuestionТypeID", entity.QuestionТypeID, System.Data.DbType.Int32);
            parameters.Add("@QuestionText", entity.Text, System.Data.DbType.String);
            parameters.Add("@AnswerOptional", entity.AnswerOptional, System.Data.DbType.Boolean);
            parameters.Add("@SurveyID", entity.SurveyID, System.Data.DbType.Int32);

            var id = await base.DbConnection.ExecuteScalarAsync<int>(sql, parameters);
            return id;
        }

        public async Task<QuestionEntity> InsertQuestionAsync(QuestionEntity entity)
        {
            var sql = "INSERT INTO dbo.Question(QuestionID, QuestionТypeID, QuestionText, AnswerOptional, SurveyID)" +
               "VALUES(@QuestionID, @QuestionТypeID, @QuestionText, @AnswerOptional, @SurveyID); SELECT CAST(SCOPE_IDENTITY() as int)";

            var parameters = new DynamicParameters();
            parameters.Add("@QuestionID", entity.Id, System.Data.DbType.Int32);
            parameters.Add("@QuestionТypeID", entity.QuestionТypeID, System.Data.DbType.Int32);
            parameters.Add("@QuestionText", entity.Text, System.Data.DbType.String);
            parameters.Add("@AnswerOptional", entity.AnswerOptional, System.Data.DbType.Boolean);
            parameters.Add("@SurveyID", entity.SurveyID, System.Data.DbType.Int32);

            var id = await base.DbConnection.ExecuteScalarAsync<int>(sql, parameters);
            entity.Id = id;
            return entity;
        }

        public override Task UpdateAsync(QuestionEntity entityToUpdate)
        {
            throw new NotImplementedException();
        }
    }
}
