using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLib.DTO
{
    public class SanPhamDTO
    {
        public int MaSanPham { get; set; }
        public string? TenSanPham { get; set; }
        public string? AnhDauTien { get; set; }
        public decimal? GiaDauTien { get; set; }
        public decimal? GiaGiamDauTien { get; set; }
    }

}
