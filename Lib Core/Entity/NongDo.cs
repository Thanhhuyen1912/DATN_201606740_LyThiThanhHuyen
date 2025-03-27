using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLib.Entity
{
    public class NongDo
    {
        [Key]
        public int MaNongDo { get; set; }
        [Required(ErrorMessage = "Tên nồng độ không được để trống.")]
        public string TenNongDo { get; set; }
        public string MoTa { get; set; }
        public DateTime NgayCapNhat { get; set; }
        public bool TrangThai { get; set; }
    }
}
