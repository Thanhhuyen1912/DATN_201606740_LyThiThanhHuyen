using CoreLib.AppDbContext;
using CoreLib.Entity;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class ThuongHieuController : Controller
    {
        private readonly AppDbContext _context;
        public ThuongHieuController(AppDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult getAll()
        {
            var list = _context.ThuongHieu.ToList();
            if (list == null)
            {
                return NotFound();
            }
            else
                return Ok(new { message = "Lấy danh sách thành công", code = 0, data = list });
        }
        [HttpGet]
        public IActionResult getDetails(int math)
        {
            var th = _context.ThuongHieu.FirstOrDefault(th => th.MaThuongHieu == math);
            if (th == null)
            {
                return NotFound();
            }
            else
                return Ok(new { message = "Lấy thông tin thành công", code = 0, data = th });
        }
        [HttpPut]
        public IActionResult Update(ThuongHieu th)
        {
            try
            {
                _context.ThuongHieu.Update(th);
                _context.SaveChanges();
                return Ok(new { message = "Cập nhật thông tin thành công", code = 0 });
            }
            catch
            { return BadRequest(); }

        }
        [HttpPost]
        public IActionResult post(ThuongHieu th)
        {
            try
            {
                _context.ThuongHieu.Add(th);
                _context.SaveChanges();
                return Ok(new { message = "Thêm thương hiệu thành công", code = 0 });
            }
            catch
            { return BadRequest(); }

        }
        [HttpGet]
        public IActionResult getByTrangthai(bool tt)
        {
            var th = _context.ThuongHieu.Where(th => th.TrangThai == tt).ToList();
            if (th == null)
            {
                return NotFound();
            }
            else
                return Ok(new { message = "Lấy danh sách thành công", code = 0, data = th });
        }
    }
}
