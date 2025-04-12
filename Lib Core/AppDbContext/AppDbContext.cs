using CoreLib.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLib.AppDbContext
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Anh> Anh { get; set; } // Bảng ảnh
        public DbSet<SanPham> SanPham { get; set; } // Bảng sản phẩm
        public DbSet<AnhSanPham> AnhSanPham { get; set; } // Bảng ảnh sản phẩm
        public DbSet<ChiTietDonHang> ChiTietDonHang { get; set; } // Bảng chi tiết đơn hàng
        public DbSet<ChiTietGioHang> ChiTietGioHang { get; set; } // Bảng chi tiết giỏ hàng
        public DbSet<ChiTietSanPham> ChiTietSanPham { get; set; } // Bảng chi tiết sản phẩm
        public DbSet<DanhGia> DanhGia { get; set; } // Bảng đánh giá
        public DbSet<DiaChi> DiaChi { get; set; } // Bảng địa chỉ
        public DbSet<DonHang> DonHang { get; set; } // Bảng đơn hàng
        public DbSet<GioHang> GioHang { get; set; } // Bảng giỏ hàng
        public DbSet<KichThuoc> KichThuoc { get; set; } // Bảng kích thước
        public DbSet<MaGiamGia> MaGiamGia { get; set; } // Bảng mã giảm giá
        public DbSet<NhomHuong> NhomHuong { get; set; } // Bảng nhóm hương
        public DbSet<PhuongThucThanhToan> PhuongThucThanhToan { get; set; } // Bảng phương thức thanh toán
        public DbSet<TaiKhoan> TaiKhoan { get; set; } // Bảng tài khoản
        public DbSet<ThanhToan> ThanhToan { get; set; } // Bảng thanh toán
        public DbSet<ThuongHieu> ThuongHieu { get; set; } // Bảng thương hiệu
        public DbSet<YeuThich> YeuThich { get; set; } // Bảng yêu thích
       
    }
}
