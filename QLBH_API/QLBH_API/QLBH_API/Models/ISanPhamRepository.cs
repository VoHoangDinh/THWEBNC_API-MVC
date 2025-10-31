namespace QLBH_API.Models
{
    public interface ISanPhamRepository
    {
        Task<IEnumerable<tbSanPham>> GetSanPhams();
        Task<tbSanPham?> GetSanPham(int id);
        Task CreateSanPham(tbSanPham sanPham);
        Task UpdateSanPham(tbSanPham sanPham);
        Task DeleteSanPham(int id);

        Task<IEnumerable<tbSanPham>> GetSanPhamsByDanhMuc(int maDanhMuc);
    }
}
