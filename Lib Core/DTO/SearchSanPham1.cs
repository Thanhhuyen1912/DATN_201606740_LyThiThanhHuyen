using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLib.DTO
{
    public class SearchSanPham1
    {
        public string? TenSanPham {  get; set; }
        public string? GioiTinh { get; set;}
        public int? MaNhomHuong { get; set; }
        public int? MaThuongHieu { get; set; }
        public decimal? GiaMin { get; set; }
        public decimal? GiaMax { get; set; }

    }
}
