using CoreLib.AppDbContext;
using CoreLib.Entity;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SanPhamController : Controller
    {
        private readonly AppDbContext _context;

        public SanPhamController(AppDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        [Route("/SanPham/Danhsach")]
        public IActionResult Index()
        {
            var list = _context.SanPham.ToList();
            if (list == null)
            {
                return NotFound();
            }
            return Ok(new { message = "Lấy danh sách thành công", data = list, code = 0 });
        }
        [HttpPost]
        [Route("/SanPham/ThemMoi")]
        public IActionResult Themmoi(SanPham tk)
        {
            try
            {
                _context.SanPham.Add(tk);
                _context.SaveChanges();
                return Ok(new { code = 0, message = "Thêm SP thành công" });
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpPut]
        [Route("/SanPham/Sua")]
        public IActionResult sua(SanPham tk)
        {
            try
            {
                _context.SanPham.Update(tk);
                _context.SaveChanges();
                return Ok(new { code = 0, message = "Sửa SP thành công" });
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpGet]
        [Route("/SanPham/ChiTiet")]
        public IActionResult Chitiet(int masp)
        {
            try
            {
                var sp = _context.SanPham.FirstOrDefault(p => p.MaSanPham == masp);
                if (sp != null)
                    return Ok(new { code = 0, message = "Lấy chi tiết thành công", data = sp });
                else
                {
                    return BadRequest();
                }

            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
