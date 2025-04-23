using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLib.Entity
{
    public class ThanhToan
    {
        [Key]
        public int MaThanhToan { get; set; }
        public int? MaTaiKhoan { get; set; }
        public int? MaDonHang { get; set; }       
        public decimal SoTienGiaoDich { get; set; }
        public DateTime ThoiGian { get; set; }
       public string NoiDungChuyenKhoan { get; set; }
    }
}
