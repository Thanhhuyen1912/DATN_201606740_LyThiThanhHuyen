﻿using API.Service;
using CoreLib.AppDbContext;
using CoreLib.DTO;
using CoreLib.Entity;
using Microsoft.AspNetCore.Mvc;
using SANPHAM.Authorize;
using System.Text;
using Microsoft.AspNetCore.Mvc.ViewEngines;
namespace DATN.Controllers
{

    public class DonHangController : Controller
    {
        private readonly AppDbContext _context;
        private readonly ILocationService _locationService;
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly ICompositeViewEngine _viewEngine;


        public DonHangController(ICompositeViewEngine viewEngine, AppDbContext context, ILocationService locationService, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            _locationService = locationService;
            _hostEnvironment = hostEnvironment;
            _viewEngine = viewEngine;
        }
        //Admin
        [RequiredLogin]
        public IActionResult Index()
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
        [RequiredLoginBuy]
        public IActionResult DatHang()
        {
            ViewBag.TieuDe = "Đặt hàng";
            return View();
        }
        //Khachhang
        [RequiredLoginBuy]
        [HttpPost]
        public async Task<IActionResult> DatHang([FromBody] Guidathang guidh)
        {
            ViewBag.TieuDe = "Đặt hàng";
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
                if (ctsp.SoLuong < chitiet.SoLuong)
                {
                    return Json(new { message = "Số lượng không đủ" });
                }
                var sp = _context.SanPham.Where(sp => sp.MaSanPham == ctsp.MaSanPham).FirstOrDefault();
                var anhsp = _context.AnhSanPham.Where(asp => asp.MaSanPham == sp.MaSanPham).FirstOrDefault();
                var anh = _context.Anh.Where(a => a.MaAnh == anhsp.MaAnh).FirstOrDefault();
                listsp.Add(new SanPhamWithImageDTO
                {
                    TenSanPham = sp.TenSanPham,
                    ImageUrl = anh.URL,
                    soluong = chitiet.SoLuong,
                    Gia = ctsp.Gia - ctsp.GiaGiam,
                    MaChiTietSP = chitiet.MaChiTietSanPham
                });
            }

            var taiKhoan = _context.TaiKhoan
                .FirstOrDefault(tk => tk.MaTaiKhoan == maTaiKhoan);
            var pt = _context.PhuongThucThanhToan.Where(pt => pt.TrangThai == true).ToList();

            var list = new ListDatHang
            {
                diachis = diachi,
                chitietgiohangs = guidh.chitietgiohangs,
                taikhoan = taiKhoan,
                phuongthucs = pt,
                sanphamhienthi = listsp ?? new List<SanPhamWithImageDTO>()
            };

