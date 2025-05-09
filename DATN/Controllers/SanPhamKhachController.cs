using CoreLib.AppDbContext;
using CoreLib.DTO;
using CoreLib.Entity;
using Lib_Core.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SANPHAM.Authorize;

namespace DATN.Controllers
{
    public class SanPhamKhachController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly AppDbContext _context;
        public SanPhamKhachController(AppDbContext context, ILogger<HomeController> logger)
        {
            _context = context;
            _logger = logger;
        }
        public IActionResult Index()
        {
            ViewBag.TieuDe = "Sản phẩm";
            return View();
        }
        [HttpPost]
        public IActionResult XoaYeuThich(int maSanPham)
        {
            try
            {
                int maTaiKhoan = int.Parse(HttpContext.Session.GetString("MaTaiKhoan"));
                var item = _context.YeuThich
                    .FirstOrDefault(x => x.MaSanPham == maSanPham && x.MaTaiKhoan == maTaiKhoan);

                if (item != null)
                {
                    _context.YeuThich.Remove(item);
                    _context.SaveChanges();

                    int quantity = _context.YeuThich.Count(y => y.MaTaiKhoan == maTaiKhoan);
                    return Json(new { code = 0, message = "Đã xóa khỏi danh sách yêu thích!", soluong = quantity });
                }
                else
                {
                    return Json(new { code = 1, message = "Không tìm thấy sản phẩm trong yêu thích." });
                }
            }
            catch (Exception ex)
            {
                return Json(new { code = 500, message = "Lỗi khi xóa sản phẩm yêu thích." });
            }
        }

        [HttpGet]
        [RequiredLogin]
        public IActionResult YeuThich()
        {
            ViewBag.TieuDe = "Sản phẩm yêu thích";
            int ma = int.Parse(HttpContext.Session.GetString("MaTaiKhoan"));
            var list = _context.YeuThich.Where(yt=>yt.MaTaiKhoan == ma).ToList();
            var danhsach = list.Select(sp => new SanPhamDTO
            {
                MaSanPham = sp.MaSanPham,
                TenSanPham = _context.SanPham.Where(spm => spm.MaSanPham == sp.MaSanPham).Select(spm => spm.TenSanPham).FirstOrDefault(),
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
            }).ToList();
            var quantity_yeuthich = _context.YeuThich.Where(ctg => ctg.MaTaiKhoan == ma).Count();
            HttpContext.Session.SetInt32("SoLuongYeuThich", quantity_yeuthich);
            return View(danhsach);
        }
        [HttpGet]
        [RequiredLogin]
        public IActionResult Themyeuthich(int id)
        {
            try
            {
                var maStr = HttpContext.Session.GetString("MaTaiKhoan");
                if (string.IsNullOrEmpty(maStr))
                {
                    return Json(new { code = -1, message = "Vui lòng đăng nhập!", url = Url.Action("DangNhap", "TaiKhoan") });
                }

                int ma = int.Parse(maStr);

                // Kiểm tra nếu sản phẩm đã có trong danh sách yêu thích
                bool exists = _context.YeuThich
                    .Any(yt => yt.MaSanPham == id && yt.MaTaiKhoan == ma);

                if (exists)
                {
                    return Json(new { code = 1, message = "Sản phẩm đã có trong yêu thích!" });
                }

                // Thêm vào bảng yêu thích
                var yeuthich = new YeuThich
                {
                    MaSanPham = id,
                    MaTaiKhoan = ma,
                    NgayCapNhat = DateTime.Now
                };

                _context.YeuThich.Add(yeuthich);
                _context.SaveChanges();

                // Lấy lại tổng số yêu thích của tài khoản này
                int quantity = _context.YeuThich
                    .Count(y => y.MaTaiKhoan == ma);

                return Json(new { code = 0, message = "Đã thêm vào yêu thích!", soluong = quantity });
            }
            catch (Exception ex)
            {
                return Json(new { code = 500, message = "Lỗi máy chủ"});
            }
        }


