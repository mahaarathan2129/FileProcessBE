using AutoMapper;
using Azure.Core;
using FileProcessApp.Model;
using FileProcessApp.Services.Interface;
using FileProcessingApp.Common.Mappings;
using FileProcessingApp.Models.Dto;
using FileProcessingApp.Models.Dto.Request;
using FileProcessingApp.Models.Dto.Response;
using FileProcessingApp.Models.Entities;
using FileProcessingApp.Repositories.Interface;
using FileProcessingApp.Services.Interface;
using FileProcessingApp.Utils;
using Microsoft.Extensions.Configuration;

namespace FileProcessingApp.Services.Implementation
{
    public class UsersService : IUsersService
    {
        private readonly IConfiguration _configuration;
        private readonly IUsersRepository _usersRepository;
        private readonly IMapper _mapper;
        private readonly ITokenUtil _tokenUtil;
        public UsersService(IUsersRepository _usersRepository,IMapper mapper, ITokenUtil _tokenUtil, IConfiguration _configuration)
        {
            this._usersRepository = _usersRepository;
            _mapper = mapper;
            this._tokenUtil = _tokenUtil;
            this._configuration = _configuration;
            
           
        }
        public async Task<ApiResponse<long>> AddUserAsync(UserRequest request)
        {
            var user = MapperProvider.Mapper.Map<Users>(request);
            long result = await _usersRepository.AddUserAsync(user);
            return ApiResponse<long>.Success(result, "User Created Successfully",201);
        }

        public async Task<ApiResponse<UserResponse>> GetUserByEmailAsync(string email)
        {
            Users result = await _usersRepository.GetUserByEmailAsync(email);
            //return ApiResponse<UserResponse>.Success(MapperProvider.Mapper.Map<UserResponse>(result), "Success");
            return ApiResponse<UserResponse>.Success(_mapper.Map<UserResponse>(result), "Success");
        }

        public async Task<ApiResponse<LoginResponseVM>> LoginAsync(LoginRequestVM request)
        {
            Users? user = await _usersRepository.GetUserByEmailAsync(request.Email);

            if (user == null)
            {
                return ApiResponse<LoginResponseVM>.Error("Invalid email", 401);
            }

            if (user.PasswordHash == request.Password) 
            {
                string token = _tokenUtil.GenerateToken(user);

                LoginResponseVM loginResponse = new LoginResponseVM
                {
                    AccessToken = token,
                    Email = user.Email,
                    Role = user.Role
                };

                return ApiResponse<LoginResponseVM>.Success(loginResponse, "Login successful");
            }
            else
            {
                return ApiResponse<LoginResponseVM>.Error("Invalid password", 401);
            }
        }

    }
}
