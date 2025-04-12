using CoreLib.AppDbContext;
using CoreLib.Entity;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GiamGiaController : Controller
    {
        private readonly AppDbContext _context;
        public GiamGiaController(AppDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        [Route("/GiamGia/Danhsach")]
        public IActionResult getAll()
        {
            var list = _context.MaGiamGia.ToList();
            if (list == null)
            {
                return NotFound();
            }
            else
                return Ok(new { message = "Lấy danh sách thành công", code = 0, data = list });
        }
        [HttpGet]
        [Route("/GiamGia/ChiTiet")]
        public IActionResult getDetails(int math)
        {
            var th = _context.MaGiamGia.FirstOrDefault(th => th.MMaGiamGia == math);
            if (th == null)
            {
                return NotFound();
            }
            else
                return Ok(new { message = "Lấy thông tin thành công", code = 0, data = th });
        }
        [HttpPut]
        [Route("/GiamGia/sua")]
        public IActionResult Update(MaGiamGia th)
        {
            try
            {
                _context.MaGiamGia.Update(th);
                _context.SaveChanges();
                return Ok(new { message = "Cập nhật thông tin thành công", code = 0 });
            }
            catch
            { return BadRequest(); }

        }
        [HttpPost]
        [Route("/GiamGia/them")]
        public IActionResult post(MaGiamGia th)
        {
            try
            {
                _context.MaGiamGia.Add(th);
                _context.SaveChanges();
                return Ok(new { message = "Thêm thành công", code = 0 });
            }
            catch
            { return BadRequest(); }

        }
        [HttpGet]
        [Route("/GiamGia/TrangThai")]
        public IActionResult getByTrangthai(bool tt)
        {
            var th = _context.MaGiamGia.Where(th => th.TrangThai == tt).ToList();
            if (th == null)
            {
                return NotFound();
            }
            else
                return Ok(new { message = "Lấy danh sách thành công", code = 0, data = th });
        }
    }
}