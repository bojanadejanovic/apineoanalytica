using NeoAnalytica.AppCore.Entities;
using NeoAnalytica.Application;
using NeoAnalytica.Infrastructure.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NeoAnalytica.Infrastructure.Interfaces
{
    public interface IQuestionService : IGenericRepository<QuestionEntity>
    {
        Task<int> InsertQuestionAsync(QuestionEntity entity);

        Task<QuestionEntity> GetQuestionById(int questionId);
    }
}
