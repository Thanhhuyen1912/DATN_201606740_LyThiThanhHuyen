using CoreLib.AppDbContext;
using CoreLib.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace API.Controllers
{
    [ApiController]
    public class ThanhToanController : ControllerBase
    {
        private readonly AppDbContext _context;
        public ThanhToanController(AppDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        [Route("/ThanhToan/Danhsach")]
        public IActionResult getAll()
        {
            var result = _context.ThanhToan.Select(sp => new
            {
                sp.ThoiGian,
                sp.MaDonHang,
                sp.NoiDungChuyenKhoan,
                sp.STKGui,
                sp.SoTienGiaoDich,
                TenTaiKhoan = _context.TaiKhoan.Where(tk => tk.MaTaiKhoan == sp.MaTaiKhoan).Select(tk => tk.HoTen).FirstOrDefault(),
            }).ToList();

            return Ok(new { code = 0, message = "Lấy danh sách thành công" , data = result });
        }

        [HttpGet]
        [Route("/ThanhToan/TimKiem")]
        public IActionResult Timkiem([FromBody] SearchThanhToan dto)
        {
            var query = _context.ThanhToan.AsQueryable();

            if (!string.IsNullOrEmpty(dto.stkgui))
            {
                query = query.Where(p => p.STKGui.Contains(dto.stkgui));
            }
            if (dto.ngaybatdau.HasValue)
            {
                query = query.Where(p => p.ThoiGian >= dto.ngaybatdau.Value);
            }
            if (dto.ngayketthuc.HasValue)
            {
                query = query.Where(p => p.ThoiGian <= dto.ngayketthuc.Value);
            }

            if (dto.madonhang.HasValue)
            {
                query = query.Where(p => p.MaDonHang == dto.madonhang.Value);
            }

            var result = query.Select(sp => new
            {
                sp.ThoiGian,
                sp.MaDonHang,
                sp.NoiDungChuyenKhoan,
                sp.STKGui,
                sp.SoTienGiaoDich,
                TenTaiKhoan = _context.TaiKhoan.Where(tk => tk.MaTaiKhoan == sp.MaTaiKhoan).Select(tk => tk.HoTen).FirstOrDefault(),
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

