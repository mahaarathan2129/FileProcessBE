using FileProcessingApp.Models.Dto.Request;
using FileProcessingApp.Services.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace FileProcessingApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : BaseController
    {
        private readonly IUsersService _usersService;
        public AuthController(IUsersService _usersService)
        {
            this._usersService = _usersService;
        }

        [HttpPost("Login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginRequestVM request)
        {
            var result = await _usersService.LoginAsync(request);
            return Ok(result);
        }
    }
}
