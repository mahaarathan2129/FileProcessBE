using FileProcessingApp.Models.Dto.Request;
using FileProcessingApp.Models.Entities;

namespace FileProcessingApp.Repositories.Interface
{
    public interface IUsersRepository
    {
        public Task<Users> GetUserByEmailAsync(string email);
        public Task<long> AddUserAsync(Users user);
    }
}
