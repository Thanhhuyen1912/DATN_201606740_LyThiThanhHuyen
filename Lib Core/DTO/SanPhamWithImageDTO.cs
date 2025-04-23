using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLib.DTO
{
    public class SanPhamWithImageDTO
    {
        public int MaSanPham { get; set; }  
        public string TenSanPham { get; set; } 
        public decimal Gia { get; set; }  
        public decimal GiaGiam { get; set; }  
        public string ImageUrl { get; set; } 
        public int? soluong { get; set; }
        public int? MaChiTietSP { get; set; }
    }

}
