using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLib.Entity
{
    public class MaGiamGia
    {
        [Key]
        public int MMaGiamGia { get; set; }
        public string MaHienThi { get; set; }
        public DateTime NgayBatDau { get; set; }
        public DateTime NgayKetThuc { get; set; }
        public string LoaiGiamGia { get; set; }
        public double GiaTri { get; set; }
        public bool TrangThai { get; set; }
        public DateTime NgayCapNhat { get; set; }
    }
}
