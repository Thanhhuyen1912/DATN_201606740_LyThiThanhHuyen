using CoreLib.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLib.DTO
{
    public class ThongTinTaiKhoanViewModel
    {
        public TaiKhoan? TaiKhoan { get; set; }
        public List<DiaChi>? DiaChis { get; set; }
    }
}
