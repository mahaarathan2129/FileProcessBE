using AutoMapper;
using FileProcessApp.Model;
using FileProcessApp.Services.Interface;
using FileProcessingApp.Models.Dto.Request;
using FileProcessingApp.Models.Dto.Response;
using FileProcessingApp.Models.Entities;
using FileProcessingApp.Repositories.Interface;
using FileProcessingApp.Services.Implementation;
using FileProcessingApp.Utils;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Moq;

namespace FileServiceAppTest
{
    public class UserServiceTest
    {
        private readonly Mock<IUsersRepository> _mockUsersRepository;
        private readonly TokenUtil _tokenUtil;
        private readonly Mock<IMapper> _mockMapper;
        private readonly Mock<IConfiguration> _mockConfiguration;
        private readonly Mock<ITokenUtil> _mockTokenUtil;
        private readonly Mock<IOptions<JWTSettings>> _mockJwtOptions;
        private readonly UsersService _userService;

        public UserServiceTest()
        {
            _mockMapper = new Mock<IMapper>();
            _mockConfiguration = new Mock<IConfiguration>();
            _mockUsersRepository = new Mock<IUsersRepository>();
            _mockJwtOptions = new Mock<IOptions<JWTSettings>>();
            _mockTokenUtil = new Mock<ITokenUtil>();
            _tokenUtil = new TokenUtil(_mockConfiguration.Object, _mockJwtOptions.Object);
            //_userService = new UsersService(_mockUsersRepository.Object, _tokenUtil, _mockConfiguration.Object);
            _userService = new UsersService(
            _mockUsersRepository.Object,
            _mockMapper.Object,
            _mockTokenUtil.Object,
            _mockConfiguration.Object
        );
        }

        [Fact]
        public async Task LoginAsync_ShouldReturnError_WhenEmailIsInvalid()
        {
            // Arrange
            var loginRequest = new LoginRequestVM { Email = "invalid@example.com", Password = "password123" };
            _mockUsersRepository.Setup(x => x.GetUserByEmailAsync(loginRequest.Email)).ReturnsAsync((Users)null);

            // Act
            var result = await _userService.LoginAsync(loginRequest);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal("Invalid email", result.Message);
            Assert.Equal(401, result.StatusCode);
        }

        [Fact]
        public async Task LoginAsync_ShouldReturnError_WhenPasswordIsInvalid()
        {
            // Arrange
            var loginRequest = new LoginRequestVM { Email = "valid@example.com", Password = "wrongpassword" };
            var user = new Users { Email = "valid@example.com", PasswordHash = "correctpassword", Role = "User", FirstName = "John", Id = 1 };
            _mockUsersRepository.Setup(x => x.GetUserByEmailAsync(loginRequest.Email)).ReturnsAsync(user);

            // Act
            var result = await _userService.LoginAsync(loginRequest);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal("Invalid password", result.Message);
            Assert.Equal(401, result.StatusCode);
        }

        [Fact]
        public async Task LoginAsync_ShouldReturnSuccess_WhenLoginIsSuccessful()
        {
            // Arrange
            var loginRequest = new LoginRequestVM { Email = "valid@example.com", Password = "correctpassword" };
            var user = new Users
            {
                Email = "valid@example.com",
                PasswordHash = "correctpassword",
                Role = "User",
                FirstName = "John",
                Id = 1
            };

            var token = "generated_token";

            // Setup the mock repository to return the user
            _mockUsersRepository.Setup(x => x.GetUserByEmailAsync(loginRequest.Email)).ReturnsAsync(user);

            // Setup the mock token util to return a predefined token
            _mockTokenUtil.Setup(x => x.GenerateToken(It.IsAny<Users>())).Returns(token);

            // Act
            var result = await _userService.LoginAsync(loginRequest);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal("Login successful", result.Message);
            Assert.NotNull(result.Data);
            Assert.Equal("valid@example.com", result.Data.Email);
            Assert.Equal(token, result.Data.AccessToken);
            Assert.Equal("User", result.Data.Role);
        }

        //[Fact]
        //public async Task LoginAsync_ShouldReturnSuccess_WhenLoginIsSuccessful()
        //{
        //    // Arrange
        //    var loginRequest = new LoginRequestVM { Email = "valid@example.com", Password = "correctpassword" };
        //    var user = new Users { Email = "valid@example.com", PasswordHash = "correctpassword", Role = "User", FirstName = "John", Id = 1 };
        //    _mockUsersRepository.Setup(x => x.GetUserByEmailAsync(loginRequest.Email)).ReturnsAsync(user);

        //    var token = "generated_token";
        //    //_tokenUtil.Setup(x => x.GenerateToken(user)).Returns(token);

        //    // Act
        //    var result = await _userService.LoginAsync(loginRequest);

        //    // Assert
        //    Assert.True(result.IsSuccess);
        //    Assert.Equal("Login successful", result.Message);
        //    Assert.Equal("valid@example.com", result.Data.Email);
        //    Assert.Equal(token, result.Data.AccessToken);
        //    Assert.Equal("User", result.Data.Role);
        //}

        [Fact]
        public async Task GetUserByEmailAsync_ShouldReturnSuccess_WhenUserExists()
        {
            // Arrange
            var email = "test@example.com";
            var mockUser = new Users
            {
                Id = 1,
                FirstName = "John",
                LastName = "Doe",
                Email = "test@example.com",
                Role = "User"
            };

            var userResponse = new UserResponse
            {
                Id = 1,
                FirstName = "John",
                LastName = "Doe",
                Email = "test@example.com",
                Role = "User"
            };

            _mockUsersRepository.Setup(x => x.GetUserByEmailAsync(email)).ReturnsAsync(mockUser);
            _mockMapper.Setup(m => m.Map<UserResponse>(mockUser)).Returns(userResponse);

            // Act
            var result = await _userService.GetUserByEmailAsync(email);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal("Success", result.Message);
            Assert.NotNull(result.Data);
            Assert.Equal(userResponse.Email, result.Data.Email);
            Assert.Equal(userResponse.FirstName, result.Data.FirstName);
        }

        //[Fact]
        //public async Task GetUserByEmailAsync_ShouldReturnSuccess_WhenUserExists()
        //{
        //    // Arrange
        //    var email = "test@example.com";
        //    var mockUser = new Users
        //    {
        //        Id = 1,
        //        FirstName = "John",
        //        LastName = "Doe",
        //        Email = "test@example.com",
        //        Role = "User"
        //    };

        //    var userResponse = new UserResponse
        //    {
        //        Id = 1,
        //        FirstName = "John",
        //        LastName = "Doe",
        //        Email = "test@example.com",
        //        Role = "User"
        //    };

        //    // Setup the mock repository to return the mock user when called
        //    _mockUsersRepository.Setup(x => x.GetUserByEmailAsync(email)).ReturnsAsync(mockUser);

        //    // Setup the mapper to map Users to UserResponse
        //    _mockMapper.Setup(m => m.Map<UserResponse>(mockUser)).Returns(userResponse);

        //    // Act
        //    var result = await _userService.GetUserByEmailAsync(email);

        //    // Assert
        //    Assert.True(result.IsSuccess);
        //    Assert.Equal("Success", result.Message);
        //    Assert.NotNull(result.Data);
        //    Assert.Equal(userResponse.Email, result.Data.Email);
        //    Assert.Equal(userResponse.FirstName, result.Data.FirstName);
        //}

    }
}