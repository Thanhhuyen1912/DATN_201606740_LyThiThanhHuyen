using CoreLib.AppDbContext;
using CoreLib.Entity;
using Lib_Core.DTO;
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
        [Route("/KichThuoc/Danhsach1")]
        public IActionResult getAll1()
        {
            var list = _context.KichThuoc.Where(p=>p.TrangThai == true).ToList();
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
                var existing = _context.KichThuoc
                    .FirstOrDefault(k => k.TenKichThuoc.Trim().ToLower() == th.TenKichThuoc.Trim().ToLower());

                if (existing != null)
                {
                    return BadRequest(new { message = "Tên kích thước đã tồn tại", code = 1 });
                }
                _context.KichThuoc.Update(th);
                _context.SaveChanges();
                return Ok(new { message = "Cập nhật thông tin thành công", code = 0 });
            }
            catch
            { return BadRequest(); }

        }
        [HttpPost]
        [Route("/KichThuoc/them")]
        public IActionResult Post(KichThuoc th)
        {
            try
            {
                var existing = _context.KichThuoc
                    .FirstOrDefault(k => k.TenKichThuoc.Trim().ToLower() == th.TenKichThuoc.Trim().ToLower());

                if (existing != null)
                {
                    return BadRequest(new { message = "Tên kích thước đã tồn tại", code = 1 });
                }

                _context.KichThuoc.Add(th);
                _context.SaveChanges();
                return Ok(new { message = "Thêm thành công", code = 0 });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Lỗi: " + ex.Message, code = -1 });
            }
        }

        [HttpPost]
        [Route("/KichThuoc/TimKiem")]
        public IActionResult TimKiem([FromBody] SearchKichThuoc dto)
        {
            var query = _context.KichThuoc.AsQueryable();

            if (!string.IsNullOrEmpty(dto.TenKichThuoc))
            {
                query = query.Where(p => p.TenKichThuoc.Contains(dto.TenKichThuoc));
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
                sp.TenKichThuoc,
                sp.MaKichThuoc,
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