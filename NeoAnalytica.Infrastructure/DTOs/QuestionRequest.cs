﻿using NeoAnalytica.AppCore.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace NeoAnalytica.Infrastructure.DTOs
{
    public class QuestionRequest
    {
        public string Text { get; set; }

        public int QuestionТypeID { get; set; }

        public bool AnswerOptional { get; set; }
    }
}
