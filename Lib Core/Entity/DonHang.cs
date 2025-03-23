using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLib.Entity
{
    public class DonHang
    {
        [Key]
        public int MaDonHang { get; set; }
        public int MaTaiKhoan { get; set; }
        public DateTime NgayTao { get; set; }
        public DateTime NgayCapNhat { get; set; }
        public string TrangThaiVanChuyen { get; set; }
        public int MaGiamGia { get; set; }
        public double TongTien { get; set; }
        public int MaPhuongThucThanhToan { get; set; }
        public bool TrangThaiThanhToan { get; set; }
        public int MaDiaChi { get; set; }
    }
}
