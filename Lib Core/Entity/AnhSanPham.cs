using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CoreLib.Entity
{
    public class AnhSanPham
    {
        [Key]
        public int MaAnhSP { get; set; }
        public int MaSanPham { get; set; }
        public int MaAnh { get; set; }
        public DateTime NgayCapNhat { get; set; }
    }
}
