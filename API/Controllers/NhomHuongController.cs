using CoreLib.AppDbContext;
using CoreLib.Entity;
using Lib_Core.DTO;
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
        [Route("/NhomHuong/Danhsach1")]
        public IActionResult getAll1()
        {
            var list = _context.NhomHuong.Where(p=>p.TrangThai == true).ToList();
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
                var existing = _context.NhomHuong
                   .FirstOrDefault(k => k.TenNhomHuong.Trim().ToLower() == th.TenNhomHuong.Trim().ToLower());

                if (existing != null)
                {
                    return BadRequest(new { message = "Tên nhóm hương đã tồn tại", code = 1 });
                }
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
                var existing = _context.NhomHuong
                    .FirstOrDefault(k => k.TenNhomHuong.Trim().ToLower() == th.TenNhomHuong.Trim().ToLower());

                if (existing != null)
                {
                    return BadRequest(new { message = "Tên nhóm hương đã tồn tại", code = 1 });
                }
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
        [HttpPost]
        [Route("/NhomHuong/TimKiem")]
        public IActionResult TimKiem([FromBody] SearchNhomhuong dto)
        {
            var query = _context.NhomHuong.AsQueryable();

            if (!string.IsNullOrEmpty(dto.TenNhomHuong))
            {
                query = query.Where(p => p.TenNhomHuong.Contains(dto.TenNhomHuong));
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
                sp.TenNhomHuong,
                sp.MaNhomHuong,
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