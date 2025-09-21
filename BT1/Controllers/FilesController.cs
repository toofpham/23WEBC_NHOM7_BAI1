//(PhamNhatLam_0306231402_3:00 17/9/2025)
using Microsoft.AspNetCore.Mvc;

namespace BT1.Controllers
{
    public class FilesController : Controller
    {
        private readonly IConfiguration _cfg;

        public FilesController(IConfiguration cfg)
        {
            _cfg = cfg;
        }

        [HttpGet]
        public IActionResult Index()
        {
            // Hiển thị form upload + thông tin cấu hình hiện tại
            var maxMb = _cfg.GetValue<long>("AppConfig:Upload:MaxFileSizeMB", 10);
            ViewBag.MaxMb = maxMb;

            var ips = _cfg.GetSection("AppConfig:Security:BlockedIPs").Get<string[]>()
                      ?? Array.Empty<string>();
            ViewBag.Blocked = string.Join(", ", ips);

            return View();
        }

        [HttpPost]
        [RequestSizeLimit(long.MaxValue)] // cho phép controller tự kiểm lại (FormOptions đã chặn ở pipeline)
        public IActionResult Upload(IFormFile file)
        {
            if (file is null || file.Length == 0)
                return BadRequest("Không có file nào được chọn.");

            long maxMb = _cfg.GetValue<long>("AppConfig:Upload:MaxFileSizeMB", 10);
            long maxBytes = maxMb * 1024L * 1024L;

            if (file.Length > maxBytes)
            {
                return BadRequest($"File vượt quá giới hạn {maxMb} MB (kích thước thực: {file.Length} bytes).");
            }

            return Ok($"Tải lên thành công: {file.FileName} ({file.Length} bytes).");
        }
    }
}
