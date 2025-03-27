using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLib.Entity
{
    public class ThuongHieu
    {
        [Key]
        public int MaThuongHieu { get; set; }
        [Required(ErrorMessage = "Tên thương hiệu không được để trống.")]
        public string TenThuongHieu { get; set; }
        public string QuocGia { get; set; }
        public string MoTa { get; set; }
        public DateTime NgayCapNhat { get; set; }
        public bool TrangThai { get; set; }
    }
}
