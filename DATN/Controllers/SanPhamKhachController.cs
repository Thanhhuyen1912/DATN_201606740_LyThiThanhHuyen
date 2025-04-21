using CoreLib.AppDbContext;
using CoreLib.DTO;
using CoreLib.Entity;
using Lib_Core.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
