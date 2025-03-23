using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLib.Entity
{
    public class ThanhToan
    {
        [Key]
        public int MaThanhToan { get; set; }
        public int MaTaiKhoan { get; set; }
        public int MaDonHang { get; set; }
        public int MaLoi { get; set; }
        public string MaGiaoDichNoiBo { get; set; }
        public string MaGiaoDich { get; set; }
        public string MoTaGiaoDich { get; set; }
        public double SoTienGiaoDich { get; set; }
        public DateTime ThoiGian { get; set; }
        public string SoTaiKhoan { get; set; }
        public string TenNgangHang { get; set; }
        public string MaNganHang { get; set; }
        public string TenNguoiGui { get; set; }
        public string STKGui { get; set; }
        public string MaNganHangGui { get; set; }
        public string TenNganHangGui { get; set; }
    }
}
