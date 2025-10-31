using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using QLBH_API.Data;

namespace QLBH_API.Models
{
    public class GioHangRepository : IGioHangRepository
    {
        private readonly DataContext _context;
        public GioHangRepository(DataContext context) { _context = context; }

        public async Task<IEnumerable<CartItemDetail>> GetCartByCustomerIdAsync(decimal maKhachHang)
        {
            var param = new SqlParameter("@MaKhachHang", maKhachHang);

            // Gọi Stored Procedure và ánh xạ kết quả vào lớp CartItemDetail
            return await _context.CartItemDetails
                .FromSqlRaw("EXEC sp_GetCart_ByCustomerId @MaKhachHang", param)
                .ToListAsync();
        }

        public async Task AddToCartAsync(tbGioHang item)
        {
            var parameters = new[] {
                new SqlParameter("@MaKhachHang", item.MAKHACHHANG),
                new SqlParameter("@MaSanPham", item.MASANPHAM),
                new SqlParameter("@SoLuong", item.SOLUONG)
            };
            await _context.Database.ExecuteSqlRawAsync("EXEC sp_AddToCart @MaKhachHang, @MaSanPham, @SoLuong", parameters);
        }

        public async Task UpdateCartQuantityAsync(decimal maKhachHang, int maSanPham, int soLuong)
        {
            var parameters = new[] {
                new SqlParameter("@MaKhachHang", maKhachHang),
                new SqlParameter("@MaSanPham", maSanPham),
                new SqlParameter("@SoLuong", soLuong)
            };
            await _context.Database.ExecuteSqlRawAsync("EXEC sp_UpdateCartQuantity @MaKhachHang, @MaSanPham, @SoLuong", parameters);
        }

        public async Task RemoveFromCartAsync(decimal maKhachHang, int maSanPham)
        {
            var parameters = new[] {
                new SqlParameter("@MaKhachHang", maKhachHang),
                new SqlParameter("@MaSanPham", maSanPham)
            };
            await _context.Database.ExecuteSqlRawAsync("EXEC sp_RemoveFromCart @MaKhachHang, @MaSanPham", parameters);
        }
    }
}