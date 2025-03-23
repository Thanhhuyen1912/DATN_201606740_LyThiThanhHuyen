using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLib.Entity
{
    public class DiaChi
    {
        [Key]
        public int MaDiaChi { get; set; }
        public int MaTaiKhoan { get; set; }
        public string ThanhPho { get; set; }
        public string Huyen { get; set; }
        public string Xa { get; set; }
        public string ChiTiet { get; set; }
        public string HoTenNguoiNhan { get; set; }
        public string SoDienThoaiNguoiNhan { get; set; }
        public DateTime NgayCapNhat { get; set; }

    }
}
