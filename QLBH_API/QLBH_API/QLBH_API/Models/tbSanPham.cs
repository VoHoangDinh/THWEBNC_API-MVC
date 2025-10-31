using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace QLBH_API.Models
{
    [Table("tbSANPHAM")]
    public class tbSanPham
    {
        [Key]
        public int MASANPHAM { get; set; }
        public string? TENSANPHAM { get; set; }
        public decimal? DONGIA { get; set; }
        public decimal? SOLUONG { get; set; }
        public string? HINHANH { get; set; }
        public string? MOTA { get; set; }
        public int? MADANHMUC { get; set; }
    }
}
