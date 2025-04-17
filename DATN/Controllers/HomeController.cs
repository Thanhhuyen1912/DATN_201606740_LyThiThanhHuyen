using System;
using System.Diagnostics;
using DATN.Models;
using Microsoft.AspNetCore.Mvc;
using CoreLib.AppDbContext;
using CoreLib.DTO;
using CoreLib.Entity;
using Microsoft.EntityFrameworkCore;
using Lib_Core.DTO;

namespace DATN.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly AppDbContext _context;
        public HomeController(AppDbContext context, ILogger<HomeController> logger)
        {
            _context = context;
            _logger = logger;
        }


        public IActionResult Index()
        {
            ViewBag.User = HttpContext.Session.GetString("User");


            var top10SanPham = _context.SanPham
    .OrderByDescending(sp => sp.MaSanPham)
    .Take(10)
    .Select(sp => new SanPhamDTO
    {
        MaSanPham = sp.MaSanPham,
        TenSanPham = sp.TenSanPham,
        AnhDauTien = (from asp in _context.AnhSanPham
                      join a in _context.Anh on asp.MaAnh equals a.MaAnh
                      where asp.MaSanPham == sp.MaSanPham
                      orderby asp.MaAnh
                      select a.URL).FirstOrDefault(),
        GiaDauTien = _context.ChiTietSanPham.Where(ct=>ct.MaSanPham == sp.MaSanPham)
                        .OrderBy(ct => ct.MaChiTietSP)
                        .Select(ct => ct.Gia)
                        .FirstOrDefault(),
        GiaGiamDauTien = _context.ChiTietSanPham.Where(ct => ct.MaSanPham == sp.MaSanPham)
                        .OrderBy(ct => ct.MaChiTietSP)
                        .Select(ct => ct.GiaGiam)
                        .FirstOrDefault()
    })
    .ToList();

            var spkhuyenmai = _context.SanPham
       .OrderByDescending(sp => sp.MaSanPham)
       .Take(10)
       .Select(sp => new SanPhamDTO
       {
           MaSanPham = sp.MaSanPham,
           TenSanPham = sp.TenSanPham,
           AnhDauTien = (from asp in _context.AnhSanPham
                         join a in _context.Anh on asp.MaAnh equals a.MaAnh
                         where asp.MaSanPham == sp.MaSanPham
                         orderby asp.MaAnh
                         select a.URL).FirstOrDefault(),
           GiaDauTien = _context.ChiTietSanPham.Where(ct => ct.MaSanPham == sp.MaSanPham)
                           .OrderBy(ct => ct.MaChiTietSP)
                           .Select(ct => ct.Gia)
                           .FirstOrDefault(),
           GiaGiamDauTien = _context.ChiTietSanPham.Where(ct => ct.MaSanPham == sp.MaSanPham)
                           .OrderBy(ct => ct.MaChiTietSP)
                           .Select(ct => ct.GiaGiam)
                           .FirstOrDefault()
       })
       .ToList();

            // Lấy danh sách 10 sản phẩm có giá giảm cao nhất
            var top10GiaGiamCaoNhat = spkhuyenmai
                .OrderByDescending(sp => sp.GiaGiamDauTien)  // Sắp xếp theo giá giảm cao nhất
                .Take(10) // Lấy 10 sản phẩm đầu tiên
                .ToList(); // Chuyển đổi thành danh sách






            var top10SanPhamBanChay = _context.ChiTietDonHang
    .GroupBy(ctdh => ctdh.MaChiTietSP)  // Nhóm theo MaSanPham để tính tổng số lượng bán
    .Select(group => new
    {
        MaSanPham = group.Key,
        TongSoLuong = group.Sum(ctdh => ctdh.SoLuong)  // Tính tổng số lượng bán cho mỗi sản phẩm
    })
    .OrderByDescending(x => x.TongSoLuong)  // Sắp xếp theo tổng số lượng bán giảm dần
    .Take(10)  // Lấy 10 sản phẩm có lượt bán cao nhất
    .Join(_context.SanPham,
        x => x.MaSanPham,
        sp => sp.MaSanPham,
        (x, sp) => new SanPhamDTO
        {
            MaSanPham = sp.MaSanPham,
            TenSanPham = sp.TenSanPham,
            AnhDauTien = (from asp in _context.AnhSanPham
                          join a in _context.Anh on asp.MaAnh equals a.MaAnh
                          where asp.MaSanPham == sp.MaSanPham
                          orderby asp.MaAnh
                          select a.URL).FirstOrDefault(),
            GiaDauTien = _context.ChiTietSanPham.Where(ct => ct.MaSanPham == sp.MaSanPham)
                            .OrderBy(ct => ct.MaChiTietSP)
                            .Select(ct => ct.Gia)
                            .FirstOrDefault(),
            GiaGiamDauTien = _context.ChiTietSanPham.Where(ct => ct.MaSanPham == sp.MaSanPham)
                            .OrderBy(ct => ct.MaChiTietSP)
                            .Select(ct => ct.GiaGiam)
                            .FirstOrDefault() 
        })
    .ToList();  // Kết quả là danh sách kiểu SanPhamDTO


            // Tạo đối tượng để truyền vào View
            var danhsach = new DanhSachSanPhamTrangChu
            {
                SanPhamBanChay = top10SanPhamBanChay,
                SanPhamKhuyenMai = top10GiaGiamCaoNhat,
                SanPhamMoi = top10SanPham
            };

            return View(danhsach);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
