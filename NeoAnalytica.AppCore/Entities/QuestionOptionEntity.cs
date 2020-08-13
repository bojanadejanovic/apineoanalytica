using System;
using System.Collections.Generic;
using System.Text;

namespace NeoAnalytica.AppCore.Entities
{
    /// <summary>
    /// Represents 
    /// </summary>
    public class QuestionOptionEntity
    {
        public int Id { get; set; }

        public int QuestionID { get; set; }

        public string QuestionOptionValue { get; set; }
    }
}
