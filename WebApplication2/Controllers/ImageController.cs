using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Threading.Tasks;

namespace WebApplication2.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UploadImageController : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> UploadImage(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("No file uploaded.");

            var uploadDirectory = Path.Combine(Directory.GetCurrentDirectory(), "upload");
            var filePath = Path.Combine(uploadDirectory, file.FileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            // 執行其他處理上傳的圖片的邏輯，例如存儲到資料庫或進行圖片處理等

            return Ok("Image uploaded successfully.");
        }
    }
}
