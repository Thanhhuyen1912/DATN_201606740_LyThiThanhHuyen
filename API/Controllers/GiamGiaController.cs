using CoreLib.AppDbContext;
using CoreLib.Entity;
using Lib_Core.DTO;
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
            if (th.NgayBatDau > th.NgayKetThuc)
            { return BadRequest(); }
            try
            {
                _context.MaGiamGia.Add(th);
                _context.SaveChanges();
                return Ok(new { message = "Thêm thành công", code = 0 });
            }
            catch
            { return BadRequest(); }

        }
        [HttpPost]
        [Route("/GiamGia/TimKiem")]
        public IActionResult TimKiem([FromBody] SearchGiamGia dto)
        {
            var query = _context.MaGiamGia.AsQueryable();

            if (!string.IsNullOrEmpty(dto.MaHienThi))
            {
                query = query.Where(p => p.MaHienThi.Contains(dto.MaHienThi));
            }
            if (!string.IsNullOrEmpty(dto.LoaiGiamGia))
            {
                query = query.Where(p => p.LoaiGiamGia.Contains(dto.LoaiGiamGia));
            }
            if (dto.NgayBatDau.HasValue)
            {
                query = query.Where(p => p.NgayBatDau >= dto.NgayBatDau.Value);
            }
            if (dto.NgayKetThuc.HasValue)
            {
                query = query.Where(p => p.NgayKetThuc <= dto.NgayKetThuc.Value);
            }

            if (dto.TrangThai.HasValue)
            {
                query = query.Where(p => p.TrangThai == dto.TrangThai.Value);
            }

            var result = query.Select(sp => new
            {
                sp.MaHienThi,
                sp.MMaGiamGia,
                sp.TrangThai,
                sp.NgayCapNhat,
                sp.LoaiGiamGia,
                sp.NgayBatDau,
                sp.GiaTri,
                sp.NgayKetThuc,
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