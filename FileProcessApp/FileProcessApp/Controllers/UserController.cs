using FileProcessingApp.Models.Dto.Request;
using FileProcessingApp.Services.Interface;
using Microsoft.AspNetCore.Mvc;


namespace FileProcessingApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : BaseController
    {
        private readonly IUsersService _usersService;
        public UserController(IUsersService _usersService)
        {
            this._usersService = _usersService;
        }

        [HttpPost]
        public async Task<IActionResult> AddAsync([FromBody] UserRequest request)
        {
            var result = await _usersService.AddUserAsync(request);
            return StatusCode(result.StatusCode, result);
        }
    }
}