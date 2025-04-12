using CoreLib.AppDbContext;
using CoreLib.Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PTThanhToanController : Controller
    {
        private readonly AppDbContext _context;
        public PTThanhToanController(AppDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        [Route("/PhuongThuc/Danhsach")]
        public IActionResult getAll()
        {
            var list = _context.PhuongThucThanhToan.ToList();
            if (list == null)
            {
                return NotFound();
            }
            else
                return Ok(new { message = "Lấy danh sách thành công", code = 0, data = list });
        }
        [HttpGet]
        [Route("/PhuongThuc/ChiTiet")]
        public IActionResult getDetails(int math)
        {
            var th = _context.PhuongThucThanhToan.FirstOrDefault(th => th.MaPhuongThuc == math);
            if (th == null)
            {
                return NotFound();
            }
            else
                return Ok(new { message = "Lấy thông tin thành công", code = 0, data = th });
        }
        [HttpPut]
        [Route("/PhuongThuc/sua")]
        public IActionResult Update(PhuongThucThanhToan th)
        {
            try
            {
                _context.PhuongThucThanhToan.Update(th);
                _context.SaveChanges();
                return Ok(new { message = "Cập nhật thông tin thành công", code = 0 });
            }
            catch
            { return BadRequest(); }

        }
        [HttpPost]
        [Route("/PhuongThuc/them")]
        public IActionResult post(PhuongThucThanhToan th)
        {
            try
            {
                _context.PhuongThucThanhToan.Add(th);
                _context.SaveChanges();
                return Ok(new { message = "Thêm thành công", code = 0 });
            }
            catch
            { return BadRequest(); }

        }
        [HttpGet]
        [Route("/PhuongThuc/TrangThai")]
        public IActionResult getByTrangthai(bool tt)
        {
            var th = _context.PhuongThucThanhToan.Where(th => th.TrangThai == tt).ToList();
            if (th == null)
            {
                return NotFound();
            }
            else
                return Ok(new { message = "Lấy danh sách thành công", code = 0, data = th });
        }
    }
}