using System;
using System.Collections.Generic;
using System.Text;

namespace NeoAnalytica.Infrastructure.DTOs
{
    public class SurveyRequest
    {
        public string SurveyName { get; set; }
        public string SurveyDescription { get; set; }
        public int? SurveyCategoryID { get; set; }
    }
}
