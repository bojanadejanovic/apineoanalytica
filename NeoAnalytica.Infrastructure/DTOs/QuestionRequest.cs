using NeoAnalytica.AppCore.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace NeoAnalytica.Infrastructure.DTOs
{
    public class QuestionRequest
    {
        public int SurveyId { get; set; }

        public List<QuestionEntity> Questions { get; set; }
    }
}
