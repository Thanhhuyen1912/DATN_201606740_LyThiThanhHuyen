using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLib.Entity
{
    public class SanPham
    {
        [Key]
        public int MaSanPham { get; set; }
        public int MaThuongHieu { get; set; }
        public int MaNhomHuong { get; set; }
        public int MaNongDo { get; set; }
        public string TenSanPham { get; set; }
        public string GioiTinh { get; set; }
        public bool TrangThai { get; set; }
        public string MoTa { get; set; }
        public DateTime NgayCapNhat { get; set; }

    }
}
