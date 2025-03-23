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
        public string HoTen { get; set; }
        public string TenDangNhap { get; set; }
        public string MatKhau { get; set; }
        public string Email { get; set; }
        public string SoDienThoai { get; set; }
        public string LoaiTaiKhoan { get; set; }
        public bool TrangThai { get; set; }
        public DateTime NgayCapNhat { get; set; }

    }
}
