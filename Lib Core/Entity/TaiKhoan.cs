using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLib.Entity
{
    public class TaiKhoan
    {
        [Key]
        public int MaTaiKhoan { get; set; }
        [Required(ErrorMessage = "Họ tên không được để trống.")]
        public string HoTen { get; set; }
        [Required(ErrorMessage = "Tên đăng nhập không được để trống.")]
        [RegularExpression(@"^(?=.*[A-Z])(?=.*[\W_]).{8,}$", ErrorMessage = "Tên đăng nhập phải có ít nhất 8 ký tự, bao gồm ít nhất 1 chữ viết hoa và 1 ký tự đặc biệt.")]
        public string TenDangNhap { get; set; }
        [Required(ErrorMessage = "Mật khẩu không được để trống.")]
        [RegularExpression(@"^[a-zA-Z0-9]{6,}$", ErrorMessage = "Mật khẩu phải có ít nhất 6 ký tự và không chứa ký tự đặc biệt.")]
        public string MatKhau { get; set; }
        [Required(ErrorMessage = "Email không được để trống.")]
        [EmailAddress(ErrorMessage = "Email không đúng định dạng.")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Số điện thoại không được để trống.")]
        [RegularExpression(@"^0[0-9]{9}$", ErrorMessage = "Số điện thoại chưa đúng định dạng chuẩn.")]
        public string SoDienThoai { get; set; }
        public string LoaiTaiKhoan { get; set; }
        public bool TrangThai { get; set; }
        public DateTime NgayCapNhat { get; set; }

    }
}
