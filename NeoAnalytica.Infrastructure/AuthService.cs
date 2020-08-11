using Dapper;
using Microsoft.Extensions.Logging;
using NeoAnalytica.AppCore.Models;
using NeoAnalytica.Application;
using NeoAnalytica.Infrastructure.DTOs;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace NeoAnalytica.Infrastructure
{
    public class AuthService : SqlRepository<ApplicationUser>, IAuthService
    {
        private readonly ILogger<AuthService> _logger;
        public AuthService(IDatabaseConnectionFactory dbConnectionFactory)
           : base(dbConnectionFactory)
        {
            
        }

        public AuthService(IDatabaseConnectionFactory dbConnectionFactory,
          ILogger<AuthService> logger) : base(dbConnectionFactory)
        {
            _logger = logger;
        }


        public override async Task DeleteAsync(int Id)
        {
            using (var conn = base.DbConnection)
            {
                var sql = "DELETE FROM ApplicationUser WHERE Id = @Id";
                var parameters = new DynamicParameters();
                parameters.Add("@Id", Id, System.Data.DbType.Int32);
                await conn.QueryFirstOrDefaultAsync<ApplicationUser>(sql, parameters);
            }
        }

        public override async Task<ApplicationUser> FindAsync(int Id)
        {
            using (var conn = base.DbConnection)
            {
                var sql = "SELECT * FROM ApplicationUser WHERE Id = @Id";
                var parameters = new DynamicParameters();
                parameters.Add("@Id", Id, System.Data.DbType.Int32);
                return await conn.QueryFirstOrDefaultAsync<ApplicationUser>(sql, parameters);
            }
        }

        public override async Task<IEnumerable<ApplicationUser>> GetAllAsync()
        {
            using (var conn = base.DbConnection)
            {
                var sql = "SELECT * FROM ApplicationUser";
                return await conn.QueryAsync<ApplicationUser>(sql);
            }
        }

        public async Task<UserModel> GetUserAsync(string username)
        {
            using (var conn = base.DbConnection)
            {
                var sql = "SELECT * FROM ApplicationUser WHERE UserName = @username";
                var parameters = new DynamicParameters();
                parameters.Add("@username", username, System.Data.DbType.String);
                var user = await conn.QueryFirstOrDefaultAsync<ApplicationUser>(sql, parameters);
                if(user != null)
                {
                    return new UserModel() { Email = user.Email, LastLoggedIn = user.LastLoggedIn, UserId = user.Id, UserName = user.UserName };
                }
                return null;
            }
        }

        public override async Task<int> InsertAsync(ApplicationUser entity)
        {
            using (var conn = base.DbConnection)
            {
                var sql = "INSERT INTO ApplicationUser(UserName, Email, PhoneNumber)" + "VALUES(@UserName, @Email, @PhoneNumber); SELECT CAST(SCOPE_IDENTITY() as int";
                var parameters = new DynamicParameters();
                parameters.Add("@UserName", entity.UserName, System.Data.DbType.String);
                parameters.Add("@Email", entity.Email, System.Data.DbType.String);
                parameters.Add("@PhoneNumber", entity.PhoneNumber, System.Data.DbType.String);

                return await conn.ExecuteScalarAsync<int>(sql, parameters);
            }
        }

        public  async Task UpdateAsync(UserModel entityToUpdate)
        {
            using (var conn = base.DbConnection)
            {
                var existingEntity = await FindAsync(entityToUpdate.UserId);

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
                parameters.Add("@Id", entityToUpdate.UserId, DbType.Int32);

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

        public override Task UpdateAsync(ApplicationUser entityToUpdate)
        {
            throw new NotImplementedException();
        }
    }
}
