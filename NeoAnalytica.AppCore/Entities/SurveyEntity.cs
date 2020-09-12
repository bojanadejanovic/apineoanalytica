using System;
using System.Collections.Generic;
using System.Text;

namespace NeoAnalytica.AppCore.Entities
{
    /// <summary>
    /// Represents Survey entity
    /// </summary>
    public class SurveyEntity
    {
        public int SurveyId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int UserId { get; set; }

        public int? SurveyCategoryId { get; set; }
    }
}
