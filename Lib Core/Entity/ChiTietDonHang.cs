using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLib.Entity
{
    public class ChiTietDonHang
    {
        [Key]
        public int MaChiTietDonHang { get; set; }
        public int MaDonHang { get; set; }
        public int MaChiTietSP { get; set; }
        public int SoLuong { get; set; }
        public decimal TongTien { get; set; }
        public decimal Gia { get; set; }
    }
}
