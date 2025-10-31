using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using QLBH_API.Data;

namespace QLBH_API.Models
{
    public class SanPhamRepository : ISanPhamRepository
    {
        private readonly DataContext _context;
        public SanPhamRepository(DataContext context) { _context = context; }

        public async Task<IEnumerable<tbSanPham>> GetSanPhams()
        {
            var param = new SqlParameter("@MASANPHAM", System.DBNull.Value);
            return await _context.tbSANPHAM.FromSqlRaw("EXEC psGetTableSANPHAM @MASANPHAM", param).ToListAsync();
        }

        public async Task<tbSanPham?> GetSanPham(int id)
        {
            var param = new SqlParameter("@MASANPHAM", id);
            var results = await _context.tbSANPHAM.FromSqlRaw("EXEC psGetTableSANPHAM @MASANPHAM", param).ToListAsync();
            return results.FirstOrDefault();
        }

        public async Task CreateSanPham(tbSanPham sanPham)
        {
            var parameters = new[]
            {
                new SqlParameter("@TENSANPHAM", sanPham.TENSANPHAM ?? (object)DBNull.Value),
                new SqlParameter("@DONGIA", sanPham.DONGIA ?? (object)DBNull.Value),
                new SqlParameter("@SOLUONG", sanPham.SOLUONG ?? (object)DBNull.Value),
                new SqlParameter("@HINHANH", sanPham.HINHANH ?? (object)DBNull.Value),
                new SqlParameter("@MOTA", sanPham.MOTA ?? (object)DBNull.Value),
                new SqlParameter("@MADANHMUC", sanPham.MADANHMUC ?? (object)DBNull.Value)
            };
            await _context.Database.ExecuteSqlRawAsync("EXEC psInsertRecordSANPHAM @TENSANPHAM, @DONGIA, @SOLUONG, @HINHANH, @MOTA, @MADANHMUC", parameters);
        }

        public async Task UpdateSanPham(tbSanPham sanPham)
        {
            var parameters = new[]
            {
                new SqlParameter("@MASANPHAM", sanPham.MASANPHAM),
                new SqlParameter("@TENSANPHAM", sanPham.TENSANPHAM ?? (object)DBNull.Value),
                new SqlParameter("@DONGIA", sanPham.DONGIA ?? (object)DBNull.Value),
                new SqlParameter("@SOLUONG", sanPham.SOLUONG ?? (object)DBNull.Value),
                new SqlParameter("@HINHANH", sanPham.HINHANH ?? (object)DBNull.Value),
                new SqlParameter("@MOTA", sanPham.MOTA ?? (object)DBNull.Value),
                new SqlParameter("@MADANHMUC", sanPham.MADANHMUC ?? (object)DBNull.Value)
            };
            await _context.Database.ExecuteSqlRawAsync("EXEC psUpdateRecordSANPHAM @MASANPHAM, @TENSANPHAM, @DONGIA, @SOLUONG, @HINHANH, @MOTA, @MADANHMUC", parameters);
        }

        public async Task DeleteSanPham(int id)
        {
            var param = new SqlParameter("@MASANPHAM", id);
            await _context.Database.ExecuteSqlRawAsync("EXEC psDeleteRecordSANPHAM @MASANPHAM", param);
        }

        public async Task<IEnumerable<tbSanPham>> GetSanPhamsByDanhMuc(int maDanhMuc)
        {
            var param = new SqlParameter("@MADANHMUC", maDanhMuc);
            return await _context.tbSANPHAM
                .FromSqlRaw("EXEC psGetTableSANPHAM_ByDanhMuc @MADANHMUC", param)
                .ToListAsync();
        }
    }
}
