using Dapper;
using FileProcessApp.Common;
using FileProcessingApp.Common.Dapper;
using FileProcessingApp.Models.Dto.Request;
using FileProcessingApp.Models.Entities;
using FileProcessingApp.Repositories.Interface;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.Data.SqlClient;
using System.Data;

namespace FileProcessingApp.Repositories.Implementation
{
    public class UsersRepository : IUsersRepository
    {
        private readonly IDapperContext _dapperContext;

        public UsersRepository(IDapperContext _dapperContext)
        {
            this._dapperContext = _dapperContext;
        }

        public async Task<Users> GetUserByEmailAsync(string email)
        {
            return await _dapperContext.QueryFirstOrDefaultAsync<Users>(
                "SELECT * FROM Users WHERE Email = @Email", new { Email = email });
        }

        public async Task<long> AddUserAsync(Users user)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@FirstName", user.FirstName, DbType.String);
            parameters.Add("@LastName", user.LastName, DbType.String);
            parameters.Add("@Email", user.Email, DbType.String);
            parameters.Add("@PasswordHash", user.PasswordHash, DbType.String);
            parameters.Add("@Role", user.Role, DbType.String);
            parameters.Add("@IsActive", user.IsActive, DbType.Boolean);
            parameters.Add("@CreatedBy", HttpContextHelper.GetClaimsValueByKey("UserId"), DbType.Int64);

            long newUserId = await _dapperContext.QueryFirstOrDefaultAsync<long>(
                "AddUser",               
                parameters       
            );

            return newUserId;
        }
    }
}
