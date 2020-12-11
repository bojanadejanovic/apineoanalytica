using System;
using System.Collections.Generic;
using System.Text;

namespace NeoAnalytica.AppCore.Entities
{
    /// <summary>
    /// Represents Question entity 
    /// </summary>
    public class QuestionEntity
    {
        public QuestionEntity()
        {
            Answers = new List<AnswerEntity>();
        }
        public int Id { get; set; }

        public string Text { get; set; }

        public int QuestionTypeID { get; set; }

        public bool AnswerOptional { get; set; }

        public int SurveyID { get; set; }

        public List<AnswerEntity> Answers { get; set; }
    }
}
