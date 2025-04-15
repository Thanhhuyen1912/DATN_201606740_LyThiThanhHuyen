using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLib.Entity
{
    public class KichThuoc
    {
        [Key]
        public int MaKichThuoc { get; set; }
        [Required(ErrorMessage = "Tên kích thước không được để trống.")]
        public string TenKichThuoc { get; set; }
        public string? MoTa { get; set; }
        public bool? TrangThai { get; set; }
        public DateTime? NgayCapNhat { get; set; }
    }
}
