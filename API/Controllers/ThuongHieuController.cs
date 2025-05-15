using CoreLib.AppDbContext;
using CoreLib.Entity;
using Lib_Core.DTO;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ThuongHieuController : Controller
    {
        private readonly AppDbContext _context;
        public ThuongHieuController(AppDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        [Route("/ThuongHieu/Danhsach")]
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
        [Route("/ThuongHieu/Danhsach1")]
        public IActionResult getAll1()
        {
            var list = _context.ThuongHieu.Where(p => p.TrangThai == true).ToList();
            if (list == null)
            {
                return NotFound();
            }
            else
                return Ok(new { message = "Lấy danh sách thành công", code = 0, data = list });
        }
        [HttpGet]
        [Route("/ThuongHieu/ChiTiet")]
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
        [Route("/ThuongHieu/sua")]
        public IActionResult Update(ThuongHieu th)
        {
            try
            {
                var existing = _context.ThuongHieu
                    .FirstOrDefault(k => k.TenThuongHieu.Trim().ToLower() == th.TenThuongHieu.Trim().ToLower());

                if (existing != null)
                {
                    return BadRequest(new { message = "Tên kích thước đã tồn tại", code = 1 });
                }
                _context.ThuongHieu.Update(th);
                _context.SaveChanges();
                return Ok(new { message = "Cập nhật thông tin thành công", code = 0 });
            }
            catch
            { return BadRequest(); }

        }
        [HttpPost]
        [Route("/ThuongHieu/them")]
        public IActionResult post(ThuongHieu th)
        {
            try
            {
                var existing = _context.ThuongHieu
                    .FirstOrDefault(k => k.TenThuongHieu.Trim().ToLower() == th.TenThuongHieu.Trim().ToLower());

                if (existing != null)
                {
                    return BadRequest(new { message = "Tên kích thước đã tồn tại", code = 1 });
                }
                _context.ThuongHieu.Add(th);
                _context.SaveChanges();
                return Ok(new { message = "Thêm thương hiệu thành công", code = 0 });
            }
            catch
            { return BadRequest(); }

        }
        [HttpGet]
        [Route("/ThuongHieu/TrangThai")]
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
        [HttpPost]
        [Route("/ThuongHieu/TimKiem")]
        public IActionResult TimKiem([FromBody] SearchThuongHieu dto)
        {
            var query = _context.ThuongHieu.AsQueryable();

            if (!string.IsNullOrEmpty(dto.TenThuongHieu))
            {
                query = query.Where(p => p.TenThuongHieu.Contains(dto.TenThuongHieu));
            }
            if (!string.IsNullOrEmpty(dto.QuocGia))
            {
                query = query.Where(p => p.QuocGia.Contains(dto.QuocGia));
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
                sp.TenThuongHieu,
                sp.MaThuongHieu,
                sp.TrangThai,
                sp.NgayCapNhat,
                sp.QuocGia,
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
