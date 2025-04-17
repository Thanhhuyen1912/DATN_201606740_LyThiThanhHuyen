using CoreLib.AppDbContext;
using CoreLib.DTO;
using CoreLib.Entity;
using Lib_Core.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static System.Net.Mime.MediaTypeNames;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SanPhamController : Controller
    {
        private readonly AppDbContext _context;

        public SanPhamController(AppDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        [Route("/SanPham/Danhsach")]
        public IActionResult Danhsach()
        {
            var sanPhams = _context.SanPham.Select(sp => new
            {
                sp.MaSanPham,
                sp.TenSanPham,
                sp.GioiTinh,
                sp.TrangThai,
                sp.MaNhomHuong,
                sp.MaThuongHieu,
                TenNhomHuong = _context.NhomHuong
                    .Where(nh => nh.MaNhomHuong == sp.MaNhomHuong)
                    .Select(nh => nh.TenNhomHuong)
                    .FirstOrDefault(),
                TenThuongHieu = _context.ThuongHieu
                    .Where(th => th.MaThuongHieu == sp.MaThuongHieu)
                    .Select(th => th.TenThuongHieu)
                    .FirstOrDefault()
            }).ToList();

            return Json(new { data = sanPhams });
        }
        [HttpPost]
        [Route("/SanPham/DanhsachSPKhach")]
        public IActionResult DanhsachKhach([FromBody] SearchSanPham1 request)
        {
            var anh = new Anh();
            var danhsachhienthi = new List<SanPhamDTO>();
            var query = _context.SanPham
            .Where(sp =>
            (string.IsNullOrEmpty(request.TenSanPham) || sp.TenSanPham.ToLower().Contains(request.TenSanPham.ToLower())) &&
            (string.IsNullOrEmpty(request.GioiTinh) || sp.GioiTinh == request.GioiTinh) &&
            (!request.MaThuongHieu.HasValue || sp.MaThuongHieu == request.MaThuongHieu) &&
            (!request.MaNhomHuong.HasValue || sp.MaNhomHuong == request.MaNhomHuong)
            ).ToList();

            foreach (var sp in query)
            {
                var CTSP = _context.ChiTietSanPham
                    .Where(p => p.MaSanPham == sp.MaSanPham && p.Gia >= request.GiaMin && p.Gia <= request.GiaMax)
                    .OrderBy(p => p.MaChiTietSP)
                    .FirstOrDefault();
                var anhSP = _context.AnhSanPham.Where(p => p.MaSanPham == sp.MaSanPham).FirstOrDefault();
                if (anhSP != null)
                {
                    anh = _context.Anh.Where(anh => anh.MaAnh == anhSP.MaAnh).FirstOrDefault();
                }
                if (CTSP != null)
                {
                    var giaDauTien = CTSP.Gia;
                    var giaGiamDauTien = CTSP.GiaGiam;
                    var spht = new SanPhamDTO
                    {
                        MaSanPham = sp.MaSanPham,
                        TenSanPham = sp.TenSanPham,
                        AnhDauTien = anh.URL,
                        GiaDauTien = CTSP.Gia,
                        GiaGiamDauTien = CTSP.GiaGiam
                    };

                    danhsachhienthi.Add(spht);
                }

            }           

            return Json(new { data = danhsachhienthi });
        }

        [HttpGet]
        [Route("/SanPham/DanhsachSPKhach1")]
        public IActionResult DanhsachKhach1()
        {
            var anh = new Anh();
            var danhsachhienthi = new List<SanPhamDTO>();
            var query = _context.SanPham.ToList();
            foreach (var sp in query)
            {
                var CTSP = _context.ChiTietSanPham
                    .Where(p => p.MaSanPham == sp.MaSanPham)
                    .OrderBy(p => p.MaChiTietSP)
                    .FirstOrDefault();
                var anhSP = _context.AnhSanPham.Where(p => p.MaSanPham == sp.MaSanPham).FirstOrDefault();
                if (anhSP != null)
                {
                    anh = _context.Anh.Where(anh => anh.MaAnh == anhSP.MaAnh).FirstOrDefault();
                }
                if (CTSP != null)
                {
                    var giaDauTien = CTSP.Gia;
                    var giaGiamDauTien = CTSP.GiaGiam;
                    var spht = new SanPhamDTO
                    {
                        MaSanPham = sp.MaSanPham,
                        TenSanPham = sp.TenSanPham,
                        AnhDauTien = anh.URL,
                        GiaDauTien = CTSP.Gia,
                        GiaGiamDauTien = CTSP.GiaGiam
                    };

                    danhsachhienthi.Add(spht);
                }

            }

            return Json(new { data = danhsachhienthi });
        }

        [HttpPost]
        [Route("/SanPham/ThemMoi")]
        public IActionResult Themmoi([FromBody] SanPham tk)
        {
            try
            {
                _context.SanPham.Add(tk);
                _context.SaveChanges();
                return Ok(new { code = 0, message = "Thêm SP thành công", data = tk.MaSanPham });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message, detail = ex.ToString() });
            }

        }
        [HttpPost]
        [Route("/SanPham/Sua")]
        public async Task<IActionResult> Sua([FromForm] SanPham tk,
                                      [FromForm] List<IFormFile> newImages,
                                      [FromForm] List<int> deletedImageIds)
        {
            try
            {
                // 1. Cập nhật thông tin sản phẩm
                _context.SanPham.Update(tk);

                // 2. Xóa các ảnh bị người dùng xóa
                // 2.1. Xóa bản ghi trong bảng AnhSanPham trước
                var anhSanPhamToDelete = _context.AnhSanPham
                    .Where(a => deletedImageIds.Contains(a.MaAnh)).ToList();
                _context.AnhSanPham.RemoveRange(anhSanPhamToDelete);

                // Gọi SaveChanges để đảm bảo không còn ràng buộc khi xóa ảnh
                await _context.SaveChangesAsync();

                // 2.2. Xóa bản ghi trong bảng Anh và file vật lý
                var imagesToDelete = _context.Anh
                    .Where(a => deletedImageIds.Contains(a.MaAnh)).ToList();

                foreach (var img in imagesToDelete)
                {
                    if (!string.IsNullOrEmpty(img.URL))
                    {
                        var filePath = Path.Combine("wwwroot", img.URL.TrimStart('/'));
                        if (System.IO.File.Exists(filePath))
                        {
                            System.IO.File.Delete(filePath);
                        }
                    }
                    _context.Anh.Remove(img);
                }

                // 3. Thêm ảnh mới
                foreach (var file in newImages)
                {
                    if (file != null && file.Length > 0)
                    {
                        var fileName = "SP_" + tk.MaSanPham + "_" + Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                        var savePath = Path.Combine("wwwroot", "images", "Products", fileName);

                        // Tạo thư mục nếu chưa có
                        var directory = Path.GetDirectoryName(savePath);
                        if (directory != null)
                        {
                            if (!Directory.Exists(directory))
                                Directory.CreateDirectory(directory);
                        }
                        using (var stream = new FileStream(savePath, FileMode.Create))
                        {
                            await file.CopyToAsync(stream);
                        }

                        var newAnh = new Anh
                        {
                            TenAnh = fileName,
                            URL = $"/images/Products/{fileName}",
                            NgayCapNhat = DateTime.Now
                        };

                        _context.Anh.Add(newAnh);
                        await _context.SaveChangesAsync();

                        var newAnhSP = new AnhSanPham
                        {
                            MaAnh = newAnh.MaAnh,
                            MaSanPham = tk.MaSanPham
                        };
                        _context.AnhSanPham.Add(newAnhSP);
                    }
                }

                await _context.SaveChangesAsync();

                return Ok(new { code = 0, message = "Cập nhật thành công" });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    code = 1,
                    message = "Lỗi: " + ex.Message +
                              (ex.InnerException != null ? " | Chi tiết: " + ex.InnerException.Message : "")
                });
            }
        }


        [HttpPut]
        [Route("/SanPham/Sua1")]
        public IActionResult sua1(ChiTietSanPham tk)
        {
            try
            {
                _context.ChiTietSanPham.Update(tk);
                _context.SaveChanges();
                return Ok(new { code = 0, message = "Sửa chi tiết SP thành công" });
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpGet]
        [Route("/SanPham/ChiTiet")]
        public IActionResult Chitiet(int masp)
        {
            try
            {
                var sp = _context.SanPham.FirstOrDefault(p => p.MaSanPham == masp);
                if (sp != null)
                    return Ok(new { code = 0, message = "Lấy chi tiết thành công", data = sp });
                else
                {
                    return BadRequest();
                }

            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpGet]
        [Route("/SanPham/ChiTietSP")]
        public IActionResult ChitietSP(int masp)
        {
            try
            {
                var chitietList = _context.ChiTietSanPham
                    .Where(p => p.MaSanPham == masp)
                    .Select(sp => new
                    {
                        sp.MaSanPham,
                        sp.MaChiTietSP,
                        sp.Gia,
                        sp.GiaGiam,
                        sp.TrangThai,
                        sp.SoLuong,
                        sp.NgayCapNhat,
                        TenSanPham = _context.SanPham
                            .Where(th => th.MaSanPham == sp.MaSanPham)
                            .Select(th => th.TenSanPham)
                            .FirstOrDefault(),
                        TenKichThuoc = _context.KichThuoc
                            .Where(nh => nh.MaKichThuoc == sp.MaKichThuoc)
                            .Select(nh => nh.TenKichThuoc)
                            .FirstOrDefault()
                    })
                    .ToList();


                return Ok(new { code = 0, message = "Lấy chi tiết thành công", data = chitietList });

            }
            catch
            {
                return StatusCode(500, new { code = -1, message = "Lỗi máy chủ" });
            }
        }

        [HttpGet]
        [Route("/SanPham/ChiTietSP1")]
        public IActionResult ChiTietSP1(int ct)
        {
            var result = _context.ChiTietSanPham.FirstOrDefault(p => p.MaChiTietSP == ct);

            if (result == null)
            {
                return NotFound();
            }

            return Ok(new { data = result });
        }
        [HttpPost]
        [Route("/SanPham/Themchitiet")]
        public IActionResult themchitiet(ChiTietSanPham ct)
        {
            try
            {
                _context.ChiTietSanPham.Add(ct);
                _context.SaveChanges();
                return Ok(new { message = "Thêm thành công" });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost]
        [Route("/SanPham/TimKiem")]
        public IActionResult TimKiemSanPham([FromBody] SearchSanPhamDTO dto)
        {
            var query = _context.SanPham.AsQueryable();

            if (!string.IsNullOrEmpty(dto.TenSanPham))
            {
                query = query.Where(p => p.TenSanPham.Contains(dto.TenSanPham));
            }

            if (dto.ThuongHieuId.HasValue)
            {
                query = query.Where(p => p.MaThuongHieu == dto.ThuongHieuId);
            }

            if (dto.NhomHuongId.HasValue)
            {
                query = query.Where(p => p.MaNhomHuong == dto.NhomHuongId);
            }

            if (!string.IsNullOrEmpty(dto.GioiTinh))
            {
                query = query.Where(p => p.GioiTinh == dto.GioiTinh);
            }

            if (dto.TrangThai.HasValue)
            {
                query = query.Where(p => p.TrangThai == dto.TrangThai.Value);
            }

            var result = query.Select(sp => new
            {
                sp.MaSanPham,
                sp.TenSanPham,
                sp.GioiTinh,
                sp.TrangThai,
                sp.MaNhomHuong,
                sp.MaThuongHieu,
                TenThuongHieu = _context.ThuongHieu
                    .Where(th => th.MaThuongHieu == sp.MaThuongHieu)
                    .Select(th => th.TenThuongHieu)
                    .FirstOrDefault(),
                TenNhomHuong = _context.NhomHuong
                    .Where(nh => nh.MaNhomHuong == sp.MaNhomHuong)
                    .Select(nh => nh.TenNhomHuong)
                    .FirstOrDefault()
            }).ToList();

            return Ok(new
            {
                code = 0,
                message = "Tìm kiếm thành công",
                data = result
            });
        }

        [HttpPost]
        [Route("/SanPham/TimKiem1")]
        public IActionResult TimKiemSanPham1([FromBody] SanPhamSearchDTO1 dto)
        {
            var query = _context.ChiTietSanPham.AsQueryable();


            if (dto.KichThuocID.HasValue)
            {
                query = query.Where(p => p.MaKichThuoc == dto.KichThuocID);
            }
            query = query.Where(p => p.MaSanPham == dto.MaSanPham);

            if (dto.Gia.HasValue)
            {
                query = query.Where(p => p.Gia == dto.Gia);
            }

            if (dto.GiaGiam.HasValue)
            {
                query = query.Where(p => p.GiaGiam == dto.GiaGiam);
            }

            if (dto.TrangThai.HasValue)
            {
                query = query.Where(p => p.TrangThai == dto.TrangThai.Value);
            }
            if (dto.NgayDau.HasValue)
            {
                query = query.Where(p => p.NgayCapNhat >= dto.NgayDau.Value);
            }
            if (dto.NgayCuoi.HasValue)
            {
                query = query.Where(p => p.NgayCapNhat <= dto.NgayCuoi.Value);
            }

            var result = query.Select(sp => new
            {
                sp.MaSanPham,
                sp.MaChiTietSP,
                sp.MaKichThuoc,
                sp.TrangThai,
                sp.NgayCapNhat,
                sp.Gia,
                sp.GiaGiam,
                sp.SoLuong,
                TenKichThuoc = _context.KichThuoc
                    .Where(th => th.MaKichThuoc == sp.MaKichThuoc)
                    .Select(th => th.TenKichThuoc)
                    .FirstOrDefault(),
                TenSanPham = _context.SanPham
                .Where(th => th.MaSanPham == sp.MaSanPham)
                .Select(th => th.TenSanPham)
                .FirstOrDefault()
            }).ToList();

            return Ok(new
            {
                code = 0,
                message = "Tìm kiếm thành công",
                data = result
            });
        }



    }
}
