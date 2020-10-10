using NeoAnalytica.AppCore.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NeoAnalytica.Infrastructure.Interfaces
{
    public interface IEmailService
    {
        Task SendEmail(Message mesage);
    }
}
