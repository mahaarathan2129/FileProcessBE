using FileProcessingApp.Models.Dto;
using FileProcessingApp.Models.Dto.Request;
using FileProcessingApp.Models.Dto.Response;
using FileProcessingApp.Models.Entities;

namespace FileProcessingApp.Services.Interface
{
    public interface IUsersService
    {
        Task<ApiResponse<LoginResponseVM>> LoginAsync(LoginRequestVM request);
        Task<ApiResponse<UserResponse>> GetUserByEmailAsync(string email);
        Task<ApiResponse<long>> AddUserAsync(UserRequest request);
    }
}
