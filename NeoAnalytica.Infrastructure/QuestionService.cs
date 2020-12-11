using Dapper;
using Microsoft.Extensions.Logging;
using NeoAnalytica.AppCore.Entities;
using NeoAnalytica.Application;
using NeoAnalytica.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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
            var sql = "INSERT INTO dbo.Question(QuestionID, QuestionTypeID, QuestionText, AnswerOptional, SurveyID)" +
                "VALUES(@QuestionID, @QuestionTypeID, @QuestionText, @AnswerOptional, @SurveyID); SELECT CAST(SCOPE_IDENTITY() as int)";

            var parameters = new DynamicParameters();
            parameters.Add("@QuestionID", entity.Id, System.Data.DbType.Int32);
            parameters.Add("@QuestionTypeID", entity.QuestionTypeID, System.Data.DbType.Int32);
            parameters.Add("@QuestionText", entity.Text, System.Data.DbType.String);
            parameters.Add("@AnswerOptional", entity.AnswerOptional, System.Data.DbType.Boolean);
            parameters.Add("@SurveyID", entity.SurveyID, System.Data.DbType.Int32);

            var id = await base.DbConnection.ExecuteScalarAsync<int>(sql, parameters);
            return id;
        }

        public async Task<int> InsertQuestionAsync(QuestionEntity entity)
        {
            int questionId = 0;
            using (var conn = base.DbConnection)
            {


                DataTable tvp = new DataTable("Answers");
                tvp.Columns.Add(new DataColumn("Item", typeof(string)));

                foreach(var answer in entity.Answers)
                {
                    tvp.Rows.Add(answer.AnswerText);
                }

                var parameters = new DynamicParameters();
                parameters.Add("@Answers", tvp.AsTableValuedParameter("dbo.StringList"));
                parameters.Add("@Text", entity.Text, DbType.String);
                parameters.Add("@SurveyID", entity.SurveyID, DbType.Int32);
                parameters.Add("@AnswerOptional", entity.AnswerOptional, DbType.Boolean);
                parameters.Add("@QuestionTypeID", entity.QuestionTypeID, DbType.Int32);
                parameters.Add("@QuestionID", entity.Id, DbType.Int32, ParameterDirection.Output);
               

                await conn.QueryAsync<int>(
                    "dbo.AddQuestionsToSurvey",
                    parameters,
                    commandType: CommandType.StoredProcedure);
                questionId = parameters.Get<int>("@QuestionID");
            }

            return questionId;
        }

        public override async Task UpdateAsync(QuestionEntity entityToUpdate)
        {
            using (var conn = base.DbConnection)
            {


                DataTable tvp = new DataTable("Answers");
                tvp.Columns.Add(new DataColumn("ID", typeof(int)));
                tvp.Columns.Add(new DataColumn("Title", typeof(string)));

                foreach (var answer in entityToUpdate.Answers)
                {
                    tvp.Rows.Add(answer.Id, answer.AnswerText);
                }

                var parameters = new DynamicParameters();
                parameters.Add("@QuestionID", entityToUpdate.Id, DbType.Int32);
                parameters.Add("@Text", entityToUpdate.Text, DbType.String);
                parameters.Add("@AnswerOptional", entityToUpdate.AnswerOptional, DbType.Boolean);
                parameters.Add("@AnswersToUpdate", tvp.AsTableValuedParameter("dbo.IdTitleListType"));



                await conn.QueryAsync(
                    "dbo.UpdateQuestion",
                    parameters,
                    commandType: CommandType.StoredProcedure);
            }
        }


    }
}
