using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLib.Entity
{
    public class PhuongThucThanhToan
    {
        [Key]
        public int MaPhuongThuc { get; set; }
        [Required(ErrorMessage = "Tên phương thức thanh toán không được để trống.")]
        public string TenPhuongThuc { get; set; }
        public string MoTa { get; set; }
        public DateTime NgayCapNhat { get; set; }
        public bool TrangThai { get; set; }
    }
}
