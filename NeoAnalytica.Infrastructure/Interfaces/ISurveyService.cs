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
    public interface ISurveyService : IGenericRepository<Survey>
    {
        Task<Survey> GetSurveyById(int suveyId);
        Task<int> CreateNewSurvey(Survey survey);
        Task UpdateSurvey(Survey survey);

        Task<IEnumerable<SurveyCategory>> GetAllSurveyCategories();
    }
}
