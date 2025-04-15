using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLib.Entity
{
    public class NhomHuong
    {
        [Key]
        public int MaNhomHuong { get; set; }
        [Required(ErrorMessage = "Tên nhóm hương không được để trống.")]
        public string TenNhomHuong { get; set; }
        public string? MoTa { get; set; }
        public DateTime? NgayCapNhat { get; set; }
        public bool TrangThai { get; set; }
    }
}
