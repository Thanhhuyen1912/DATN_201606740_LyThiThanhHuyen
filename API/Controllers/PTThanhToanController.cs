using CoreLib.AppDbContext;
using CoreLib.Entity;
using Lib_Core.DTO;
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
        [Route("/PhuongThuc/Sua")]
        public IActionResult Update(PhuongThucThanhToan th)
        {
            try
            {
                var existing = _context.PhuongThucThanhToan
                   .FirstOrDefault(k => k.TenPhuongThuc.Trim().ToLower() == th.TenPhuongThuc.Trim().ToLower());

                if (existing != null)
                {
                    return BadRequest(new { message = "Tên phương thức đã tồn tại", code = 1 });
                }
                _context.PhuongThucThanhToan.Update(th);
                _context.SaveChanges();
                return Ok(new { message = "Cập nhật thông tin thành công", code = 0 });
            }
            catch
            { return BadRequest(); }

        }
        [HttpPost]
        [Route("/PhuongThuc/Them")]
        public IActionResult post(PhuongThucThanhToan th)
        {
            try
            {
                var existing = _context.PhuongThucThanhToan
                    .FirstOrDefault(k => k.TenPhuongThuc.Trim().ToLower() == th.TenPhuongThuc.Trim().ToLower());

                if (existing != null)
                {
                    return BadRequest(new { message = "Tên phương thức đã tồn tại", code = 1 });
                }
                _context.PhuongThucThanhToan.Add(th);
                _context.SaveChanges();
                return Ok(new { message = "Thêm thành công", code = 0 });
            }
            catch
            { return BadRequest(); }

        }
        [HttpPost]
        [Route("/PhuongThuc/TimKiem")]
        public IActionResult TimKiem([FromBody] SearchPT dto)
        {
            var query = _context.PhuongThucThanhToan.AsQueryable();

            if (!string.IsNullOrEmpty(dto.TenPhuongThuc))
            {
                query = query.Where(p => p.TenPhuongThuc.Contains(dto.TenPhuongThuc));
            }           
            if (dto.NgayDau.HasValue)
            {
                query = query.Where(p => p.NgayCapNhat >= dto.NgayDau.Value);
            }
            if (dto.NgayCuoi.HasValue)
            {
                query = query.Where(p => p.NgayCapNhat <= dto.NgayCuoi.Value);
            }

            if (dto.TrangThai.HasValue)
            {
                query = query.Where(p => p.TrangThai == dto.TrangThai.Value);
            }

            var result = query.Select(sp => new
            {
                sp.TenPhuongThuc,
                sp.MaPhuongThuc,
                sp.TrangThai,
                sp.NgayCapNhat,
                sp.MoTa
            }).ToList();

            return Ok(new
            {
                code = 0,
                message = "Tìm kiếm thành công",
                data = result
            });
        }
    }
}