using API.Service;
using CoreLib.AppDbContext;
using CoreLib.Entity;
using Lib_Core.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;
namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaiKhoanController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly ILocationService _locationService;

        public TaiKhoanController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("/TaiKhoan/Danhsach")]
        public IActionResult GetAll()
        {
            var list = _context.TaiKhoan.Where(p => p.LoaiTaiKhoan == 2 || p.LoaiTaiKhoan == 1).ToList();
            if (list == null)
            {
                return NotFound();
            }
            else
                return Ok(new { code = 0, message = "Tải danh sách tài khoản thành công", data = list });

        }
        [HttpGet]
        [Route("/TaiKhoan/TrangThai")]
        public IActionResult TrangThai(bool trangthai)
        {
            var list = _context.TaiKhoan.ToList().Where(p => p.TrangThai == trangthai);
            if (list == null)
            {
                return NotFound();
            }
            else
                return Ok(new { code = 0, message = "Tải danh sách tài khoản thành công", data = list });

        }
        [HttpGet]
        [Route("/TaiKhoan/Chitiet")]
        public IActionResult Chitiet(int matk)
        {
            var list = _context.TaiKhoan.Where(p => p.MaTaiKhoan == matk).FirstOrDefault();
            if (list == null)
            {
                return NotFound();
            }
            else
                return Ok(new { code = 0, message = "", data = list });

        }
        [HttpPost]
        [Route("/TaiKhoan/ThemMoi")]
        public IActionResult Themmoi(TaiKhoan tk)
        {
            try
            {
                _context.TaiKhoan.Add(tk);
                return Ok(new { code = 0, message = "Thêm thành công" });
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpPut]
        [Route("/TaiKhoan/Sua")]
        public IActionResult Sua([FromBody] TaiKhoan tk)
        {
            try
            {
                _context.TaiKhoan.Update(tk);
                _context.SaveChanges();
                return Ok(new { code = 0, message = "Cập nhật thành công" });
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpGet]
        [Route("/DiaChi/DanhSach")]
        public IActionResult DanhSach(int mtk)
        {
            var list = _context.DiaChi
                .Where(p => p.MaTaiKhoan == mtk)
                .Select(p => new
                {
                    p.MaDiaChi,
                    p.Huyen,
                    p.Xa,
                    p.ThanhPho,
                    p.ChiTiet,
                    p.SoDienThoaiNguoiNhan,
                    p.HoTenNguoiNhan,
                    HoTen = _context.TaiKhoan.Where(tp => tp.MaTaiKhoan == p.MaTaiKhoan).Select(tp => tp.HoTen).FirstOrDefault()
                })
                .ToList();

            return Ok(new { message = "Lấy danh sách thành công", code = 0, data = list });
        }

        [HttpPost]
        [Route("/TaiKhoan/TimKiem")]
        public IActionResult TimKiem([FromBody] SearchTaiKhoan dto)
        {
            var query = _context.TaiKhoan.AsQueryable();

            if (!string.IsNullOrEmpty(dto.TenTaiKhoan))
            {
                query = query.Where(p => p.HoTen.Contains(dto.TenTaiKhoan));
            }

            if (!string.IsNullOrEmpty(dto.SoDienThoai))
            {
                query = query.Where(p => p.SoDienThoai.Contains(dto.SoDienThoai));
            }
            if (dto.LoaiTaiKhoan.HasValue)
            {
                query = query.Where(p => p.LoaiTaiKhoan == dto.LoaiTaiKhoan.Value);
            }

            if (dto.TrangThai.HasValue)
            {
                query = query.Where(p => p.TrangThai == dto.TrangThai.Value);
            }

            var result = query.Where(sp => sp.LoaiTaiKhoan == 1 || sp.LoaiTaiKhoan == 2).Select(sp => new
            {
                sp.HoTen,
                sp.Email,
                sp.TrangThai,
                sp.SoDienThoai,
                sp.LoaiTaiKhoan
            }).ToList();

            return Ok(new
            {
                code = 0,
                message = "Tìm kiếm thành công",
                data = result
            });
        }

        [HttpPost]
        [Route("/DiaChi/TimKiem")]
        public IActionResult TimKiem1([FromBody] SearchDiaChi dto)
        {
            var query = _context.DiaChi.AsQueryable();

            if (!string.IsNullOrEmpty(dto.ThanhPho))
            {
                query = query.Where(p => p.ThanhPho.Contains(dto.ThanhPho));
            }

            if (!string.IsNullOrEmpty(dto.Huyen))
            {
                query = query.Where(p => p.Huyen.Contains(dto.Huyen));
            }

            if (!string.IsNullOrEmpty(dto.Xa))
            {
                query = query.Where(p => p.Xa.Contains(dto.Xa));
            }

            var result = query
                .Join(_context.TaiKhoan,
                      diaChi => diaChi.MaTaiKhoan,
                      taiKhoan => taiKhoan.MaTaiKhoan,
                      (diaChi, taiKhoan) => new
                      {
                          diaChi.ThanhPho,
                          diaChi.Huyen,
                          diaChi.Xa,
                          diaChi.ChiTiet,
                          diaChi.HoTenNguoiNhan,
                          diaChi.SoDienThoaiNguoiNhan,
                          HoTen = taiKhoan.HoTen
                      })
                .ToList();

            return Ok(new
            {
                code = 0,
                message = "Tìm kiếm thành công",
                data = result
            });
        }            



        }
    }

