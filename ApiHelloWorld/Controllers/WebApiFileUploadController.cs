using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;

namespace ApiHelloWorld.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WebApiFileUploadController : ControllerBase
    {
        private readonly IWebHostEnvironment environment;

        public WebApiFileUploadController(IWebHostEnvironment environment)
        {
            this.environment = environment;
        }

        [HttpPost]
        public async Task<IActionResult> Post(IEnumerable<IFormFile> files)
        {
            var uploadFolder = Path.Combine(this.environment.WebRootPath, "files");

            foreach (var file in files)
            {
                if (file.Length > 0)
                {
                    var fileName = Path.GetFileName(ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim().ToString());

                    using var fileStream = new FileStream(Path.Combine(uploadFolder, fileName), FileMode.Create);
                    await file.CopyToAsync(fileStream);
                }
            }

            return Ok(new { message = "OK" });
        }
    }
}
