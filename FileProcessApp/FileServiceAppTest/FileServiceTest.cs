using FileProcessApp.Common;
using FileProcessingApp.Common.Dapper;
using FileProcessingApp.Models.Dto;
using FileProcessingApp.Models.Entities;
using FileProcessingApp.Repositories.Interface;
using FileProcessingApp.Services.Implementation;
using Microsoft.Extensions.Configuration;
using Moq;

namespace FileServiceAppTest
{
    public class FileServiceTest
    {

        private readonly Mock<IFilesRepository> _mockFilesRepository;
        private readonly Mock<IConfiguration> _mockConfiguration;
        private readonly MessageBrokerService _messageBrokerService;
        private readonly FilesService _filesService;

        public FileServiceTest()
        {
            _mockFilesRepository = new Mock<IFilesRepository>();
            _mockConfiguration = new Mock<IConfiguration>();

            var mockConfiguration = new Mock<IConfiguration>();

            _messageBrokerService = new MessageBrokerService(mockConfiguration.Object);

            _filesService = new FilesService(_mockFilesRepository.Object, _messageBrokerService, _mockConfiguration.Object);
        }


        [Fact]
        public async Task GetFilesForUserAsync_ShouldReturnSuccess_WhenFilesAreRetrieved()
        {
            // Arrange
            var files = new List<Files>
        {
            new Files { Id = 1, FileName = "File1" },
            new Files { Id = 2, FileName = "File2" }
        };

            _mockFilesRepository
                .Setup(repo => repo.GetFilesForUserAsync())
                .ReturnsAsync(files);

            // Act
            var result = await _filesService.GetFilesForUserAsync();

            // Assert
            Assert.NotNull(result);
            Assert.True(result.IsSuccess);
            Assert.Equal(files, result.Data);
        }

        [Fact]
        public async Task GetFilesForUserAsync_ShouldReturnEmpty_WhenNoRecordsFound()
        {

            // Arrange
            var files = new List<Files>();

            _mockFilesRepository
                .Setup(repo => repo.GetFilesForUserAsync())
                .ReturnsAsync(files);

            // Act
            var result = await _filesService.GetFilesForUserAsync();

            // Assert
            Assert.NotNull(result);
            Assert.NotNull(result.Data);
            Assert.True(result.Data.Count() == 0);
            Assert.Equal(files, result.Data);
        }
    }
}