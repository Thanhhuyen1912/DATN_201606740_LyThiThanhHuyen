using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib_Core.DTO
{
    public class SanPhamBanChayDTO
    {
        public int MaChiTietSP { get; set; }
        public int MaSanPham { get; set; }
        public decimal Gia { get; set; }
        public decimal GiaGiam { get; set; }
        public int SoLuongBan { get; set; }
        public string TenSanPham { get; set; }
    }
}
