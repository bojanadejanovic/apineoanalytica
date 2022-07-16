using Microsoft.AspNetCore.Identity;
using NeoAnalytica.AppCore.Entities;
using NeoAnalytica.AppCore.Models;
using NeoAnalytica.Application;
using NeoAnalytica.Infrastructure.DTOs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NeoAnalytica.Infrastructure
{
    public interface ISurveyService : IGenericRepository<SurveyEntity>
    {
        Task<SurveyEntity> GetSurveyById(int suveyId);

        Task<SurveyEntity> InsertSurveyAsync(SurveyEntity entity);
        Task UpdateSurvey(SurveyEntity survey);
        Task<IEnumerable<SurveyCategoryEntity>> GetAllSurveyCategories();
        //Task AddQuestionsToSurvey(QuestionRequest questionRequest);
        Task<IEnumerable<SurveyEntity>> GetAllSurveys(Pager pager, int UserId);
    }
}
