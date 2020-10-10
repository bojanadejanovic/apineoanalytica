using System;
using System.Collections.Generic;
using System.Text;

namespace NeoAnalytica.AppCore.Models
{
    public class EmailSettings
    {
        public string Server { get; set; }
        public int Port { get; set; }
        public string Security { get; set; }
        public string FromAddress { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string ApiKey { get; set; }
    }
}
