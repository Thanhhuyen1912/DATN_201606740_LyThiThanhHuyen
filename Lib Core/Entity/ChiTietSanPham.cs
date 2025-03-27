using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLib.Entity
{
    public class ChiTietSanPham
    {
        [Key]
        public int MaChiTietSP { get; set; }
        public int MaSanPham { get; set; }
        public int MaKichThuoc { get; set; }
        [Required(ErrorMessage = "Số lượng không được để trống.")]
        [RegularExpression(@"^[0-9]+$", ErrorMessage = "Số lượng phải là số.")]
        public int SoLuong { get; set; }
        [Required(ErrorMessage = "Giá không được để trống.")]
        [RegularExpression(@"^[0-9]+$", ErrorMessage = "Giá phải là số.")]
        public double Gia { get; set; }
        public DateTime NgayCapNhat { get; set; }
        public bool TrangThai { get; set; }
    }
}