            if (guidh.chitietgiohangs.Count > 1)
            {
                ViewBag.Tongtien = _context.ChiTietGioHang.Where(ctgh => ctgh.MaGioHang == ma).Sum(ctgh => ctgh.TongTien);
            }
            else
            {
                ViewBag.Tongtien = listsp[0].soluong * (listsp[0].Gia - listsp[0].GiaGiam);
            }
            ViewBag.Giamgia = 0;
            HttpContext.Session.Remove("MaSanPham");
            return View(list);
        }
        [RequiredLogin]
        [HttpPost]
        public IActionResult XacNhan([FromBody] OrderRequest request)
        {
            try
            {
                int mataikhoan = int.Parse(HttpContext.Session.GetString("MaTaiKhoan"));
                int magiohang = int.Parse(HttpContext.Session.GetString("MaGioHang"));
                DonHang donhang = new DonHang
                {
                    MaTaiKhoan = mataikhoan,
                    NgayTao = DateTime.Now,
                    NgayCapNhat = DateTime.Now,
                    TrangThaiVanChuyen = "Chờ xác nhận",
                    MMaGiamGia = string.IsNullOrEmpty(request.magiamgia) ? null : _context.MaGiamGia.Where(gg => gg.MaHienThi == request.magiamgia).Select(gg => gg.MMaGiamGia).FirstOrDefault(),
                    MaPhuongThucThanhToan = _context.PhuongThucThanhToan.Where(gg => gg.MaPhuongThuc == request.maphuongthuc).Select(gg => gg.MaPhuongThuc).FirstOrDefault(),
                    TrangThaiThanhToan = false,
                    MaDiaChi = request.diachi,
                    TongTien = request.thanhtien
                };
                _context.DonHang.Add(donhang);
                _context.SaveChanges();

                foreach (var dathang in request.listsp)
                {
                    ChiTietDonHang chitietdonhang = new ChiTietDonHang
                    {
                        MaDonHang = donhang.MaDonHang,
                        MaChiTietSP = dathang.MaChiTietSanPham,
                        SoLuong = dathang.SoLuong,
                        TongTien = dathang.TongTien,
                        Gia = dathang.SoLuong == 0 ? 0 : dathang.TongTien / dathang.SoLuong
                    };
                    var chitietsp = _context.ChiTietSanPham.Where(x => x.MaChiTietSP == chitietdonhang.MaChiTietSP).FirstOrDefault();
                    if (chitietsp != null)
                    {
                        chitietsp.SoLuong -= chitietdonhang.SoLuong;
                        _context.ChiTietDonHang.Add(chitietdonhang);
                        ChiTietGioHang? chitietgiohang = _context.ChiTietGioHang.Where(x => x.MaChiTietSanPham == chitietsp.MaChiTietSP && x.MaGioHang == magiohang).FirstOrDefault();
                        if (chitietgiohang != null)
                        {
                            _context.ChiTietGioHang.Remove(chitietgiohang);
                        }
                    }
                    else
                    {
                        return Json(new { code = 1, message = "Sản phẩm không tồn tại trong kho." });
                    }
                }

                _context.SaveChanges();
                HttpContext.Session.SetInt32("MaDonHang", donhang.MaDonHang);
                if (donhang.MaPhuongThucThanhToan == 2)
                {
                    return Json(new { code = 0, message = "Đặt hàng thành công vui lòng hoàn thành thanh toán.", redirectUrl = Url.Action("XacNhanThanhToan", "DonHang") });
                }
                else
                {
                    return Json(new { code = 0, message = "Đặt hàng thành công", redirectUrl = Url.Action("Index", "SanPhamKhach") });

                }

            }
            catch (Exception ex)
            {
                return Json(new { code = 1, message = "Đặt hàng không thành công: " + ex.Message });
            }
        }
        [HttpPost]
        public IActionResult TaoSessionMaDonHang([FromBody] DonHangSessionDto data)
        {
            HttpContext.Session.SetInt32("MaDonHang", int.Parse(data.MaDonHang));
            return Ok();
        }

