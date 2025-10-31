using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace QLBH_API.Models
{
    [Table("tbDANHMUC")]
    public class tbDanhMuc
    {
        [Key]
        public int MADANHMUC { get; set; }
        public string? TENDANHMUC { get; set; }
        public int? DANHMUCCHA { get; set; }
        public string? MOTA { get; set; }
    }
}
