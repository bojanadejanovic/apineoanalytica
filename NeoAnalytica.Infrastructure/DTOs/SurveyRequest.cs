using NeoAnalytica.AppCore.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace NeoAnalytica.Infrastructure.DTOs
{
    public class SurveyRequest
    {
        public SurveyRequest()
        {
            //Questions = new List<QuestionRequest>();
        }
        public string SurveyName { get; set; }
        public string SurveyDescription { get; set; }
        public int SurveyCategoryID { get; set; }

        //public List<QuestionRequest> Questions { get; set; }
    }
}
