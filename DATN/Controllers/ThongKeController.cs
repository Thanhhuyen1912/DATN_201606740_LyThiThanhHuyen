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
                var dulieu_gioitinh = LayDoanhThuTheoGioiTinhCSV();
                var dulieu_xuatxu = LayDoanhThuTheXuatXuCSV();
                var dulieu_donut = LayDoanhThuTongTheoCSV();
                var dulieu_top5chay = Top5SanPhamBanChay();
                var dulieu_top5e = Top5SanPhamBanE();

                var dulieutong = new ThongKeDoanhThuTong
                {
                    dulieu = dulieu,
                    dulieu_thuonghieu = dulieu_thuonghieu,
                    dulieu_nhomhuong = dulieu_nhomhuong,
                    dulieu_gioitinh = dulieu_gioitinh,
                    dulieu_xuatxu = dulieu_xuatxu,
                    dulieu_donut = dulieu_donut,
                    dulieu_top5chay = dulieu_top5chay,
                    dulieu_top5e = dulieu_top5e
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
                        where dh.TrangThaiThanhToan == true
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
        }
        public string LayDoanhThuTheoGioiTinhCSV()
        {
            var query = from dh in _context.DonHang
                        where dh.TrangThaiThanhToan == true
                        join ctdh in _context.ChiTietDonHang on dh.MaDonHang equals ctdh.MaDonHang
                        join ctsp in _context.ChiTietSanPham on ctdh.MaChiTietSP equals ctsp.MaChiTietSP
                        join sp in _context.SanPham on ctsp.MaSanPham equals sp.MaSanPham
                        group ctdh by sp.GioiTinh ?? "Không xác định" into g
                        select new
                        {
                            TenGioiTinh = g.Key,
                            SoLuong = g.Sum(x => x.SoLuong)
                        };
            var GioiTinhList = query.ToList();

            int tongSoLuong = GioiTinhList.Sum(x => x.SoLuong);

            var top3 = GioiTinhList
                .OrderByDescending(x => x.SoLuong)
                .ToList();

            int soLuongTop3 = top3.Sum(x => x.SoLuong);

            var ketQua = top3
                .Select(x => new
                {
                    TenGioiTinh = x.TenGioiTinh,
                    SoLuong = x.SoLuong,
                    PhanTram = Math.Round((double)x.SoLuong / tongSoLuong * 100, 2)
                })
                .ToList();

            // Tạo chuỗi CSV
            StringBuilder csv = new StringBuilder();
            csv.AppendLine("TenGioiTinh,SoLuong,PhanTram");

            foreach (var item in ketQua)
            {
                // Đảm bảo dùng dấu chấm cho số thập phân
                string phanTramStr = item.PhanTram.ToString(CultureInfo.InvariantCulture);
                csv.AppendLine($"{item.TenGioiTinh},{item.SoLuong},{phanTramStr}");
            }
            return csv.ToString();
        }

        public string LayDoanhThuTheXuatXuCSV()
        {
            var query = from dh in _context.DonHang
                        where dh.TrangThaiThanhToan == true
                        join ctdh in _context.ChiTietDonHang on dh.MaDonHang equals ctdh.MaDonHang
                        join ctsp in _context.ChiTietSanPham on ctdh.MaChiTietSP equals ctsp.MaChiTietSP
                        join sp in _context.SanPham on ctsp.MaSanPham equals sp.MaSanPham
                        join th in _context.ThuongHieu on sp.MaThuongHieu equals th.MaThuongHieu
                        group ctdh by th.QuocGia ?? "Không xác định" into g
                        select new
                        {
                            TenQuocGia = g.Key,
                            SoLuong = g.Sum(x => x.SoLuong)
                        };
            var QGList = query.ToList();

            int tongSoLuong = QGList.Sum(x => x.SoLuong);

            var top5 = QGList
                .OrderByDescending(x => x.SoLuong)
                .ToList();

            int soLuongTop5 = top5.Sum(x => x.SoLuong);

            var ketQua = top5
                .Select(x => new
                {
                    TenQuocGia = x.TenQuocGia,
                    SoLuong = x.SoLuong,
                    PhanTram = Math.Round((double)x.SoLuong / tongSoLuong * 100, 2)
                })
                .ToList();
            if (tongSoLuong > soLuongTop5)
            {
                ketQua.Add(new
                {
                    TenQuocGia = "Khác",
                    SoLuong = tongSoLuong - soLuongTop5,
                    PhanTram = Math.Round((double)(tongSoLuong - soLuongTop5) / tongSoLuong * 100, 2)
                });
            }
            // Tạo chuỗi CSV
            StringBuilder csv = new StringBuilder();
            csv.AppendLine("TenQuocGia,SoLuong,PhanTram");

            foreach (var item in ketQua)
            {
                // Đảm bảo dùng dấu chấm cho số thập phân
                string phanTramStr = item.PhanTram.ToString(CultureInfo.InvariantCulture);
                csv.AppendLine($"{item.TenQuocGia},{item.SoLuong},{phanTramStr}");
            }
            return csv.ToString();
        }
        public string LayDoanhThuTheoNhomHuongCSV()
        {
            var query = from dh in _context.DonHang
                        where dh.TrangThaiThanhToan == true
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
                .Where(dh => dh.NgayCapNhat.Year == DateTime.Now.Year && dh.TrangThaiThanhToan == true)
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
        public string LayDoanhThuTongTheoCSV()
        {
            var donHangsNamNay = _context.DonHang
                .Where(dh => dh.TrangThaiThanhToan == true)
                .ToList();

            decimal tong = donHangsNamNay.Sum(d => d.TongTien);
            decimal online = donHangsNamNay
                .Where(d => d.MaPhuongThucThanhToan == 2)
                .Sum(d => d.TongTien);
            decimal offline = donHangsNamNay
                .Where(d => d.MaPhuongThucThanhToan == 1)
                .Sum(d => d.TongTien);

            var csv = new StringBuilder();
            csv.AppendLine("TongDoanhThu,DoanhThuOnline,DoanhThuOffline");
            csv.AppendLine($"{tong},{online},{offline}");

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

        public string Top5SanPhamBanChay()
        {
            var topSanPham = (from dh in _context.DonHang
                              where dh.TrangThaiThanhToan == true
                              join ctdh in _context.ChiTietDonHang on dh.MaDonHang equals ctdh.MaDonHang
                              join ctsp in _context.ChiTietSanPham on ctdh.MaChiTietSP equals ctsp.MaChiTietSP
                              join sp in _context.SanPham on ctsp.MaSanPham equals sp.MaSanPham
                              group ctdh by sp.TenSanPham into g
                              select new
                              {
                                  TenSanPham = g.Key,
                                  SoLuong = g.Sum(x => x.SoLuong)
                              })
                             .OrderByDescending(x => x.SoLuong)
                             .Take(5)
                             .ToList();

            var csv = new StringBuilder();
            csv.AppendLine("TenSanPham,SoLuongBan");

            foreach (var sp in topSanPham)
            {
                var ten = sp.TenSanPham.Replace(",", " ");
                csv.AppendLine($"{ten},{sp.SoLuong}");
            }

            return csv.ToString();
        }


        public string Top5SanPhamBanE()
        {
            var topSanPham = (from ctsp in _context.ChiTietSanPham                              
                              join sp in _context.SanPham on ctsp.MaSanPham equals sp.MaSanPham
                              group ctsp by sp.TenSanPham into g
                              select new
                              {
                                  TenSanPham = g.Key,
                                  SoLuong = g.Sum(x => x.SoLuong)
                              })
                             .OrderByDescending(x => x.SoLuong)
                             .Take(5)
                             .ToList();

            var csv = new StringBuilder();
            csv.AppendLine("TenSanPham,SoLuongCon");

            foreach (var sp in topSanPham)
            {
                var ten = sp.TenSanPham.Replace(",", " ");
                csv.AppendLine($"{ten},{sp.SoLuong}");
            }

            return csv.ToString();
        }
    }
}
