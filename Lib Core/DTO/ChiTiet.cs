using CoreLib.Entity;
using System.Reflection;

namespace CoreLib.DTO
{
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
}
