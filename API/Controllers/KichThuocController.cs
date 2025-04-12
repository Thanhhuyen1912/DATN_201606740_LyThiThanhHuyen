using CoreLib.AppDbContext;
using CoreLib.Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KichThuocController : Controller
    {
        private readonly AppDbContext _context;
        public KichThuocController(AppDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        [Route("/KichThuoc/Danhsach")]
        public IActionResult getAll()
        {
            var list = _context.KichThuoc.ToList();
            if (list == null)
            {
                return NotFound();
            }
            else
                return Ok(new { message = "Lấy danh sách thành công", code = 0, data = list });
        }
        [HttpGet]
        [Route("/KichThuoc/ChiTiet")]
        public IActionResult getDetails(int math)
        {
            var th = _context.KichThuoc.FirstOrDefault(th => th.MaKichThuoc == math);
            if (th == null)
            {
                return NotFound();
            }
            else
                return Ok(new { message = "Lấy thông tin thành công", code = 0, data = th });
        }
        [HttpPut]
        [Route("/KichThuoc/sua")]
        public IActionResult Update(KichThuoc th)
        {
            try
            {
                _context.KichThuoc.Update(th);
                _context.SaveChanges();
                return Ok(new { message = "Cập nhật thông tin thành công", code = 0 });
            }
            catch
            { return BadRequest(); }

        }
        [HttpPost]
        [Route("/KichThuoc/them")]
        public IActionResult post(KichThuoc th)
        {
            try
            {
                _context.KichThuoc.Add(th);
                _context.SaveChanges();
                return Ok(new { message = "Thêm thành công", code = 0 });
            }
            catch
            { return BadRequest(); }

        }
        [HttpGet]
        [Route("/KichThuoc/TrangThai")]
        public IActionResult getByTrangthai(bool tt)
        {
            var th = _context.KichThuoc.Where(th => th.TrangThai == tt).ToList();
            if (th == null)
            {
                return NotFound();
            }
            else
                return Ok(new { message = "Lấy danh sách thành công", code = 0, data = th });
        }
    }
}