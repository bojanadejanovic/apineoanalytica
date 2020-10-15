using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace NeoAnalytica.AppCore.Models
{
    public class Pager
    {
        [JsonProperty("page")]
        public int Page { get; set; }

        [JsonProperty("pageSize")]
        public int PageSize { get; set; }

        [JsonIgnore]
        public int Offset { get; set; }

        [JsonIgnore]
        public int Next { get; set; }

        [JsonIgnore]
        public string OrderBy { get; set; }

        public Pager()
        {
            Page = 1;
            PageSize = 10;
        }
        public Pager(int page, int pageSize = 10)
        {
            Page = page < 1 ? 1 : page;
            PageSize = pageSize < 1 ? 10 : pageSize;

            Next = pageSize;
            Offset = (Page - 1) * Next;
        }

    }
}
