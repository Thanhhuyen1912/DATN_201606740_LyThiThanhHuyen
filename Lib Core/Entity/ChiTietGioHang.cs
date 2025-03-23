using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLib.Entity
{
    public class ChiTietGioHang
    {
        [Key]
        public int MaChiTietGio { get; set; }
        public int MaGioHang { get; set; }
        public int MaChiTietSanPham { get; set; }
        public int SoLuong { get; set; }
        public double TongTien { get; set; }
        public DateTime NgayCapNhat { get; set; }
    }
}
