using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using QLBH_API.Data;

namespace QLBH_API.Models
{
    public class DanhMucRepository : IDanhMucRepository
    {
        private readonly DataContext _context;
        public DanhMucRepository(DataContext context) { _context = context; }

        public async Task<tbDanhMuc?> GetDanhMuc(int id)
        {
            var param = new SqlParameter("@MADANHMUC", id);
            var results = await _context.tbDANHMUC
                .FromSqlRaw("EXEC psGetTableDANHMUC @MADANHMUC", param)
                .ToListAsync();
            return results.FirstOrDefault();
        }

        public async Task<IEnumerable<tbDanhMuc>> GetDanhMucs()
        {
            // Truyền DBNull.Value cho tham số để stored procedure trả về tất cả
            var param = new SqlParameter("@MADANHMUC", System.DBNull.Value);
            return await _context.tbDANHMUC
                .FromSqlRaw("EXEC psGetTableDANHMUC @MADANHMUC", param)
                .ToListAsync();
        }
    }
}
