using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLib.Entity
{
    public class DanhGia
    {
        [Key]
        public int MaDanhGia { get; set; }
        public int MaTaiKhoan { get; set; }
        public int MaCTSanPham { get; set; }
        public int SoDiem { get; set; }
        public string MoTa { get; set; }
        public DateTime NgayCapNhat { get; set; }
    }
}
