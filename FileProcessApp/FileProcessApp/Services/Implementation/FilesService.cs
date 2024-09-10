using FileProcessApp.Common;
using FileProcessingApp.Models.Dto;
using FileProcessingApp.Models.Entities;
using FileProcessingApp.Repositories.Interface;
using FileProcessingApp.Services.Interface;
using Newtonsoft.Json;
using System.Globalization;
using System.Linq;

namespace FileProcessingApp.Services.Implementation
{
    public class FilesService : IFilesService
    {
        private readonly IFilesRepository _filesRepository;
        private readonly MessageBrokerService _messageService;
        private readonly IConfiguration _configuration;

        public FilesService(IFilesRepository _filesRepository, MessageBrokerService _messageService, IConfiguration _configuration)
        {
            this._filesRepository = _filesRepository;
            this._messageService = _messageService;
            this._configuration = _configuration;
        }

        public async Task<ApiResponse<IEnumerable<Files>>> GetFilesForUserAsync()
        {
            return ApiResponse<IEnumerable<Files>>.Success(await _filesRepository.GetFilesForUserAsync());
        }

        public async Task<ApiResponse<string>> ProcessFile(string filePath)
        {
            if (!File.Exists(filePath))
            {
                return ApiResponse<string>.Error("File does not exist: " + filePath);
            }

            FileInfo fileInfo = new FileInfo(filePath);
            if (fileInfo.Extension.ToLower() != ".csv")
            {
                return ApiResponse<string>.Error("The input file should be a csv: " + filePath);
            }

            using (var reader = new StreamReader(filePath))
            using (var csv = new CsvHelper.CsvReader(reader, CultureInfo.InvariantCulture))
            {
                csv.Read();
                csv.ReadHeader();
                var headerColumns = csv.HeaderRecord;

                if (!headerColumns.SequenceEqual(_configuration["CsvColumns"].ToString().Split(",")))
                {
                    return ApiResponse<string>.Error("CSV columns do not match the expected sequence.");
                }

                var file = new Files
                {
                    FilePath = filePath,
                    FileName = fileInfo.Name,
                    FileSize = fileInfo.Length
                };

                await _filesRepository.AddFileAsync(file);

                _messageService.Produce(file.Id.ToString(), JsonConvert.SerializeObject(file));

                return ApiResponse<string>.Success("File Processed Successfully");
            }

        }
    }
}
