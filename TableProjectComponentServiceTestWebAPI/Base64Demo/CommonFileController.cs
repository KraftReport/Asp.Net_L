using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace TableProjectComponentServiceTestWebAPI.Base64Demo
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommonFileController : ControllerBase
    {
        private readonly CommonFileService _fileService;
        public CommonFileController(CommonFileService fileService) 
        {
            _fileService = fileService;
        }

        [HttpPost("{fileName}")]
        public async Task<IActionResult> UploadFile(string fileName, IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("No file uploaded");

            await _fileService.UploadFile(fileName, file);
            return Ok("File uploaded successfully");
        }

        [HttpGet("{fileName}")]
        public async Task<IActionResult> DownloadFile(string fileName)
        {
            var objList = await _fileService.downloadFile(fileName);
            byte[] ba = objList;

            return File(ba, "application/octet-stream",  "test");
        }
/*
        private string GetMimeType(string extension)
        {
            var mimeTypes = new Dictionary<string, string>
    {
        { ".txt", "text/plain" },
        { ".pdf", "application/pdf" },
        { ".doc", "application/vnd.ms-word" },
        { ".docx", "application/vnd.openxmlformats-officedocument.wordprocessingml.document" },
        { ".xls", "application/vnd.ms-excel" },
        { ".xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet" },
        { ".png", "image/png" },
        { ".jpg", "image/jpeg" },
        { ".jpeg", "image/jpeg" },
        { ".gif", "image/gif" },
        { ".csv", "text/csv" }
        // Add more MIME types as needed
    };

            return mimeTypes.ContainsKey(extension) ? mimeTypes[extension] : "application/octet-stream";
        }*/
    }
}
