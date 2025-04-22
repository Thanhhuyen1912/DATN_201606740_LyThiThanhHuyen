using API.Service;
using CoreLib.AppDbContext;
using CoreLib.DTO;
using CoreLib.Entity;
using Lib_Core.DTO;
using Microsoft.AspNetCore.Mvc;
using SANPHAM.Authorize;

namespace DATN.Controllers
{
    public class DonHangController : Controller
    {
        private readonly AppDbContext _context;
        private readonly ILocationService _locationService;
        public DonHangController(AppDbContext context, ILocationService locationService)
        {
            _context = context;
            _locationService = locationService;
        }
        //Admin
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult DatHang()
        {
            return View();
        }
        //Khachhang
        [RequiredLogin]
        [HttpPost]
        public async Task<IActionResult> DatHang([FromBody] Guidathang guidh)
        {
            int ma = int.Parse(HttpContext.Session.GetString("MaGioHang"));
            if (guidh == null || guidh.mataikhoan == 0)
            {
                return BadRequest("Dữ liệu không hợp lệ.");
            }

            var maTaiKhoan = guidh.mataikhoan;

            var listDiaChi = _context.DiaChi
                .Where(dc => dc.MaTaiKhoan == maTaiKhoan)
                .ToList();
            var diachi = new List<DiaChi>();
          
            foreach (var p in listDiaChi)
            {
                // Gọi API để lấy tên các địa phương
                var huyen = await _locationService.GetDistrictNameByIdAsync(int.Parse(p.Huyen));
                var xa = await _locationService.GetWardNameByIdAsync(int.Parse(p.Xa), int.Parse(p.Huyen));
                var tp = await _locationService.GetProvinceNameByIdAsync(int.Parse(p.ThanhPho));

                diachi.Add(new DiaChi
                {
                    MaDiaChi = p.MaDiaChi,
                    Huyen = huyen,
                    Xa = xa,
                    ThanhPho = tp,
                    ChiTiet = p.ChiTiet,
                    SoDienThoaiNguoiNhan = p.SoDienThoaiNguoiNhan,
                    HoTenNguoiNhan = p.HoTenNguoiNhan,
                });
            }
            var listsp = new List<SanPhamWithImageDTO>();
            foreach (var chitiet in guidh.chitietgiohangs)
            {
                var ctsp = _context.ChiTietSanPham.Where(ctsp => ctsp.MaChiTietSP == chitiet.MaChiTietSanPham).FirstOrDefault();
                var sp = _context.SanPham.Where(sp => sp.MaSanPham == ctsp.MaSanPham).FirstOrDefault();
                var anhsp = _context.AnhSanPham.Where(asp => asp.MaSanPham == sp.MaSanPham).FirstOrDefault();
                var anh = _context.Anh.Where(a => a.MaAnh == anhsp.MaAnh).FirstOrDefault();
                listsp.Add(new SanPhamWithImageDTO
                {
                    TenSanPham = sp.TenSanPham,
                    ImageUrl = anh.URL,
                    soluong = chitiet.SoLuong,
                    Gia = ctsp.Gia - ctsp.GiaGiam

                });

            }
            var taiKhoan = _context.TaiKhoan
                .FirstOrDefault(tk => tk.MaTaiKhoan == maTaiKhoan);
            var pt = _context.PhuongThucThanhToan.Where(pt=>pt.TrangThai == true).ToList();
            var list = new List<ListDatHang>
    {
        new ListDatHang
        {
            diachis = diachi,
            chitietgiohangs = guidh.chitietgiohangs,
            taikhoan = taiKhoan,
            phuongthucs = pt,
            sanphamhienthi = listsp
        }
    };
            ViewBag.Tongtien = _context.ChiTietGioHang.Where(ctgh => ctgh.MaGioHang == ma).Sum(ctgh => ctgh.TongTien);
            return View(list);
        }

    }
}
