using CoreLib.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLib.DTO
{
    public class Hienthigiohang
    {
        public ChiTietGioHang? chitietgiohang {  get; set; }
        public ChiTietSanPham? chitietsanpham {  get; set; }
        public Anh? anh {  get; set; }
        public string? tensanpham { get; set; }

    }
}
