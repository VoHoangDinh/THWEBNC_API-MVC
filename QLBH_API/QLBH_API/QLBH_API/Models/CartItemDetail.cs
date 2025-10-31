namespace QLBH_API.Models
{
    // Lớp này không tương ứng với bảng nào, chỉ dùng để hứng kết quả
    public class CartItemDetail
    {
        public int MASANPHAM { get; set; }
        public int SOLUONG { get; set; }
        public string TENSANPHAM { get; set; }
        public decimal? DONGIA { get; set; }
        public string? HINHANH { get; set; }
    }
}
