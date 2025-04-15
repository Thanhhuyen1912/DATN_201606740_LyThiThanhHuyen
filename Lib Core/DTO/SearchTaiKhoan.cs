using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib_Core.DTO
{
    public class SearchTaiKhoan
    {
        public string? TenTaiKhoan { get; set; }
        public string? SoDienThoai { get; set; }
        public int? LoaiTaiKhoan { get; set; }
        public bool? TrangThai { get; set; }
    }
}