        [RequiredLogin]
        [HttpPost]
        public IActionResult ApDungMaGiamGia([FromBody] MaGiamGiaRequest request)
        {
            try
            {
                var giamgia = _context.MaGiamGia.FirstOrDefault(m => m.MaHienThi == request.magiamgia);
                if (giamgia == null)
                    return Json(new { code = 1, message = "Mã giảm giá không tồn tại" });
                decimal tongmoi = 0;
                decimal giagtrigiam = 0;
                string loaigiamgia = giamgia.LoaiGiamGia;
                if (DateTime.Now >= giamgia.NgayKetThuc || giamgia.NgayBatDau >= DateTime.Now)
                {
                    return Json(new { code = 1, message = "Thời gian áp dụng mã không phù hợp" });
                }
                if (loaigiamgia.Contains("Giảm theo %"))
                {
                    tongmoi = request.tongtien * (1 - (decimal)giamgia.GiaTri / 100);
                    giagtrigiam = (decimal)(giamgia.GiaTri / 100) * request.tongtien;
                    if (tongmoi < 0) tongmoi = 0;
                }
                else
                {
                    tongmoi = request.tongtien - (decimal)giamgia.GiaTri;
                    giagtrigiam = (decimal)giamgia.GiaTri;

                    if (tongmoi < 0) tongmoi = 0;
                }
                return Json(new { code = 0, tongtien = tongmoi, giamgia = giagtrigiam, message = "Thành công áp dụng mã giảm giá" });
            }
            catch (Exception ex)
            {
                return Json(new { code = 1, message = "Không thể áp dụng mã giảm giá" });
            }
        }
        [RequiredLogin]
        [HttpGet]
        public async Task<IActionResult> XacNhanThanhToan()
        {
            ViewBag.TieuDe = "Thanh toán";
            int? madonhang = HttpContext.Session.GetInt32("MaDonHang");
            var donhang = _context.DonHang.Where(dh => dh.MaDonHang == madonhang).FirstOrDefault();
            int? mataikhoan = int.Parse(HttpContext.Session.GetString("MaTaiKhoan"));
            var taikhoan = _context.TaiKhoan.Where(dh => dh.MaTaiKhoan == mataikhoan).FirstOrDefault();
            var p = _context.DiaChi.Where(dc => dc.MaDiaChi == donhang.MaDiaChi).FirstOrDefault();

            var huyen = await _locationService.GetDistrictNameByIdAsync(int.Parse(p.Huyen));
            var xa = await _locationService.GetWardNameByIdAsync(int.Parse(p.Xa), int.Parse(p.Huyen));
            var tp = await _locationService.GetProvinceNameByIdAsync(int.Parse(p.ThanhPho));
            var ND = "TTĐH" + donhang.MaDonHang + "HN";
            var Url = TaoURLQRThanhToan(donhang.TongTien, ND);
            p.Huyen = huyen; p.Xa = xa; p.ThanhPho = tp;
            var list = new TTTToan
            {
                donhang = donhang,
                diachi = p,
                taikhoan = taikhoan,
                URLThanhToan = Url
            };
            HttpContext.Session.Remove("MaDonHang");
            return View(list);
        }
        private string TaoURLQRThanhToan(decimal soTien, string noiDung)
        {

            var tenNganHang = "mb";
            var soTaiKhoan = "9969671912";
            var url = $"https://img.vietqr.io/image/{tenNganHang}-{soTaiKhoan}-compact2.png" +
                      $"?amount={soTien.ToString()}&addInfo={noiDung.ToString()}";

            return url;
        }
        [HttpPost]
        public async Task<IActionResult> CheckChuyenKhoan([FromBody] ThongTinCheckThanhToan ttck)
        {
            if (string.IsNullOrWhiteSpace(ttck.Noidungchuyenkhoan) || ttck.sotien <= 0)
            {
                return Json(new { code = 2, message = "Dữ liệu chuyển khoản không hợp lệ" });
            }

            var thanhtoan = _context.ThanhToan.FirstOrDefault(tt =>
                tt.NoiDungChuyenKhoan.ToLower().Contains(ttck.Noidungchuyenkhoan.ToLower()) &&
                tt.SoTienGiaoDich == ttck.sotien);

            if (thanhtoan == null)
            {
                return Json(new { code = 1, message = "Thanh toán chưa được xác nhận" });
            }

            thanhtoan.MaDonHang = ttck.madonhang;
            thanhtoan.MaTaiKhoan = ttck.mataikhoan;

            var donhang = _context.DonHang.Where(dh => dh.MaDonHang == ttck.madonhang).FirstOrDefault();
            donhang.TrangThaiThanhToan = true;
            donhang.TrangThaiVanChuyen = "Đang vận chuyển";
            await _context.SaveChangesAsync();

            return Json(new { code = 0, message = "Xác thực thanh toán thành công" });
        }
        [RequiredLogin]
        public IActionResult DonMua()
        {
            ViewBag.TieuDe = "Lịch sử mua hàng";
            int? mataikhoan = int.Parse(HttpContext.Session.GetString("MaTaiKhoan"));
            var donhang = _context.DonHang
                .Where(sp => sp.MaTaiKhoan == mataikhoan)
                .Select(sp => new DonMuaViewModel
                {
                    MaDonHang = sp.MaDonHang,
                    MaTaiKhoan = sp.MaTaiKhoan,
                    MaDiaChi = sp.MaDiaChi,
                    MaPhuongThucThanhToan = sp.MaPhuongThucThanhToan,
                    MMaGiamGia = sp.MMaGiamGia,
                    TongTien = sp.TongTien,
                    NgayTao = sp.NgayTao,
                    TrangThaiThanhToan = sp.TrangThaiThanhToan,
                    TrangThaiVanChuyen = sp.TrangThaiVanChuyen,
                    TenDatMua = _context.TaiKhoan.FirstOrDefault(nh => nh.MaTaiKhoan == sp.MaTaiKhoan).HoTen,
                    DiaChiTP = _context.DiaChi.FirstOrDefault(th => th.MaDiaChi == sp.MaDiaChi).ThanhPho,
                    DiaChiHuyen = _context.DiaChi.FirstOrDefault(th => th.MaDiaChi == sp.MaDiaChi).Huyen,
                    DiaChiXa = _context.DiaChi.FirstOrDefault(th => th.MaDiaChi == sp.MaDiaChi).Xa,
                    TienGiam = (decimal)_context.MaGiamGia.FirstOrDefault(th => th.MMaGiamGia == sp.MMaGiamGia).GiaTri,
                    LoaiGiamGia = _context.MaGiamGia.FirstOrDefault(th => th.MMaGiamGia == sp.MMaGiamGia).LoaiGiamGia,
                    Phuongthucthanhtoan = _context.PhuongThucThanhToan.FirstOrDefault(th => th.MaPhuongThuc == sp.MaPhuongThucThanhToan).TenPhuongThuc,
                    TenNhanHang = _context.DiaChi.FirstOrDefault(th => th.MaDiaChi == sp.MaDiaChi).HoTenNguoiNhan,
                    SDTNhanHang = _context.DiaChi.FirstOrDefault(th => th.MaDiaChi == sp.MaDiaChi).SoDienThoaiNguoiNhan
                })
                .ToList();

            return View(donhang);
        }

