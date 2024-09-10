using FileProcessingApp.Models.Entities;

namespace FileProcessApp.Services.Interface
{
    public interface ITokenUtil
    {
        string GenerateToken(Users user);
    }
}
