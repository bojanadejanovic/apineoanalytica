using Dapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NeoAnalytica.AppCore.Models;
using NeoAnalytica.Application;
using NeoAnalytica.Infrastructure.DTOs;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace NeoAnalytica.Infrastructure
{
    public class AuthService : SqlRepository<ApplicationUser>, IAuthService
    {
        private readonly ILogger<AuthService> _logger;

        public AuthService(string connectionString) : base(connectionString) { }

        public AuthService(string connectionString,
            ILogger<AuthService> logger) : base(connectionString)
        {
            _logger = logger;
        }
       
        public override async Task DeleteAsync(int Id)
        {
            using (var conn = GetOpenConnection())
            {
                var sql = "DELETE FROM ApplicationUser WHERE Id = @Id";
                var parameters = new DynamicParameters();
                parameters.Add("@Id", Id, System.Data.DbType.Int32);
                await conn.QueryFirstOrDefaultAsync<ApplicationUser>(sql, parameters);
            }
        }

        public override async Task<ApplicationUser> FindAsync(int Id)
        {
            using (var conn = GetOpenConnection())
            {
                var sql = "SELECT * FROM ApplicationUser WHERE Id = @Id";
                var parameters = new DynamicParameters();
                parameters.Add("@Id", Id, System.Data.DbType.Int32);
                return await conn.QueryFirstOrDefaultAsync<ApplicationUser>(sql, parameters);
            }
        }

        public override async Task<IEnumerable<ApplicationUser>> GetAllAsync()
        {
            using (var conn = GetOpenConnection())
            {
                var sql = "SELECT * FROM ApplicationUser";
                return await conn.QueryAsync<ApplicationUser>(sql);
            }
        }

        public async Task<ApplicationUser> GetUserAsync(string username)
        {
            using (var conn = GetOpenConnection())
            {
                var sql = "SELECT * FROM ApplicationUser WHERE UserName = @username";
                var parameters = new DynamicParameters();
                parameters.Add("@username", username, System.Data.DbType.String);
                return await conn.QueryFirstOrDefaultAsync<ApplicationUser>(sql, parameters);
            }
        }

        public override async Task InsertAsync(ApplicationUser entity)
        {
            using (var conn = GetOpenConnection())
            {
                var sql = "INSERT INTO ApplicationUser(UserName, Email, PhoneNumber)" + "VALUES(@UserName, @Email, @PhoneNumber)";
                var parameters = new DynamicParameters();
                parameters.Add("@UserName", entity.UserName, System.Data.DbType.String);
                parameters.Add("@Email", entity.Email, System.Data.DbType.String);
                parameters.Add("@PhoneNumber", entity.PhoneNumber, System.Data.DbType.String);

                await conn.QueryAsync(sql, parameters);
            }
        }

        public override async Task UpdateAsync(ApplicationUser entityToUpdate)
        {
            using (var conn = GetOpenConnection())
            {
                var existingEntity = await FindAsync(entityToUpdate.Id);

                var sql = "UPDATE ApplicationUser "
                    + "SET ";

                var parameters = new DynamicParameters();
                if (existingEntity.UserName != entityToUpdate.UserName)
                {
                    sql += "UserName=@UserName,";
                    parameters.Add("@UserName", entityToUpdate.UserName, DbType.String);
                }

                if(existingEntity.LastLoggedIn != entityToUpdate.LastLoggedIn)
                {
                    sql += "LastLoggedIn=@LastLoggedIn,";
                    parameters.Add("@LastLoggedIn", entityToUpdate.LastLoggedIn, DbType.DateTime);
                }

                sql = sql.TrimEnd(',');

                sql += " WHERE Id=@Id";
                parameters.Add("@Id", entityToUpdate.Id, DbType.Int32);

                await conn.QueryAsync(sql, parameters);
            }
        }

        public async Task<bool> UserExists(string username)
        {
            var user = await GetUserAsync(username);
            if(user != null)
            {
                return true;
            }

            return false;
        }

        public async Task UpdateLoginInfo(string username)
        {
            var user = await GetUserAsync(username);
            if(user != null)
            {
                user.LastLoggedIn = DateTime.UtcNow;
                await UpdateAsync(user);
            }
        }
    }
}
