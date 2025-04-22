using CoreLib.Entity;
using Lib_Core.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLib.DTO
{
    public class ListDatHang
    {
        public List<DiaChi> diachis { get; set; }
        public TaiKhoan taikhoan { get; set; }
        public List<ChiTietGioHang> chitietgiohangs { get; set; }
        public List<PhuongThucThanhToan> phuongthucs { get; set; }
        public List<SanPhamWithImageDTO> sanphamhienthi { get; set; }

    }
}
