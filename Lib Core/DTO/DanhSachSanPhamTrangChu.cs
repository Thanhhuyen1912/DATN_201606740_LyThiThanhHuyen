using CoreLib.Entity;
using Lib_Core.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLib.DTO
{
    public class DanhSachSanPhamTrangChu
    {
        public List<SanPhamDTO>? SanPhamKhuyenMai {  get; set; }
        public List<SanPhamDTO>? SanPhamMoi {  get; set; }
        public List<SanPhamDTO>? SanPhamBanChay {  get; set; } 
    }
}
