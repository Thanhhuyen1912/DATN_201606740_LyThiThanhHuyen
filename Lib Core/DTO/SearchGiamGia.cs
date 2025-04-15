using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib_Core.DTO
{
    public class SearchGiamGia
    {
        public string? MaHienThi {  get; set; }
        public string? LoaiGiamGia { get;set; }
        public bool? TrangThai  { get; set; }  
        public DateTime? NgayBatDau { get; set; }
        public DateTime? NgayKetThuc { get; set; }
    }
}