        [RequiredLogin]
        public async Task<IActionResult> ThayDoiDiaChi(string id)
        {
            ViewBag.TieuDe = "Thay đổi địa chỉ";
            if (string.IsNullOrWhiteSpace(id))
            {
                return BadRequest("Mã đơn hàng không được để trống.");
            }

            if (!int.TryParse(id, out int maDonHangInt))
            {
                return BadRequest("Mã đơn hàng không hợp lệ.");
            }

            var dh = _context.DonHang.FirstOrDefault(d => d.MaDonHang == maDonHangInt);
            if (dh == null)
            {
                return NotFound("Không tìm thấy đơn hàng.");
            }

            var p = _context.DiaChi.FirstOrDefault(dcc => dcc.MaDiaChi == dh.MaDiaChi);
            if (p == null)
            {
                return NotFound("Không tìm thấy địa chỉ của đơn hàng.");
            }

            if (!int.TryParse(p.Huyen, out int huyenId) ||
                !int.TryParse(p.Xa, out int xaId) ||
                !int.TryParse(p.ThanhPho, out int tpId))
            {
                return BadRequest("Thông tin địa chỉ không hợp lệ.");
            }

            var diachilist = _context.DiaChi
                .Where(dcc => dcc.MaTaiKhoan == dh.MaTaiKhoan)
                .ToList();

            var huyen = await _locationService.GetDistrictNameByIdAsync(huyenId);
            var xa = await _locationService.GetWardNameByIdAsync(xaId, huyenId);
            var tp = await _locationService.GetProvinceNameByIdAsync(tpId);
            p.Huyen = huyen; p.Xa = xa; p.ThanhPho = tp;


            var dclist = new List<DiaChi>();
            foreach (var dc in diachilist)
            {
                if (int.TryParse(dc.Huyen, out int huyenDc) &&
                    int.TryParse(dc.Xa, out int xaDc) &&
                    int.TryParse(dc.ThanhPho, out int tpDc))
                {
                    var huyen1 = await _locationService.GetDistrictNameByIdAsync(huyenDc);
                    var xa1 = await _locationService.GetWardNameByIdAsync(xaDc, huyenDc);
                    var tp1 = await _locationService.GetProvinceNameByIdAsync(tpDc);

                    dc.Huyen = huyen1;
                    dc.Xa = xa1;
                    dc.ThanhPho = tp1;

                    dclist.Add(dc);
                }
            }
            var listthaydoi = new viewthaydoidiachi
            {
                list = dclist,
                p = p,
                madonhang = dh.MaDonHang
            };

            return View(listthaydoi);
        }
        [RequiredLogin]
        [HttpPost]
        public IActionResult doidiachi([FromBody] noidungthaydiachi dto)
        {
            var donhang = _context.DonHang.Where(dh => dh.MaDonHang == dto.madonhang).FirstOrDefault();
            donhang.MaDiaChi = dto.madiachi;
            _context.SaveChanges();
            return RedirectToAction("DonMua", "DonHang");
        }

