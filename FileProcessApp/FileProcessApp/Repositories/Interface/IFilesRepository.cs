using FileProcessingApp.Models.Entities;

namespace FileProcessingApp.Repositories.Interface
{
    public interface IFilesRepository
    {
        Task<IEnumerable<Files>> GetFilesForUserAsync();
        public Task<long> AddFileAsync(Files file);
    }
}
