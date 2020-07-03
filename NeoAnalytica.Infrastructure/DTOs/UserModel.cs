using System;
using System.Collections.Generic;
using System.Text;

namespace NeoAnalytica.Infrastructure.DTOs
{
    public class UserModel
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public DateTime? LastLoggedIn { get; set; }
    }
}
