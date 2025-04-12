using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib_Core.DTO
{
    public class SearchSanPhamDTO
    {
        public string? TenSanPham { get; set; }
        public int? ThuongHieuId { get; set; }
        public int? NhomHuongId { get; set; }
        public string? GioiTinh { get; set; } 
        public bool? TrangThai {  get; set; }
    }
}
