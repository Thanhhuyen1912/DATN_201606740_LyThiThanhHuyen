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
        [HttpGet]
        public IActionResult ThemDiaChi()
        {
            string? ma = HttpContext.Session.GetString("MaTaiKhoan");
            if (ma != null)
            {
                var taikhoan = _context.TaiKhoan.FirstOrDefault(p => p.MaTaiKhoan == int.Parse(ma));
                return View(taikhoan);
            }
            else
            {
                return RedirectToAction("QuanLyTK", "TaiKhoan", new { id = ma });
            }


        }
        [HttpPost]
        public IActionResult ThemDiaChi(string HoTenNguoiNhan, string SoDienThoaiNguoiNhan, string Huyen, string Xa, string ChiTiet, string ThanhPho)
        {
            string? ma = HttpContext.Session.GetString("MaTaiKhoan");
            if (ma != null)
            {
                var taikhoan = _context.TaiKhoan.FirstOrDefault(p => p.MaTaiKhoan == int.Parse(ma));
                var dc = new DiaChi();
                dc.MaTaiKhoan = int.Parse(ma);
                dc.ThanhPho = ThanhPho;
                dc.Xa = Xa;
                dc.HoTenNguoiNhan = HoTenNguoiNhan;
                dc.SoDienThoaiNguoiNhan = SoDienThoaiNguoiNhan;
                dc.Huyen = Huyen;
                dc.ChiTiet = ChiTiet;
                dc.NgayCapNhat = DateTime.Now;

                _context.DiaChi.Add(dc);
                _context.SaveChanges();

                TempData["Message"] = "Thêm địa chỉ thành công!";
                TempData["matk"] = ma;
            }

            return View();

        }
        [HttpGet]
        public IActionResult CapNhatDiaChi(int id)
        {
            string? ma = HttpContext.Session.GetString("MaTaiKhoan");
            if (ma != null)
            {
                var nd = new NoidungCapnhat
                {
                    taikhoan = _context.TaiKhoan.FirstOrDefault(p => p.MaTaiKhoan == int.Parse(ma)),
                    diachi = _context.DiaChi.FirstOrDefault(p=> p.MaDiaChi == id)
                };
                
                return View(nd);
            }
            else
            {
                return RedirectToAction("QuanLyTK", "TaiKhoan", new { id = ma });
            }
        }
        [HttpPost]
        public IActionResult CapNhatDiaChi(int MaDiaChi, string ChiTiet, string Xa, string Huyen, string ThanhPho , string SoDienThoaiNguoiNhan , string HoTenNguoiNhan)
        {
            string? ma = HttpContext.Session.GetString("MaTaiKhoan");
            var diachi = _context.DiaChi.FirstOrDefault(p => p.MaDiaChi == MaDiaChi);
            if (diachi != null)
            {
                diachi.ThanhPho = ThanhPho;
                diachi.Huyen = Huyen;
                diachi.Xa = Xa;
                diachi.ChiTiet = ChiTiet;
                diachi.SoDienThoaiNguoiNhan = SoDienThoaiNguoiNhan;
                diachi.HoTenNguoiNhan = HoTenNguoiNhan;

                _context.SaveChanges();
                return RedirectToAction("QuanLyTK", "TaiKhoan", new { id = ma });
            }
            else
            {
                return View();
            } 
                
        }

        [HttpGet]
        public IActionResult XoaDiaChi(int id)
        {
            string? ma = HttpContext.Session.GetString("MaTaiKhoan");
            var diachi = _context.DiaChi.FirstOrDefault(p => p.MaDiaChi == id);
            if (diachi != null)
            {
               _context.DiaChi.Remove(diachi);
                _context.SaveChanges();
                return RedirectToAction("QuanLyTK", "TaiKhoan", new { id = ma });
            }
            else
            {
                return View();
            }

        }
        [HttpPost]
        public IActionResult DangNhap(string user, string matkhau)
        {
            var tk = _context.TaiKhoan.FirstOrDefault(u => u.Email == user && u.MatKhau == matkhau);

            if (tk != null && tk.TrangThai == true)
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

                ViewBag.ThongBao = "Thông tin tài khoản chưa hợp lệ";
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
            if (taikhoan != null)
                ViewBag.MatKhau = taikhoan.MatKhau;
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

                        return Json(new
                        {
                            success = true,
                            message = "Cập nhật thông tin thành công!",
                            redirectUrl = Url.Action("QuanLyTK", "TaiKhoan", new { id = tk.MaTaiKhoan })
                        });
                    }
                    return Json(new { success = false, message = "Tài khoản không tồn tại!" });
                }
                catch
                {
                    return Json(new { success = false, message = "Lỗi khi cập nhật tài khoản!" });
                }
            }
            return Json(new { success = false, message = "Không tìm thấy phiên đăng nhập!" });
        }
    }
}
