using CoreLib.AppDbContext;
using CoreLib.Entity;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DonHangController : Controller
    {
        private readonly AppDbContext _context;
        public DonHangController(AppDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        [Route("/DonHang/Danhsach")]
        public IActionResult getAll()
        {
            var list = _context.DonHang.ToList();
            if (list == null)
            {
                return NotFound();
            }
            else
                return Ok(new { message = "Lấy danh sách thành công", code = 0, data = list });
        }
        [HttpGet]
        [Route("/DonHang/ChiTiet")]
        public IActionResult getDetails(int math)
        {
            var th = _context.DonHang.FirstOrDefault(th => th.MaDonHang == math);
            if (th == null)
            {
                return NotFound();
            }
            else
                return Ok(new { message = "Lấy thông tin thành công", code = 0, data = th });
        }
        [HttpPut]
        [Route("/DonHang/sua")]
        public IActionResult Update(DonHang th)
        {
            try
            {
                _context.DonHang.Update(th);
                _context.SaveChanges();
                return Ok(new { message = "Cập nhật thông tin thành công", code = 0 });
            }
            catch
            { return BadRequest(); }

        }
        [HttpPost]
        [Route("/DonHang/them")]
        public IActionResult post(DonHang th)
        {
            try
            {
                _context.DonHang.Add(th);
                _context.SaveChanges();
                return Ok(new { message = "Thêm thành công", code = 0 });
            }
            catch
            { return BadRequest(); }

        }
        [HttpGet]
        [Route("/DonHang/TrangThaiTT")]
        public IActionResult getByTrangthai(bool ttthanhtoan)
        {
            var th = _context.DonHang.Where(th => th.TrangThaiThanhToan == ttthanhtoan).ToList();
            if (th == null)
            {
                return NotFound();
            }
            else
                return Ok(new { message = "Lấy danh sách thành công", code = 0, data = th });
        }
        [HttpGet]
        [Route("/DonHang/TrangThaiVC")]
        public IActionResult getByTrangthaiVC(string vc)
        {
            var th = _context.DonHang.Where(th => th.TrangThaiVanChuyen == vc).ToList();
            if (th == null)
            {
                return NotFound();
            }
            else
                return Ok(new { message = "Lấy danh sách thành công", code = 0, data = th });
        }
    }
}