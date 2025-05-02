using CoreLib.AppDbContext;
using CoreLib.DTO;
using CoreLib.Entity;
using Microsoft.AspNetCore.Mvc;
using SANPHAM.Authorize;
using System.Globalization;
using System.Text;

namespace DATN.Controllers
{
    public class ThongKeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly AppDbContext _context;
        public ThongKeController(AppDbContext context, ILogger<HomeController> logger)
        {
            _context = context;
            _logger = logger;
        }
        [RequiredLogin]
        public IActionResult Index()
        {
            string? role = HttpContext.Session.GetString("Role");
            if (role != null && (role.Contains("0") || role.Contains("1")))
            {
                var dulieu = LayDoanhThuTheoThangCSV(_context.DonHang.ToList());
                var dulieu_thuonghieu = LayDoanhThuTheoThuongHieuCSV();
                var dulieu_nhomhuong = LayDoanhThuTheoNhomHuongCSV();

                var dulieutong = new ThongKeDoanhThuTong
                {
                    dulieu = dulieu,
                    dulieu_thuonghieu = dulieu_thuonghieu,
                    dulieu_nhomhuong = dulieu_nhomhuong
                };
                return View(model: dulieutong);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
        public string LayDoanhThuTheoThuongHieuCSV()
        {
            var query = from dh in _context.DonHang                        
                        join ctdh in _context.ChiTietDonHang on dh.MaDonHang equals ctdh.MaDonHang
                        join ctsp in _context.ChiTietSanPham on ctdh.MaChiTietSP equals ctsp.MaChiTietSP
                        join sp in _context.SanPham on ctsp.MaSanPham equals sp.MaSanPham
                        join th in _context.ThuongHieu on sp.MaThuongHieu equals th.MaThuongHieu
                        group ctdh by th.TenThuongHieu into g
                        select new
                        {
                            TenThuongHieu = g.Key,
                            SoLuong = g.Sum(x => x.SoLuong)
                        };
            var thuongHieuList = query.ToList();

            int tongSoLuong = thuongHieuList.Sum(x => x.SoLuong);

            var top5 = thuongHieuList
                .OrderByDescending(x => x.SoLuong)
                .Take(5)
                .ToList();

            int soLuongTop5 = top5.Sum(x => x.SoLuong);

            var ketQua = top5
                .Select(x => new
                {
                    TenThuongHieu = x.TenThuongHieu,
                    SoLuong = x.SoLuong,
                    PhanTram = Math.Round((double)x.SoLuong / tongSoLuong * 100, 2)
                })
                .ToList();

            if (tongSoLuong > soLuongTop5)
            {
                ketQua.Add(new
                {
                    TenThuongHieu = "Khác",
                    SoLuong = tongSoLuong - soLuongTop5,
                    PhanTram = Math.Round((double)(tongSoLuong - soLuongTop5) / tongSoLuong * 100, 2)
                });
            }
            // Tạo chuỗi CSV
            StringBuilder csv = new StringBuilder();
            csv.AppendLine("TenThuongHieu,SoLuong,PhanTram");
           
            foreach (var item in ketQua)
            {
                // Đảm bảo dùng dấu chấm cho số thập phân
                string phanTramStr = item.PhanTram.ToString(CultureInfo.InvariantCulture);
                csv.AppendLine($"{item.TenThuongHieu},{item.SoLuong},{phanTramStr}");
            }
            return csv.ToString();
        } public string LayDoanhThuTheoNhomHuongCSV()
        {
            var query = from dh in _context.DonHang                        
                        join ctdh in _context.ChiTietDonHang on dh.MaDonHang equals ctdh.MaDonHang
                        join ctsp in _context.ChiTietSanPham on ctdh.MaChiTietSP equals ctsp.MaChiTietSP
                        join sp in _context.SanPham on ctsp.MaSanPham equals sp.MaSanPham
                        join th in _context.NhomHuong on sp.MaNhomHuong equals th.MaNhomHuong
                        group ctdh by th.TenNhomHuong into g
                        select new
                        {
                            TenNhomHuong = g.Key,
                            SoLuong = g.Sum(x => x.SoLuong)
                        };
            var nhomhuongList = query.ToList();

            int tongSoLuong = nhomhuongList.Sum(x => x.SoLuong);

            var top5 = nhomhuongList
                .OrderByDescending(x => x.SoLuong)
                .Take(5)
                .ToList();

            int soLuongTop5 = top5.Sum(x => x.SoLuong);

            var ketQua = top5
                .Select(x => new
                {
                    TenNhomHuong = x.TenNhomHuong,
                    SoLuong = x.SoLuong,
                    PhanTram = Math.Round((double)x.SoLuong / tongSoLuong * 100, 2)
                })
                .ToList();

            if (tongSoLuong > soLuongTop5)
            {
                ketQua.Add(new
                {
                    TenNhomHuong = "Khác",
                    SoLuong = tongSoLuong - soLuongTop5,
                    PhanTram = Math.Round((double)(tongSoLuong - soLuongTop5) / tongSoLuong * 100, 2)
                });
            }
            // Tạo chuỗi CSV
            StringBuilder csv = new StringBuilder();
            csv.AppendLine("TenNhomHuong,SoLuong,PhanTram");
           
            foreach (var item in ketQua)
            {
                // Đảm bảo dùng dấu chấm cho số thập phân
                string phanTramStr = item.PhanTram.ToString(CultureInfo.InvariantCulture);
                csv.AppendLine($"{item.TenNhomHuong},{item.SoLuong},{phanTramStr}");
            }
            return csv.ToString();
        }

        public string LayDoanhThuTheoThangCSV(List<DonHang> donHangs)
        {
            var doanhThuThang = donHangs
                .Where(dh => dh.NgayCapNhat.Year == DateTime.Now.Year)
                .GroupBy(dh => dh.NgayCapNhat.Month)
                .ToDictionary(
                    g => g.Key,
                    g => new
                    {
                        Tong = g.Sum(d => d.TongTien),
                        Online = g.Where(d => d.MaPhuongThucThanhToan == 2).Sum(d => d.TongTien),
                        Offline = g.Where(d => d.MaPhuongThucThanhToan == 1).Sum(d => d.TongTien)
                    });

            var csv = new StringBuilder();
            csv.AppendLine("Thang,TongDoanhThu,DoanhThuOnline,DoanhThuOffline");

            for (int i = 1; i <= 12; i++)
            {
                var doanhThu = doanhThuThang.ContainsKey(i) ? doanhThuThang[i] : null;
                var tong = doanhThu?.Tong ?? 0;
                var online = doanhThu?.Online ?? 0;
                var offline = doanhThu?.Offline ?? 0;

                csv.AppendLine($"{i},{tong},{online},{offline}");
            }

            return csv.ToString();
        }


        [RequiredLogin]
        public IActionResult TheoTuan()
        {
            string? role = HttpContext.Session.GetString("Role");
            if (role != null && (role.Contains("0") || role.Contains("1")))
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
    }
}
