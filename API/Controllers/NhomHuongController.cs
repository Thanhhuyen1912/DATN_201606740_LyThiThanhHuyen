using CoreLib.AppDbContext;
using CoreLib.Entity;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NhomHuongController : Controller
    {
        private readonly AppDbContext _context;
        public NhomHuongController(AppDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        [Route("/NhomHuong/Danhsach")]
        public IActionResult getAll()
        {
            var list = _context.NhomHuong.ToList();
            if (list == null)
            {
                return NotFound();
            }
            else
                return Ok(new { message = "Lấy danh sách thành công", code = 0, data = list });
        }
        [HttpGet]
        [Route("/NhomHuong/ChiTiet")]
        public IActionResult getDetails(int manh)
        {
            var th = _context.NhomHuong.FirstOrDefault(th => th.MaNhomHuong == manh);
            if (th == null)
            {
                return NotFound();
            }
            else
                return Ok(new { message = "Lấy thông tin thành công", code = 0, data = th });
        }
        [HttpPut]
        [Route("/NhomHuong/sua")]
        public IActionResult Update(NhomHuong th)
        {
            try
            {
                _context.NhomHuong.Update(th);
                _context.SaveChanges();
                return Ok(new { message = "Cập nhật thông tin thành công", code = 0 });
            }
            catch
            { return BadRequest(); }

        }
        [HttpPost]
        [Route("/NhomHuong/them")]
        public IActionResult post(NhomHuong th)
        {
            try
            {
                _context.NhomHuong.Add(th);
                _context.SaveChanges();
                return Ok(new { message = "Thêm thành công", code = 0 });
            }
            catch
            { return BadRequest(); }

        }
        [HttpGet]
        [Route("/NhomHuong/TrangThai")]
        public IActionResult getByTrangthai(bool tt)
        {
            var th = _context.NhomHuong.Where(th => th.TrangThai == tt).ToList();
            if (th == null)
            {
                return NotFound();
            }
            else
                return Ok(new { message = "Lấy danh sách thành công", code = 0, data = th });
        }
    }
}