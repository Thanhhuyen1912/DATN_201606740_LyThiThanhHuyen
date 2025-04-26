using CoreLib.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLib.DTO
{
    public class DonMuaViewModel
    {
        public int MaDonHang { get; set; }
        public int MaTaiKhoan { get; set; }
        public int? MaDiaChi { get; set; }
        public int? MaPhuongThucThanhToan { get; set; }
        public int? MMaGiamGia { get; set; }
        public decimal? TongTien { get; set; }
        public DateTime? NgayTao { get; set; }
        public bool TrangThaiThanhToan { get; set; }
        public string TrangThaiVanChuyen { get; set; }
        public string TenDatMua { get; set; }
        public string DiaChiTP { get; set; }
        public string DiaChiHuyen { get; set; }
        public string DiaChiXa { get; set; }
        public decimal? TienGiam { get; set; }
        public string LoaiGiamGia { get; set; }
        public string Phuongthucthanhtoan { get; set; }
        public string TenNhanHang { get; set; }
        public string SDTNhanHang { get; set; }
    }

    public class viewthaydoidiachi
    {
        public DiaChi p { get; set; }
        public List<DiaChi> list { get; set; }
        public int madonhang { get; set; }
    }

    public class noidungthaydiachi
    {
        public int madiachi { get; set; }
        public int madonhang { get; set; }
    }
}
