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
            return View();
        }

        [HttpGet]
        [RequiredLogin]
        public IActionResult YeuThich()
        {
            int ma = int.Parse(HttpContext.Session.GetString("MaTaiKhoan"));
            var list = _context.YeuThich.ToList();
            var danhsach = list.Select(sp => new SanPhamDTO
   {
       MaSanPham = sp.MaSanPham,
       TenSanPham = _context.SanPham.Where(spm=>spm.MaSanPham == sp.MaSanPham).Select(spm=>spm.TenSanPham).FirstOrDefault(),
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
            return View(danhsach);
        }
        [HttpGet]
        [RequiredLogin]
        public IActionResult Themyeuthich(int id)
        {
            int ma = int.Parse(HttpContext.Session.GetString("MaTaiKhoan"));
            if (ma > 0)
            {
                // Check sản phẩm đã tồn tại trong bảng yêu thích hay chưa
                var exists = _context.YeuThich
                    .Any(yt => yt.MaSanPham == id && yt.MaTaiKhoan == ma);

                if (!exists)
                {
                    var yeuthich = new YeuThich
                    {
                        MaSanPham = id,
                        MaTaiKhoan = ma,
                        NgayCapNhat = DateTime.Now
                    };

                    _context.YeuThich.Add(yeuthich);
                    _context.SaveChanges();
                    TempData["Message"] = "Đã thêm vào yêu thích";
                }
                else
                {
                    TempData["Message"] = "Sản phẩm này đã có trong yêu thích";
                }

                return RedirectToAction("Index");
            }
            else
            {
                TempData["Message"] = "Vui lòng đăng nhập!";
                return RedirectToAction("DangNhap", "TaiKhoan");
            }
        }

        public IActionResult ChiTietSP(int masp)
        {
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
                .Include(ct=>ct.KichThuoc).OrderBy(kt=>kt.MaKichThuoc)
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

            return View(result);
        }


    }
}
