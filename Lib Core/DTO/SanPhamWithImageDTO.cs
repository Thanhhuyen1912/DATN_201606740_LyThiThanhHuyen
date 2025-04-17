using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib_Core.DTO
{
    public class SanPhamWithImageDTO
    {
        public int MaSanPham { get; set; }  // Mã sản phẩm
        public string TenSanPham { get; set; }  // Tên sản phẩm
        public decimal Gia { get; set; }  // Giá sản phẩm
        public decimal GiaGiam { get; set; }  // Giá giảm (nếu có)
        public string ImageUrl { get; set; }  // URL của ảnh sản phẩm
    }

}
