using NeoAnalytica.AppCore.Entities;
using NeoAnalytica.Infrastructure.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NeoAnalytica.Infrastructure.Interfaces
{
    public interface IQuestionService
    {
        Task<QuestionEntity> InsertQuestionAsync(QuestionEntity entity);

        Task<QuestionEntity> GetQuestionById(int questionId);
    }
}
