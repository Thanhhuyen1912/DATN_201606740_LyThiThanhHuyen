using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib_Core.DTO
{
    public  class SearchPT
    {
        public string? TenPhuongThuc { get; set; }
        public bool? TrangThai { get; set; }
        public DateTime? NgayDau { get; set; }
        public DateTime? NgayCuoi { get; set; }
    }
}