        public IActionResult ChiTietSP(int masp)
        {
            ViewBag.TieuDe = "Sản phẩm » Chi tiết sản phẩm ";
            ViewBag.View = "Index";
            ViewBag.Controller = "SanPhamKhach";
            HttpContext.Session.SetInt32("MaSanPham", masp);
            var sp = _context.SanPham.FirstOrDefault(sp => sp.MaSanPham == masp);

            if (sp == null)
            {
                return NotFound();
            }

            var dsachanhsp = _context.AnhSanPham
                .Where(anhsp => anhsp.MaSanPham == sp.MaSanPham)
                .ToList();

            var chitiet = _context.ChiTietSanPham
                .Where(ct => ct.MaSanPham == masp)
                .Include(ct => ct.KichThuoc).OrderBy(kt => kt.MaKichThuoc)
                .ToList();

            var dsanh = new List<Anh>();
            foreach (AnhSanPham anhsp in dsachanhsp)
            {
                var anh = _context.Anh.FirstOrDefault(a => a.MaAnh == anhsp.MaAnh);
                if (anh != null)
                {
                    dsanh.Add(anh);
                }
            }
            var nhomhuong = _context.NhomHuong.Where(nh => nh.MaNhomHuong == sp.MaNhomHuong).FirstOrDefault();
            var thuonghieu = _context.ThuongHieu.Where(nh => nh.MaThuongHieu == sp.MaThuongHieu).FirstOrDefault();
            var anhdau = dsanh.FirstOrDefault();
            var giaThapNhat = chitiet.Min(ct => ct.Gia);
            var giaCaoNhat = chitiet.Max(ct => ct.Gia);

            var result = new ChiTiet
            {
                anhdau = anhdau,
                anhs = dsanh,
                sanpham = sp,
                chitietsanpham = chitiet,
                GiaThapNhat = giaThapNhat,
                GiaCaoNhat = giaCaoNhat,
                thuonghieu = thuonghieu.TenThuongHieu,
                nhomhuong = nhomhuong.TenNhomHuong,
                xuatxu = thuonghieu.QuocGia
            };

            var dssp1 = _context.SanPham.Where(sp1 => sp1.MaNhomHuong == sp.MaNhomHuong).ToList();
            var result1 = new List<SanPhamDTO>();
            foreach (var sp1 in dssp1)
            {
                var dsachanhsp_nhomhuong = _context.AnhSanPham
               .Where(anhsp => anhsp.MaSanPham == sp1.MaSanPham)
               .ToList();

                var chitiet_nhomhuong = _context.ChiTietSanPham
                    .Where(ct => ct.MaSanPham == sp1.MaSanPham)
                    .Include(ct => ct.KichThuoc).OrderBy(kt => kt.MaKichThuoc)
                    .ToList();

                var dsanh_nhomhuong = new List<Anh>();
                foreach (AnhSanPham anhsp in dsachanhsp_nhomhuong)
                {
                    var anh = _context.Anh.FirstOrDefault(a => a.MaAnh == anhsp.MaAnh);
                    if (anh != null)
                    {
                        dsanh_nhomhuong.Add(anh);
                    }
                }
                var nhomhuong_nhomhuong = _context.NhomHuong.Where(nh => nh.MaNhomHuong == sp1.MaNhomHuong).FirstOrDefault();
                var thuonghieu_nhomhuong = _context.ThuongHieu.Where(nh => nh.MaThuongHieu == sp1.MaThuongHieu).FirstOrDefault();
                var anhdau_nhomhuong = dsanh.FirstOrDefault();
                var giaThapNhat_nhomhuong = chitiet_nhomhuong.Min(ct => ct.Gia);
                var giaCaoNhat_nhomhuong = chitiet_nhomhuong.Max(ct => ct.Gia);

                var rs = new SanPhamDTO
                {
                    MaSanPham = sp1.MaSanPham,
                    TenSanPham = sp1.TenSanPham,
                    AnhDauTien = dsanh_nhomhuong.Select(dsa => dsa.URL).FirstOrDefault(),
                    GiaDauTien = chitiet_nhomhuong.Select(gdt => gdt.Gia).FirstOrDefault(),
                    GiaGiamDauTien = chitiet_nhomhuong.Select(gdt => gdt.GiaGiam).FirstOrDefault(),
                };
                result1.Add(rs);
            }

            var dssp2 = _context.SanPham.Where(sp1 => sp1.GioiTinh == sp.GioiTinh).ToList();

            var result2 = new List<SanPhamDTO>();
            foreach (var sp2 in dssp1)
            {
                var dsachanhsp_gioitinh = _context.AnhSanPham
               .Where(anhsp => anhsp.MaSanPham == sp2.MaSanPham)
               .ToList();

                var chitiet_gioitinh = _context.ChiTietSanPham
                    .Where(ct => ct.MaSanPham == sp2.MaSanPham)
                    .Include(ct => ct.KichThuoc).OrderBy(kt => kt.MaKichThuoc)
                    .ToList();

                var dsanh_gioitinh = new List<Anh>();
                foreach (AnhSanPham anhsp in dsachanhsp_gioitinh)
                {
                    var anh = _context.Anh.FirstOrDefault(a => a.MaAnh == anhsp.MaAnh);
                    if (anh != null)
                    {
                        dsanh_gioitinh.Add(anh);
                    }
                }
                var nhomhuong_gioitinh = _context.NhomHuong.Where(nh => nh.MaNhomHuong == sp2.MaNhomHuong).FirstOrDefault();
                var thuonghieu_gioitinh = _context.ThuongHieu.Where(nh => nh.MaThuongHieu == sp2.MaThuongHieu).FirstOrDefault();
                var anhdau_gioitinh = dsanh.FirstOrDefault();
                var giaThapNhat_gioitinh = chitiet_gioitinh.Min(ct => ct.Gia);
                var giaCaoNhat_gioitinh = chitiet_gioitinh.Max(ct => ct.Gia);

                var rs = new SanPhamDTO
                {
                    MaSanPham = sp2.MaSanPham,
                    TenSanPham = sp2.TenSanPham,
                    AnhDauTien = dsanh_gioitinh.Select(dsa => dsa.URL).FirstOrDefault(),
                    GiaDauTien = chitiet_gioitinh.Select(gdt => gdt.Gia).FirstOrDefault(),
                    GiaGiamDauTien = chitiet_gioitinh.Select(gdt => gdt.GiaGiam).FirstOrDefault(),
                };
                result1.Add(rs);
            }
            var dssp3 = _context.SanPham.Where(sp1 => sp1.MaThuongHieu == sp.MaThuongHieu).ToList();
            var result3 = new List<SanPhamDTO>();
            foreach (var sp3 in dssp1)
            {
                var dsachanhsp_thuonghieu = _context.AnhSanPham
               .Where(anhsp => anhsp.MaSanPham == sp3.MaSanPham)
               .ToList();

                var chitiet_thuonghieu = _context.ChiTietSanPham
                    .Where(ct => ct.MaSanPham == sp3.MaSanPham)
                    .Include(ct => ct.KichThuoc).OrderBy(kt => kt.MaKichThuoc)
                    .ToList();

                var dsanh_thuonghieu = new List<Anh>();
                foreach (AnhSanPham anhsp in dsachanhsp_thuonghieu)
                {
                    var anh = _context.Anh.FirstOrDefault(a => a.MaAnh == anhsp.MaAnh);
                    if (anh != null)
                    {
                        dsanh_thuonghieu.Add(anh);
                    }
                }
                var nhomhuong_thuonghieu = _context.NhomHuong.Where(nh => nh.MaNhomHuong == sp3.MaNhomHuong).FirstOrDefault();
                var thuonghieu_thuonghieu = _context.ThuongHieu.Where(nh => nh.MaThuongHieu == sp3.MaThuongHieu).FirstOrDefault();
                var anhdau_thuonghieu = dsanh.FirstOrDefault();
                var giaThapNhat_thuonghieu = chitiet_thuonghieu.Min(ct => ct.Gia);
                var giaCaoNhat_thuonghieu = chitiet_thuonghieu.Max(ct => ct.Gia);

                var rs = new SanPhamDTO
                {
                    MaSanPham = sp3.MaSanPham,
                    TenSanPham = sp3.TenSanPham,
                    AnhDauTien = dsanh_thuonghieu.Select(dsa => dsa.URL).FirstOrDefault(),
                    GiaDauTien = chitiet_thuonghieu.Select(gdt => gdt.Gia).FirstOrDefault(),
                    GiaGiamDauTien = chitiet_thuonghieu.Select(gdt => gdt.GiaGiam).FirstOrDefault(),
                };
                result1.Add(rs);
            }
            decimal diemdanhgia = 0;
            if (_context.DanhGia.Where(dg => dg.MaSanPham == masp).ToList().Count() > 0)
            {
                diemdanhgia = (decimal)_context.DanhGia.Where(dg => dg.MaSanPham == masp).Average(dg => dg.SoDiem);
            }
            var dsdg = _context.DanhGia.Where(dg => dg.MaSanPham == masp).ToList();
            var dsdanhgia = new List<Hienthidanhgia>();
            foreach (var itemm in dsdg)
            {
                var dg = new Hienthidanhgia
                {
                    MaSanPham = masp,
                    MaTaiKhoan = itemm.MaTaiKhoan,
                    SoDiem = itemm.SoDiem,
                    MoTa = itemm.MoTa,
                    NgayDanhGia = itemm.NgayCapNhat,
                    TenKhachHang = _context.TaiKhoan.Where(tk => tk.MaTaiKhoan == itemm.MaTaiKhoan).Select(tk => tk.HoTen).FirstOrDefault()
                };
                dsdanhgia.Add(dg);
            }
            var hienthi = new HienThiChiTiet
            {
                spchinh = result,
                dslienquan = result1.Where(sp => sp.MaSanPham != masp).DistinctBy(sp => sp.MaSanPham).Take(4).ToList(),
                diemtrungbinh = diemdanhgia,
                danhgias = dsdanhgia.Take(5).ToList()
            };
            return View(hienthi);
        }


    }
}