        [RequiredLogin]
        [HttpPost]
        public IActionResult huydonhang(int id, int page = 1)
        {
            var donhang = _context.DonHang.Where(dh => dh.MaDonHang == id).FirstOrDefault();
            if (donhang.TrangThaiVanChuyen == "Chờ xác nhận")
            {
                donhang.TrangThaiVanChuyen = "Đã hủy";
                _context.SaveChanges();
                TempData["Message"] = "Hủy đơn hàng thành công.";
            }
            else
            {
                TempData["Message"] = "Không thể hủy đơn hàng.";
            }

            return RedirectToAction("DonMua", "DonHang", new { page });
        }


        [RequiredLogin]
        [HttpGet]
        public async Task<IActionResult> XemChiTiet(int id)
        {
            ViewBag.TieuDe = "Chi tiết đơn hàng";
            var donhang = _context.ChiTietDonHang
                .Where(p => p.MaDonHang == id)
                .Select(dh => new Chitietdh
                {
                    MaSanPham = _context.ChiTietSanPham
                        .Where(ct => ct.MaChiTietSP == dh.MaChiTietSP)
                        .Select(ct => ct.MaSanPham)
                        .FirstOrDefault(),

                    GiaTien = _context.ChiTietSanPham
                        .Where(ct => ct.MaChiTietSP == dh.MaChiTietSP)
                        .Select(ct => ct.Gia)
                        .FirstOrDefault(),

                    GiaGiam = _context.ChiTietSanPham
                        .Where(ct => ct.MaChiTietSP == dh.MaChiTietSP)
                        .Select(ct => ct.GiaGiam)
                        .FirstOrDefault(),

                    TenSanPham = (from ct in _context.ChiTietSanPham
                                  join sp in _context.SanPham on ct.MaSanPham equals sp.MaSanPham
                                  where ct.MaChiTietSP == dh.MaChiTietSP
                                  select sp.TenSanPham).FirstOrDefault(),

                    SoLuong = dh.SoLuong,
                    TongTien = dh.TongTien
                })
                .ToList();
            int mataikhoan = int.Parse(HttpContext.Session.GetString("MaTaiKhoan"));

            var dg = _context.DanhGia.ToList();

            var don = _context.DonHang.Where(dh => dh.MaDonHang == id).FirstOrDefault();
            decimal sotienduocgiam = 0;
            var giatrigiam = _context.MaGiamGia.Where(gg => gg.MMaGiamGia == don.MMaGiamGia).FirstOrDefault();
            if (giatrigiam != null)
            {
                sotienduocgiam = giatrigiam.LoaiGiamGia.Contains("Giảm theo %") ? ((decimal)giatrigiam.GiaTri / 100) * don.TongTien : (decimal)giatrigiam.GiaTri;
            }
            var diachi = _context.DiaChi.Where(dc => dc.MaDiaChi == don.MaDiaChi).FirstOrDefault();

            var huyen1 = await _locationService.GetDistrictNameByIdAsync(int.Parse(diachi.Huyen));
            var xa1 = await _locationService.GetWardNameByIdAsync(int.Parse(diachi.Xa), int.Parse(diachi.Huyen));
            var tp1 = await _locationService.GetProvinceNameByIdAsync(int.Parse(diachi.ThanhPho));

            diachi.Huyen = huyen1; diachi.ThanhPho = tp1; diachi.Xa = xa1;

            var taikhoan = _context.TaiKhoan.Where(tk => tk.MaTaiKhoan == don.MaTaiKhoan).FirstOrDefault();

            var pttt = _context.PhuongThucThanhToan.Where(pt => pt.MaPhuongThuc == don.MaPhuongThucThanhToan).Select(pt => pt.TenPhuongThuc).FirstOrDefault();
            var nd = new NdChitiet
            {
                chitiet = donhang,
                id = id,
                dh = don,
                danhgia = dg,
                mataikhoan = mataikhoan,
                diachi = diachi,
                taikhoan = taikhoan,
                phuongthucthanhtoan = pttt,
                ttthanhtoan = don.TrangThaiThanhToan == true ? "Đã thanh toán" : "Chưa thanh toán",
                giamgia = sotienduocgiam
            };

            return View(nd);
        }

