using Microsoft.AspNetCore.Identity;
using NeoAnalytica.AppCore.Models;
using NeoAnalytica.Application;
using NeoAnalytica.Infrastructure.DTOs;
using System;
using System.Threading.Tasks;

namespace NeoAnalytica.Infrastructure
{
    public interface IAuthService : IGenericRepository<ApplicationUser>
    {
        Task<bool> UserExists(string username);
        Task<ApplicationUser> GetUserAsync(string username);

        Task UpdateLoginInfo(string user);
    }
}
