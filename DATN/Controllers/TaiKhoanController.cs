using CoreLib.AppDbContext;
using CoreLib.Entity;
using CoreLib.DTO;
using Microsoft.AspNetCore.Mvc;
using SANPHAM.Authorize;

namespace DATN.Controllers
{
    public class TaiKhoanController : Controller
    {
        private readonly AppDbContext _context;
        private readonly ILocationService _locationService;
        public TaiKhoanController(AppDbContext context, ILocationService locationService)
        {
            _context = context;
            _locationService = locationService;
        }
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
        [HttpGet]
        public IActionResult DangNhap()
        {
            return View();
        }
        [HttpGet]
        public IActionResult DangKy()
        {
            return View();
        }

        [HttpPost]
        public IActionResult DangNhap(string user, string matkhau)
        {
            var tk = _context.TaiKhoan.FirstOrDefault(u => u.Email == user && u.MatKhau == matkhau);

            if (tk != null)
            {
                HttpContext.Session.SetString("Role", tk.LoaiTaiKhoan.ToString() ?? "");
                HttpContext.Session.SetString("MaTaiKhoan", tk.MaTaiKhoan.ToString());
                HttpContext.Session.SetString("User", tk.HoTen.ToString() ?? "");
                string? returnUrl = HttpContext.Session.GetString("ReturnUrl");
                if (returnUrl != null)
                {
                    HttpContext.Session.Remove("ReturnUrl");
                    return Redirect(returnUrl);
                }

                return RedirectToAction("Index", "Home");
            }

            ViewBag.ThongBao = "Email hoặc mật khẩu không đúng!";
            return View();
        }
        public IActionResult DangXuat()
        {
            HttpContext.Session.Remove("Role");
            HttpContext.Session.Remove("MaTaiKhoan");
            HttpContext.Session.Remove("User");
            return RedirectToAction("Index", "Home");
        }
        [HttpPost]
        public IActionResult DangKy(string hoten, string sodienthoai, string email, string matkhau)
        {
            try
            {
                var Tk = new TaiKhoan();
                Tk.TrangThai = true;
                Tk.MatKhau = matkhau;
                Tk.Email = email;
                Tk.HoTen = hoten;
                Tk.NgayCapNhat = DateTime.Now;
                Tk.LoaiTaiKhoan = 2;
                Tk.SoDienThoai = sodienthoai;
                _context.TaiKhoan.Add(Tk);
                _context.SaveChanges();
                TempData["LoginMessage1"] = "Đăng ký thành công!";
                return RedirectToAction("DangNhap", "TaiKhoan");
            }
            catch (Exception ex)
            {

                ViewBag.ThongBao = "Thông tin tài khoản chưa hợp lệ" + ex;
                return View();
            }
        }
        [HttpGet]
        public async Task<IActionResult> QuanLyTK(string id)
        {
            if (!int.TryParse(id, out int maTaiKhoan))
            {
                return RedirectToAction("DangNhap", "TaiKhoan");
            }

            var taiKhoan = _context.TaiKhoan.FirstOrDefault(t => t.MaTaiKhoan == maTaiKhoan);
            var diaChis = _context.DiaChi.Where(d => d.MaTaiKhoan == maTaiKhoan).ToList();

            // Duyệt qua danh sách địa chỉ và thêm tên tỉnh/huyện/xã
            var diaChiViewModels = new List<DiaChi>();
            foreach (var diaChi in diaChis)
            {
                var tinh = await _locationService.GetProvinceNameByIdAsync(int.Parse(diaChi.ThanhPho));
                var huyen = await _locationService.GetDistrictNameByIdAsync(int.Parse(diaChi.Huyen));
                var xa = await _locationService.GetWardNameByIdAsync(int.Parse(diaChi.Xa), int.Parse(diaChi.Huyen));

                diaChiViewModels.Add(new DiaChi
                {
                    ChiTiet = diaChi.ChiTiet,
                    ThanhPho = tinh,
                    Huyen = huyen,
                    Xa = xa,
                    SoDienThoaiNguoiNhan = diaChi.SoDienThoaiNguoiNhan,
                    HoTenNguoiNhan = diaChi.HoTenNguoiNhan,
                    MaDiaChi = diaChi.MaDiaChi

                });
            }

            var viewModel = new ThongTinTaiKhoanViewModel
            {
                TaiKhoan = taiKhoan,
                DiaChis = diaChiViewModels
            };

            return View(viewModel);
        }

        public IActionResult ChiTietTK(string id)
        {
            if (!int.TryParse(id, out int maTaiKhoan))
            {
                return RedirectToAction("QuanLyTK", "TaiKhoan", new { id = id });
            }
            var taikhoan = _context.TaiKhoan.FirstOrDefault(p => p.MaTaiKhoan == maTaiKhoan);
            return View(taikhoan);
        }
        [HttpPost]
        public IActionResult CapNhatTaiKhoan(string HoTen, string Email, string SoDienThoai, string MatKhau)
        {
            string? ma = HttpContext.Session.GetString("MaTaiKhoan");
            if (ma != null)
            {
                var tk = _context.TaiKhoan.FirstOrDefault(p => p.MaTaiKhoan == int.Parse(ma));
                try
                {
                    if (tk != null)
                    {
                        tk.HoTen = HoTen;
                        tk.Email = Email;
                        tk.SoDienThoai = SoDienThoai;
                        tk.MatKhau = MatKhau;

                        _context.SaveChanges();
                    }
                    return RedirectToAction("QuanLyTK", "TaiKhoan", new { id = int.Parse(ma) });

                }
                catch
                {
                    return RedirectToAction("ChiTietTK", "TaiKhoan", new { id = int.Parse(ma) });

                }
            }
            else return RedirectToAction("Index", "Home");

        }

    }
}
