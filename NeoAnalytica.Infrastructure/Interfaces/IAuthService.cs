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
        Task<UserModel> GetUserAsync(string username);
        Task GetAndUpdateUserLoginInfoAsync(string username, DateTime loginTime);
    }
}
