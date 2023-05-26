using MyAPI.Models;

namespace MyAPI.Services
{
    public interface IUserService
    {
        Task<bool> CreateUsers(Users user);
        Task<List<Users>> GetUsersList();
        Task<Users> UpdateUsers(Users user);
        Task<bool> DeleteUsers(int key);
    }
}
