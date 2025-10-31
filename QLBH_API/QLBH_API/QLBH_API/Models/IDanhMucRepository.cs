namespace QLBH_API.Models
{
    public interface IDanhMucRepository
    {
        Task<IEnumerable<tbDanhMuc>> GetDanhMucs();
        Task<tbDanhMuc?> GetDanhMuc(int id);
    }
}
