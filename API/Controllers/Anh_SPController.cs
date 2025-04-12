using CoreLib.AppDbContext;
using CoreLib.Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Anh_SPController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;
        public Anh_SPController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        [HttpGet]
        [Route("/Anh_SP/Chitiet")]
        public IActionResult DanhSach(int masp)
        {
            var list = _context.AnhSanPham
                .Where(a => a.MaSanPham == masp)
                .Select(anh => new
                {
                    anh.MaSanPham,
                    anh.MaAnh,
                    DuongDan = _context.Anh
                        .Where(p => p.MaAnh == anh.MaAnh)
                        .Select(p => p.URL)
                        .FirstOrDefault(),
                    TenAnh = _context.Anh
                        .Where(p => p.MaAnh == anh.MaAnh)
                        .Select(p => p.TenAnh)
                        .FirstOrDefault()
                }).ToList();

            return Ok(list);
        }
        [HttpPost]
        [Route("/Anh_SP/ThemAnh")]
        public async Task<IActionResult> Upload([FromForm] int maSanPham, [FromForm] List<IFormFile> images)
        {
            if (images == null || images.Count == 0)
                return BadRequest("Không có ảnh được chọn.");
            if (_env.WebRootPath == null)
            {
                return StatusCode(500, "Không xác định được đường dẫn gốc (WebRootPath).");
            }

            var folderPath = Path.Combine(_env.WebRootPath, "images", "Products");
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }
            var sp = _context.SanPham.FirstOrDefault(p => p.MaSanPham == maSanPham);
            if (sp == null)
            { return BadRequest(); }
            foreach (var file in images)
            {
                var ext = Path.GetExtension(file.FileName);
                var fileName = $"SP_{sp.MaSanPham}_{Guid.NewGuid()}{ext}";
                var filePath = Path.Combine(folderPath, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                // Lưu vào bảng Anh
                var anh = new Anh
                {
                    TenAnh = fileName,
                    URL = $"/images/Products/{fileName}",
                    NgayCapNhat = DateTime.Now
                };
                _context.Anh.Add(anh);
                await _context.SaveChangesAsync();

                // Lưu vào bảng AnhSanPham
                var asp = new AnhSanPham
                {
                    MaSanPham = maSanPham,
                    MaAnh = anh.MaAnh,
                    NgayCapNhat = DateTime.Now
                };
                _context.AnhSanPham.Add(asp);
            }

            await _context.SaveChangesAsync();
            Console.WriteLine($"maSanPham: {maSanPham}");
            Console.WriteLine($"Số ảnh nhận được: {images?.Count ?? 0}");


            return Ok(new { message = "Tải ảnh thành công." });
        }
    }
}