        [RequiredLogin]
        [HttpGet]
        public IActionResult DanhGia(int id)
        {
            ViewBag.TieuDe = "Đánh giá sản phẩm";
            int mataikhoan = int.Parse(HttpContext.Session.GetString("MaTaiKhoan"));
            ViewBag.MaTaiKhoan = mataikhoan;
            var sp = _context.SanPham.Where(sp => sp.MaSanPham == id).FirstOrDefault();
            return View(sp);
        }
        [HttpPost]
        public IActionResult DanhGia([FromBody] DanhGiaForm danhgia)
        {
            try
            {
                var dg = _context.DanhGia
                    .FirstOrDefault(dg => dg.MaTaiKhoan == danhgia.MaTaiKhoan && dg.MaSanPham == danhgia.MaSanPham);

                if (dg == null)
                {
                    var danhg = new DanhGia
                    {
                        MaSanPham = danhgia.MaSanPham,
                        SoDiem = danhgia.SoDiem,
                        MoTa = danhgia.MoTa,
                        MaTaiKhoan = danhgia.MaTaiKhoan,
                        NgayCapNhat = DateTime.Now
                    };
                    _context.DanhGia.Add(danhg);
                    _context.SaveChanges();
                }

                return Ok(new { message = "Đánh giá thành công", redirectUrl = Url.Action("DonMua", "DonHang") });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Có lỗi xảy ra khi xử lý đánh giá." });
            }
        }
        [HttpGet]
        [RequiredLogin]
        public async Task<IActionResult> XemHoaDon(int id)
        {
            string? role = HttpContext.Session.GetString("Role");
            if (role != null && (role.Contains("0") || role.Contains("1")))
            {
                ViewBag.TieuDe = "Xuất hóa đơn";
                var donhang = _context.ChiTietDonHang
                   .Where(p => p.MaDonHang == id)
                   .Select(dh => new Chitietdh
                   {
                       MaSanPham = _context.ChiTietSanPham
                           .Where(ct => ct.MaChiTietSP == dh.MaChiTietSP)
                           .Select(ct => ct.MaSanPham)
                           .FirstOrDefault(),

                       GiaTien = _context.ChiTietSanPham
                           .Where(ct => ct.MaChiTietSP == dh.MaChiTietSP)
                           .Select(ct => ct.Gia)
                           .FirstOrDefault(),

                       GiaGiam = _context.ChiTietSanPham
                           .Where(ct => ct.MaChiTietSP == dh.MaChiTietSP)
                           .Select(ct => ct.GiaGiam)
                           .FirstOrDefault(),

                       TenSanPham = (from ct in _context.ChiTietSanPham
                                     join sp in _context.SanPham on ct.MaSanPham equals sp.MaSanPham
                                     where ct.MaChiTietSP == dh.MaChiTietSP
                                     select sp.TenSanPham).FirstOrDefault(),

                       SoLuong = dh.SoLuong,
                       TongTien = dh.TongTien
                   })
                   .ToList();
                int mataikhoan = int.Parse(HttpContext.Session.GetString("MaTaiKhoan"));

                var dg = _context.DanhGia.ToList();

                var don = _context.DonHang.Where(dh => dh.MaDonHang == id).FirstOrDefault();
                decimal sotienduocgiam = 0;
                var giatrigiam = _context.MaGiamGia.Where(gg => gg.MMaGiamGia == don.MMaGiamGia).FirstOrDefault();
                if (giatrigiam != null)
                {
                    sotienduocgiam = giatrigiam.LoaiGiamGia.Contains("Giảm theo %") ? ((decimal)giatrigiam.GiaTri / 100) * don.TongTien : (decimal)giatrigiam.GiaTri;
                }
                var diachi = _context.DiaChi.Where(dc => dc.MaDiaChi == don.MaDiaChi).FirstOrDefault();

                var huyen1 = await _locationService.GetDistrictNameByIdAsync(int.Parse(diachi.Huyen));
                var xa1 = await _locationService.GetWardNameByIdAsync(int.Parse(diachi.Xa), int.Parse(diachi.Huyen));
                var tp1 = await _locationService.GetProvinceNameByIdAsync(int.Parse(diachi.ThanhPho));

                diachi.Huyen = huyen1; diachi.ThanhPho = tp1; diachi.Xa = xa1;

                var taikhoan = _context.TaiKhoan.Where(tk => tk.MaTaiKhoan == don.MaTaiKhoan).FirstOrDefault();

                var pttt = _context.PhuongThucThanhToan.Where(pt => pt.MaPhuongThuc == don.MaPhuongThucThanhToan).Select(pt => pt.TenPhuongThuc).FirstOrDefault();
                var nd = new NdChitiet
                {
                    chitiet = donhang,
                    id = id,
                    dh = don,
                    danhgia = dg,
                    mataikhoan = mataikhoan,
                    diachi = diachi,
                    taikhoan = taikhoan,
                    phuongthucthanhtoan = pttt,
                    ttthanhtoan = don.TrangThaiThanhToan == true ? "Đã thanh toán" : "Chưa thanh toán",
                    giamgia = sotienduocgiam
                };
                ViewBag.TongTienChu = NumberToText.DocTien((long)(nd.dh.TongTien + 30000 - nd.giamgia));
                ViewBag.TongTienSo = nd.dh.TongTien + 30000 - nd.giamgia;
                return View(nd);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        public static class NumberToText
        {
            private static readonly string[] ChuSo = { "không", "một", "hai", "ba", "bốn", "năm", "sáu", "bảy", "tám", "chín" };

            private static string DocHangChuc(int number, bool daydu)
            {
                string result = "";
                int chuc = number / 10;
                int donvi = number % 10;

                if (chuc > 1)
                {
                    result = " " + ChuSo[chuc] + " mươi";
                    if (donvi == 1) result += " mốt";
                }
                else if (chuc == 1)
                {
                    result = " mười";
                    if (donvi == 1) result += " một";
                }
                else if (daydu && donvi > 0) result = " lẻ";

                if (donvi == 5 && chuc >= 1)
                {
                    result += " lăm";
                }
                else if (donvi > 0 && !(donvi == 1 && (chuc == 1 || chuc == 0)))
                {
                    result += " " + ChuSo[donvi];
                }

                return result;
            }

            private static string DocHangTram(int number, bool daydu)
            {
                int tram = number / 100;
                int chuc = number % 100;
                string result = "";

                if (daydu || tram > 0)
                {
                    result = " " + ChuSo[tram] + " trăm";
                    result += DocHangChuc(chuc, true);
                }
                else
                {
                    result = DocHangChuc(chuc, false);
                }

                return result;
            }

            public static string DocTien(long number)
            {
                if (number == 0) return "Không đồng";

                string[] donvi = { "", " nghìn", " triệu", " tỷ", " nghìn tỷ", " triệu tỷ" };
                string result = "";
                int i = 0;

                while (number > 0)
                {
                    int n = (int)(number % 1000);
                    if (n != 0)
                    {
                        string str = DocHangTram(n, i > 0);
                        result = str + donvi[i] + result;
                    }
                    number /= 1000;
                    i++;
                }

                result = result.Trim().Replace("không trăm", "").Replace("lẻ", "");

                result = char.ToUpper(result[0]) + result.Substring(1) + " đồng";
                return result;
            }
        }
    }
}