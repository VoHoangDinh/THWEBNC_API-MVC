using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace QLBH_API.Models
{
    [Table("tbGIOHANG")]
    public class tbGioHang
    {
        [Key]
        public int ID { get; set; }
        public decimal MAKHACHHANG { get; set; }
        public int MASANPHAM { get; set; }
        public int SOLUONG { get; set; }
    }
}
