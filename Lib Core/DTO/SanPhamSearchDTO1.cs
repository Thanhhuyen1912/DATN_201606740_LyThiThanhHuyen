using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib_Core.DTO
{
    public class SanPhamSearchDTO1
    {
        public int? KichThuocID { get; set; }
        public int MaSanPham { get; set; }
        public decimal? Gia { get; set; }
        public decimal? GiaGiam { get; set; }
        public bool? TrangThai { get; set; }
        public DateTime? NgayDau { get; set; }
        public DateTime? NgayCuoi { get; set; }
    }

}
