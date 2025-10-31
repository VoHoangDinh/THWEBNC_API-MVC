using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QLBH_WEB.Models
{
    public class SanPhamViewModel
    {
        public int MASANPHAM { get; set; }
        public string TENSANPHAM { get; set; }
        public decimal? DONGIA { get; set; }
        public decimal? SOLUONG { get; set; }
        public string HINHANH { get; set; }
        public string MOTA { get; set; }
        public int? MADANHMUC { get; set; }
    }
}