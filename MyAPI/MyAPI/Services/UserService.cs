using Dapper;
using Microsoft.Extensions.Configuration;
using MyAPI.Models;
using Npgsql;
using System.Configuration;
using System.Data;

namespace MyAPI.Services
{
    public class UserService : IUserService
    {
        private readonly IDbService _dbService;
        private readonly IDbConnection connection;

        public UserService(IDbService dbService, IConfiguration configuration)
        {
            _dbService = dbService;
            using var connection = new NpgsqlConnection(configuration.GetConnectionString("ConnectionDB"));

        }
        public async Task<bool> CreateUsers(Users user)
        {
            var result =
            await _dbService.EditData(
                "INSERT INTO public.\"Users\" (\"UserID\", \"UserName\") VALUES (@UserID, @UserName)",
                user);
            return true;
        }

        public async Task<bool> DeleteUsers(int id)
        {
            
            var procedure = "deleteEntry"; 
            var param = new DynamicParameters();
            param.Add("id_param", id);
            var deleteUser = await _dbService.DeleteUser(param,procedure);
            if(deleteUser == true) {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<List<Users>> GetUsersList()
        {
            var userList = await _dbService.GetAll<Users>("SELECT * FROM public.\"Users\"", new { });
            return userList;
        }

        public async Task<Users> UpdateUsers(Users user)
        {
            var updateEmployee =
            await _dbService.EditData(
                "Update public.\"Users\" SET \"UserName\"=@UserName WHERE \"UserID\"=@UserID",
                user);
            return user;
        }
    }
}
