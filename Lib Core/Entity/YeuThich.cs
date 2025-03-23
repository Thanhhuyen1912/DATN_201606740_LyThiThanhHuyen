using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLib.Entity
{
    public class YeuThich
    {
        [Key]
        public int MaYeuThich { get; set; }
        public int MaTaiKhoan { get; set; }
        public int MaSanPham { get; set; }
        public DateTime NgayCapNhat { get; set; }
    }
}
