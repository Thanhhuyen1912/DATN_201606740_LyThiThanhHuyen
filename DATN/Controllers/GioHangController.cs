using CoreLib.AppDbContext;
using CoreLib.Entity;
using CoreLib.DTO;
using Microsoft.AspNetCore.Mvc;
using SANPHAM.Authorize;
using API.Service;

namespace DATN.Controllers
{
    public class GioHangController : Controller
    {
        private readonly AppDbContext _context;
        private readonly ILocationService _locationService;
        public GioHangController(AppDbContext context, ILocationService locationService)
        {
            _context = context;
            _locationService = locationService;
        }
        [RequiredLogin]
        public IActionResult Index()
        {
            string? ma = HttpContext.Session.GetString("MaTaiKhoan");
            if (ma == null)
            {
                return RedirectToAction("DangNhap", "TaiKhoan");
            }
            var giohang = _context.GioHang.Where(gh => gh.MaTaiKhoan == int.Parse(ma)).FirstOrDefault();
            HttpContext.Session.SetString("MaGioHang", giohang.MaGioHang.ToString());

            var listct = _context.ChiTietGioHang.Where(ct => ct.MaGioHang == giohang.MaGioHang).ToList();
            var list = new List<Hienthigiohang>();

            foreach( var ct in listct)
            {
                var ctsp = _context.ChiTietSanPham.Where(sp => sp.MaChiTietSP == ct.MaChiTietSanPham).FirstOrDefault();
                var sp = _context.SanPham.Where(sp => sp.MaSanPham == ctsp.MaSanPham).FirstOrDefault();
                var anhsp = _context.AnhSanPham.Where(asp => asp.MaSanPham == sp.MaSanPham).FirstOrDefault();
                var anh = _context.Anh.Where(a => a.MaAnh == anhsp.MaAnh).FirstOrDefault();

                list.Add(new Hienthigiohang
                {
                    chitietgiohang = ct,
                    chitietsanpham = ctsp,
                    tensanpham = sp.TenSanPham,
                    anh = anh
                });
            }

            ViewBag.Tongtien = _context.ChiTietGioHang.Where(ctg=>ctg.MaGioHang == giohang.MaGioHang).Sum(ctg => ctg.TongTien);
            return View(list);
        }
        [RequiredLogin]
        public IActionResult Themgiohang(int mactsp, int soluong)
        {
            try
            {
                string? ma = HttpContext.Session.GetString("MaTaiKhoan");
                if (ma == null)
                {
                    return RedirectToAction("DangNhap", "TaiKhoan");
                }

                var ctsp = _context.ChiTietSanPham.Where(gh => gh.MaChiTietSP == mactsp).FirstOrDefault();
                decimal gia = Convert.ToDecimal(ctsp.Gia);
                decimal giaGiam = Convert.ToDecimal(ctsp.GiaGiam);

                int? magiohang = int.Parse(HttpContext.Session.GetString("MaGioHang")); // Sử dụng GetInt32 thay vì GetString
                if (!magiohang.HasValue || magiohang == 0)
                {
                    return RedirectToAction("DangNhap", "TaiKhoan");
                }

                var giohang = _context.GioHang.Where(gh => gh.MaTaiKhoan == magiohang.Value).FirstOrDefault();

                // Kiểm tra xem chi tiết sản phẩm đã có trong giỏ hàng chưa
                var existingItem = _context.ChiTietGioHang
                    .Where(ctgh => ctgh.MaGioHang == magiohang.Value && ctgh.MaChiTietSanPham == mactsp)
                    .FirstOrDefault();

                if (existingItem != null)
                {
                    existingItem.SoLuong += soluong;
                    existingItem.TongTien = (gia - giaGiam) * existingItem.SoLuong;
                    existingItem.NgayCapNhat = DateTime.Now;

                }
                else
                {
                    // Nếu sản phẩm chưa có, thêm mới vào giỏ hàng
                    var ctgh = new ChiTietGioHang
                    {
                        MaChiTietSanPham = mactsp,
                        SoLuong = soluong,
                        MaGioHang = magiohang.Value,
                        TongTien = (gia - giaGiam) * soluong,
                        NgayCapNhat = DateTime.Now
                    };

                    _context.ChiTietGioHang.Add(ctgh);
                }

                _context.SaveChanges();

                return Json(new { code = 0, message = "Giỏ hàng đã được cập nhật" });
            }
            catch (Exception ex)
            {
                return Json(new { code = 2, message = "Giỏ hàng chưa được cập nhật: " + ex.Message });
            }
        }

        [RequiredLogin]
        [HttpPost]
        public IActionResult XoaSPGioHang ( int mactgh)
        {
            try
            {
                var ct = _context.ChiTietGioHang.Where(ct => ct.MaChiTietGio == mactgh).FirstOrDefault();
                _context.ChiTietGioHang.Remove(ct);
                _context.SaveChanges();
                return Json(new { code = 0, message = "Xóa thành công" });  
            }
            catch
            {
                return Json(new { code = 1, message = "Xóa không thành công" });
            }
        }

        [RequiredLogin]
        [HttpPost]
        public IActionResult SuaSPGioHang(int mactgh, int soluong)
        {
            try
            {
                var ct = _context.ChiTietGioHang.Where(ct => ct.MaChiTietGio == mactgh).FirstOrDefault();
                ct.SoLuong = soluong;
                var ctsp = _context.ChiTietSanPham.Where(ctsp => ctsp.MaChiTietSP == ct.MaChiTietSanPham).FirstOrDefault();
                ct.TongTien = (ctsp.Gia - ctsp.GiaGiam) * soluong;
                _context.SaveChanges();
                return Json(new { code = 0, message = "Cập nhật thành công" });
            }
            catch
            {
                return Json(new { code = 1, message = "Cập nhật không thành công" });
            }
        }
    }
}
