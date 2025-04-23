using CoreLib.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLib.DTO
{
    public class TTTToan
    {
        public DonHang donhang { get; set; }
        public DiaChi diachi { get; set; }
        public TaiKhoan taikhoan { get; set; }

        public string URLThanhToan {  get; set; }
    }
}
