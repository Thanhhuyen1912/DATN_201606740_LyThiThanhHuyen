using CoreLib.AppDbContext;
using CoreLib.Entity;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
   
    [ApiController]
    public class DonHangController : Controller
    {
        private readonly AppDbContext _context;
        public DonHangController(AppDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        [Route("/DonHang/Danhsach")]
        public IActionResult getAll()
        {
            var donhang = _context.DonHang.Select(sp => new
            {
                sp.MaDonHang,
                sp.MaTaiKhoan,
                sp.MaDiaChi,
                sp.MaPhuongThucThanhToan,
                sp.MMaGiamGia,
                sp.TongTien,
                sp.NgayTao,
                sp.TrangThaiThanhToan,
                sp.TrangThaiVanChuyen,
                TenDatMua = _context.TaiKhoan
                  .Where(nh => nh.MaTaiKhoan == sp.MaTaiKhoan)
                  .Select(nh => nh.HoTen)
                  .FirstOrDefault(),
                DiaChiTP = _context.DiaChi
                  .Where(th => th.MaDiaChi == sp.MaDiaChi)
                  .Select(th => th.ThanhPho)
                  .FirstOrDefault(),
                DiaChiHuyen = _context.DiaChi
                  .Where(th => th.MaDiaChi == sp.MaDiaChi)
                  .Select(th => th.Huyen)
                  .FirstOrDefault(),
                DiaChiXa = _context.DiaChi
                  .Where(th => th.MaDiaChi == sp.MaDiaChi)
                  .Select(th => th.Xa)
                  .FirstOrDefault(),
                TienGiam = _context.MaGiamGia
                  .Where(th => th.MMaGiamGia == sp.MMaGiamGia)
                  .Select(th => th.GiaTri)
                  .FirstOrDefault(),
                LoaiGiamGia = _context.MaGiamGia
                  .Where(th => th.MMaGiamGia == sp.MMaGiamGia)
                  .Select(th => th.LoaiGiamGia)
                  .FirstOrDefault(),
                Phuongthucthanhtoan = _context.PhuongThucThanhToan
                  .Where(th => th.MaPhuongThuc == sp.MaPhuongThucThanhToan)
                  .Select(th => th.TenPhuongThuc)
                  .FirstOrDefault(),
                TenNhanHang = _context.DiaChi
                  .Where(th => th.MaDiaChi == sp.MaDiaChi)
                  .Select(th => th.HoTenNguoiNhan)
                  .FirstOrDefault(),
                SDTNhanHang = _context.DiaChi
                  .Where(th => th.MaDiaChi == sp.MaDiaChi)
                  .Select(th => th.SoDienThoaiNguoiNhan)
                  .FirstOrDefault(),
            }).ToList();

            return Json(new { data = donhang });

        }
        [HttpGet]
        [Route("/DonHang/ChiTiet")]
        public IActionResult getDetails(int madh)
        {
            var donhang = _context.DonHang
     .Where(sp => sp.MaDonHang == madh)
     .Select(sp => new
     {
         sp.MaDonHang,
         sp.MaTaiKhoan,
         sp.MaDiaChi,
         sp.MaPhuongThucThanhToan,
         sp.MMaGiamGia,
         sp.TongTien,
         sp.NgayTao,
         sp.TrangThaiThanhToan,
         sp.TrangThaiVanChuyen,

         TenDatMua = _context.TaiKhoan
             .Where(nh => nh.MaTaiKhoan == sp.MaTaiKhoan)
             .Select(nh => nh.HoTen)
             .FirstOrDefault(),

         DiaChiTP = _context.DiaChi
             .Where(th => th.MaDiaChi == sp.MaDiaChi)
             .Select(th => th.ThanhPho)
             .FirstOrDefault(),

         DiaChiHuyen = _context.DiaChi
             .Where(th => th.MaDiaChi == sp.MaDiaChi)
             .Select(th => th.Huyen)
             .FirstOrDefault(),

         DiaChiXa = _context.DiaChi
             .Where(th => th.MaDiaChi == sp.MaDiaChi)
             .Select(th => th.Xa)
             .FirstOrDefault(),

         DiaChiChiTiet = _context.DiaChi
             .Where(th => th.MaDiaChi == sp.MaDiaChi)
             .Select(th => th.ChiTiet)
             .FirstOrDefault(),

         TienGiam = _context.MaGiamGia
             .Where(th => th.MMaGiamGia == sp.MMaGiamGia)
             .Select(th => th.GiaTri)
             .FirstOrDefault(),

         LoaiGiamGia = _context.MaGiamGia
             .Where(th => th.MMaGiamGia == sp.MMaGiamGia)
             .Select(th => th.LoaiGiamGia)
             .FirstOrDefault(),

         Phuongthucthanhtoan = _context.PhuongThucThanhToan
             .Where(th => th.MaPhuongThuc == sp.MaPhuongThucThanhToan)
             .Select(th => th.TenPhuongThuc)
             .FirstOrDefault(),

         TenNhanHang = _context.DiaChi
             .Where(th => th.MaDiaChi == sp.MaDiaChi)
             .Select(th => th.HoTenNguoiNhan)
             .FirstOrDefault(),

         SDTNhanHang = _context.DiaChi
             .Where(th => th.MaDiaChi == sp.MaDiaChi)
             .Select(th => th.SoDienThoaiNguoiNhan)
             .FirstOrDefault(),

         Mail = _context.TaiKhoan
             .Where(th => th.MaTaiKhoan == sp.MaTaiKhoan)
             .Select(th => th.Email)
             .FirstOrDefault(),

         // Hiển thị thêm ký hiệu phần trăm nếu là giảm phần trăm
         TienGiamText = _context.MaGiamGia
             .Where(th => th.MMaGiamGia == sp.MMaGiamGia)
             .Select(th =>
                 th.LoaiGiamGia == "phantram"
                     ? th.GiaTri.ToString() + "%"
                     : th.GiaTri.ToString() + " ₫"
             )
             .FirstOrDefault()
     })
     .FirstOrDefault();

            return Ok(new { message = "Lấy thông tin thành công", code = 0, data = donhang });
        }

        [HttpPost]
        [Route("/DonHang/Them")]
        public IActionResult post(DonHang th)
        {
            try
            {
                _context.DonHang.Add(th);
                _context.SaveChanges();
                return Ok(new { message = "Thêm thành công", code = 0 });
            }
            catch
            { return BadRequest(); }

        }
        [HttpPut]
        [Route("/DonHang/CapNhat")]
        public IActionResult put(int madonhang, string ttvanchuyen, bool ttthanhtoan, DateTime ngaycapnhat)
        {
            var donhang = _context.DonHang.FirstOrDefault(p => p.MaDonHang == madonhang);
            if (donhang != null)
            {
                donhang.TrangThaiVanChuyen = ttvanchuyen;
                donhang.TrangThaiThanhToan = ttthanhtoan;
                donhang.NgayCapNhat = ngaycapnhat;
                _context.SaveChanges();
                return Ok(new { message = "Cập nhật thành công", code = 0 });
            }
            else
            {
                return BadRequest(new { message = "Cập nhật thất bại", code = 1 });
            }
        }
        [HttpGet]
        [Route("/DonHang/DanhSachSP")]
        public IActionResult DS(int madonhang)
        {
            var donhang = _context.ChiTietDonHang
                .Where(p => p.MaDonHang == madonhang)
                .Select(dh => new
                {
                    MaSanPham = _context.ChiTietSanPham.Where(ct => ct.MaChiTietSP == dh.MaChiTietSP).Select(ct => ct.MaSanPham).FirstOrDefault(),
                    GiaTien = _context.ChiTietSanPham.Where(ct => ct.MaChiTietSP == dh.MaChiTietSP).Select(ct => ct.Gia).FirstOrDefault(),
                    GiaGiam = _context.ChiTietSanPham.Where(ct => ct.MaChiTietSP == dh.MaChiTietSP).Select(ct => ct.GiaGiam).FirstOrDefault(),
                    TenSanPham = (from ct in _context.ChiTietSanPham
                                  join sp in _context.SanPham on ct.MaSanPham equals sp.MaSanPham
                                  where ct.MaChiTietSP == dh.MaChiTietSP
                                  select sp.TenSanPham).FirstOrDefault(),
                    SoLuong = dh.SoLuong,
                    TongTien = dh.TongTien}).ToList();

            return Ok(new { message = "Lấy danh sách thành công", code = 0, data = donhang });

        }
        public class DonHangSearchRequest
        {
            public string? TrangThaiVanChuyen { get; set; }
            public int? Ptthanhtoan { get; set; }
            public bool? TrangthaiThanhToan { get; set; }
            public DateTime? NgayBatDau { get; set; }
            public DateTime? NgayKetThuc { get; set; }
        }

        [HttpPost]
        [Route("DonHang/TimKiem")]
        public IActionResult LocDonHang([FromBody] DonHangSearchRequest request)
        {
            var donhang = _context.DonHang
                .Where(sp =>
                    (string.IsNullOrEmpty(request.TrangThaiVanChuyen) || sp.TrangThaiVanChuyen == request.TrangThaiVanChuyen) &&
                    (!request.Ptthanhtoan.HasValue || sp.MaPhuongThucThanhToan == request.Ptthanhtoan) &&
                    (!request.TrangthaiThanhToan.HasValue || sp.TrangThaiThanhToan == request.TrangthaiThanhToan) &&
                    (!request.NgayBatDau.HasValue || sp.NgayTao >= request.NgayBatDau.Value) &&
                    (!request.NgayKetThuc.HasValue || sp.NgayTao <= request.NgayKetThuc.Value)
                )
                .Select(sp => new
                {
                    sp.MaDonHang,
                    sp.MaTaiKhoan,
                    sp.MaDiaChi,
                    sp.MaPhuongThucThanhToan,
                    sp.MMaGiamGia,
                    sp.TongTien,
                    sp.NgayTao,
                    sp.TrangThaiThanhToan,
                    sp.TrangThaiVanChuyen,
                    TenDatMua = _context.TaiKhoan
                  .Where(nh => nh.MaTaiKhoan == sp.MaTaiKhoan)
                  .Select(nh => nh.HoTen)
                  .FirstOrDefault(),
                    DiaChiTP = _context.DiaChi
                  .Where(th => th.MaDiaChi == sp.MaDiaChi)
                  .Select(th => th.ThanhPho)
                  .FirstOrDefault(),
                    DiaChiHuyen = _context.DiaChi
                  .Where(th => th.MaDiaChi == sp.MaDiaChi)
                  .Select(th => th.Huyen)
                  .FirstOrDefault(),
                    DiaChiXa = _context.DiaChi
                  .Where(th => th.MaDiaChi == sp.MaDiaChi)
                  .Select(th => th.Xa)
                  .FirstOrDefault(),
                    TienGiam = _context.MaGiamGia
                  .Where(th => th.MMaGiamGia == sp.MMaGiamGia)
                  .Select(th => th.GiaTri)
                  .FirstOrDefault(),
                    LoaiGiamGia = _context.MaGiamGia
                  .Where(th => th.MMaGiamGia == sp.MMaGiamGia)
                  .Select(th => th.LoaiGiamGia)
                  .FirstOrDefault(),
                    Phuongthucthanhtoan = _context.PhuongThucThanhToan
                  .Where(th => th.MaPhuongThuc == sp.MaPhuongThucThanhToan)
                  .Select(th => th.TenPhuongThuc)
                  .FirstOrDefault(),
                    TenNhanHang = _context.DiaChi
                  .Where(th => th.MaDiaChi == sp.MaDiaChi)
                  .Select(th => th.HoTenNguoiNhan)
                  .FirstOrDefault(),
                    SDTNhanHang = _context.DiaChi
                  .Where(th => th.MaDiaChi == sp.MaDiaChi)
                  .Select(th => th.SoDienThoaiNguoiNhan)
                  .FirstOrDefault(),
                }).ToList();
            return Ok(new { message = "Tìm kiếm thành công", code = 0, data = donhang });
        }

    }
}