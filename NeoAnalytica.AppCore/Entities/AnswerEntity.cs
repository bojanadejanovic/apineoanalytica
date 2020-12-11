using System;
using System.Collections.Generic;
using System.Text;

namespace NeoAnalytica.AppCore.Entities
{
    /// <summary>
    /// Represnets Answer entity
    /// </summary>
    public class AnswerEntity
    {
        public int Id { get; set; }
        public int QuestionID { get; set; }
        public string AnswerText { get; set; }
    }
}
