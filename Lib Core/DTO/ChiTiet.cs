using CoreLib.Entity;
using System.Reflection;

namespace CoreLib.DTO
{
    public class DonHangSessionDto
    {
        public string MaDonHang { get; set; }
    }

    public class OrderRequest
    {
        public decimal thanhtien { get; set; }
        public int diachi { get; set; }
        public int maphuongthuc { get; set; }
        public string magiamgia { get; set; }
        public List<ChiTietGioHang> listsp { get; set; }
    }
    public class MaGiamGiaRequest
    {
        public string magiamgia { get; set; }
        public decimal tongtien { get; set; }
    }
    public class ChiTiet
    {
        public SanPham sanpham {  get; set; }
        public List<ChiTietSanPham>? chitietsanpham {  get; set; }
        public List<Anh>? anhs {  get; set; }
        public Anh anhdau {  get; set; }
        public decimal GiaThapNhat {  get; set; }
        public decimal GiaCaoNhat { get; set; }
        public string thuonghieu { get; set; }
        public string nhomhuong {  get; set; }
        public string xuatxu { get; set; }
    }

    public class NdChitiet
    {
        public List<Chitietdh> chitiet { get; set; }
        public int id { get; set; }
        public DonHang dh {  get; set; }
        public List<DanhGia> danhgia { get; set; }
        public int mataikhoan {  get; set; }
        public DiaChi diachi {  get; set; }
        public TaiKhoan taikhoan {  get; set; }
        public string phuongthucthanhtoan { get; set; }
        public string ttthanhtoan { get; set; }
        public decimal giamgia {  get; set; }

    }
    public class HienThiChiTiet
    {
        public ChiTiet spchinh { get; set; }
        public decimal diemtrungbinh {  get; set; }
        public List<Hienthidanhgia> danhgias { get; set; }
        public List<SanPhamDTO> dslienquan { get; set; }
    }
    public class Hienthidanhgia
    {
        public int MaSanPham { get; set; }
        public int MaTaiKhoan { get; set; }
        public string TenKhachHang { get; set; }
        public int SoDiem { get; set; }
        public string MoTa { get; set; }
        public DateTime NgayDanhGia { get; set; }
    }
    public class DanhGiaForm
    {
        public string MoTa { get; set; }
        public int SoDiem { get; set; }
        public int MaTaiKhoan { get; set; }
        public int MaSanPham { get; set; }
    }
    public class Chitietdh
    {
        public int MaSanPham { get; set; }
        public decimal GiaTien { get; set; }
        public decimal GiaGiam { get; set; }
        public string TenSanPham { get; set; }
        public int SoLuong { get; set; }
        public decimal TongTien { get; set; }
    }
}
