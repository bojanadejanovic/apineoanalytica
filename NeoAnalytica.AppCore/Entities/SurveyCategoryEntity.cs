using System;
using System.Collections.Generic;
using System.Text;

namespace NeoAnalytica.AppCore.Entities
{
    /// <summary>
    /// Represents Survey Category entity 
    /// </summary>
    public class SurveyCategoryEntity
    {
        public int SurveyCategoryID { get; set; }

        public string CategoryName { get; set; }

        public string CategoryDescription { get; set; }
    }
}
