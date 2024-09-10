using FileProcessingApp.Models.Dto;
using FileProcessingApp.Models.Entities;

namespace FileProcessingApp.Services.Interface
{
    public interface IFilesService
    {
        Task<ApiResponse<IEnumerable<Files>>> GetFilesForUserAsync();
        public Task<ApiResponse<string>> ProcessFile(string file);
    }
}
