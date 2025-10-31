namespace QLBH_API.Models
{
    public interface IGioHangRepository
    {
        // Sửa kiểu trả về từ IEnumerable<object> thành IEnumerable<CartItemDetail>
        Task<IEnumerable<CartItemDetail>> GetCartByCustomerIdAsync(decimal maKhachHang);
        Task AddToCartAsync(tbGioHang item);
        Task UpdateCartQuantityAsync(decimal maKhachHang, int maSanPham, int soLuong);
        Task RemoveFromCartAsync(decimal maKhachHang, int maSanPham);
    }
}
