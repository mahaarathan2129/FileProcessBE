using FileProcessingApp.Models.Dto.Request;
using FileProcessingApp.Services.Interface;
using Microsoft.AspNetCore.Mvc;


namespace FileProcessingApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileController : BaseController
    {
        private readonly IFilesService _filesService;

        public FileController(IFilesService _filesService)
        {
            this._filesService = _filesService;

        }

        [HttpGet]
        public async Task<IActionResult> GetMyFiles()
        {
            var result = await _filesService.GetFilesForUserAsync();
            return StatusCode(result.StatusCode,result);
        }


        [HttpPost]
        public async Task<IActionResult> ProcessFileByFilePath(string filePath)
        {

            var result = await _filesService.ProcessFile(filePath);
            return StatusCode(result.StatusCode,result);
        }
    }
}