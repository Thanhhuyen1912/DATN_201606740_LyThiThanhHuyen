using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CoreLib.Entity
{
    public class Anh
    {
        [Key]
        public int MaAnh { get; set; }
        [Required(ErrorMessage = "Tên ảnh không được để trống.")]
        public string TenAnh { get; set; }
        public string DuongDan { get; set; }
        public string MoTa { get; set; }
        public DateTime NgayCapNhat { get; set; }
    }
}
