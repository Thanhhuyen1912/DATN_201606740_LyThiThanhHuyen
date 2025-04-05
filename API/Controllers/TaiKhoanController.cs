using CoreLib.AppDbContext;
using CoreLib.Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaiKhoanController : ControllerBase
    {
        private readonly AppDbContext _context;

        public TaiKhoanController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("/TaiKhoan/Danhsach")]
        public IActionResult GetAll()
        {
            var list = _context.TaiKhoan.ToList();
            if (list == null)
            {
                return NotFound();
            }
            else
                return Ok(new { code = 0, message = "Tải danh sách tài khoản thành công", data = list });

        }

        [HttpGet]
        [Route("/TaiKhoan/Quyen")]
        public IActionResult GetByRole(int role)
        {
            var list = _context.TaiKhoan.ToList().Where(p => p.LoaiTaiKhoan == role);
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
            var list = _context.TaiKhoan.ToList().Where(p => p.MaTaiKhoan == matk);
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
            return Ok();
        }

    }
}

