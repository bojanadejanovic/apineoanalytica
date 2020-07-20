using System;
using System.Collections.Generic;
using System.Text;

namespace NeoAnalytica.AppCore.Models
{
    public class SurveyModel
    {
        public int SurveyId { get; set; }

        public string SurveyName { get; set; }

        public string Description { get; set; }

        public int UserId { get; set; }

        public int SurveyCategoryId { get; set; }
    }
}
