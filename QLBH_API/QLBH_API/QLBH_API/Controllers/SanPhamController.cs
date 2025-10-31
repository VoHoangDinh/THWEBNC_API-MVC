using Microsoft.AspNetCore.Mvc;
using QLBH_API.Models;

namespace QLBH_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SanPhamController : ControllerBase
    {
        private readonly ISanPhamRepository _sanPhamRepo;
        //private readonly IWebHostEnvironment _env; // Thêm biến môi trường

        //public SanPhamController(ISanPhamRepository sanPhamRepo, IWebHostEnvironment env)
        public SanPhamController(ISanPhamRepository sanPhamRepo)
        {
            _sanPhamRepo = sanPhamRepo;
            //_env = env; // Khởi tạo
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _sanPhamRepo.GetSanPhams());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var sanPham = await _sanPhamRepo.GetSanPham(id);
            return sanPham == null ? NotFound() : Ok(sanPham);
        }





        //ACTION CREATE 
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] tbSanPham sanPham)
        {
            if (sanPham == null)
            {
                return BadRequest();
            }

            // API giờ chỉ việc nhận dữ liệu đã có sẵn tên ảnh và gọi repository
            await _sanPhamRepo.CreateSanPham(sanPham);

            // Trả về thông tin sản phẩm vừa tạo thành công
            return CreatedAtAction(nameof(GetById), new { id = sanPham.MASANPHAM }, sanPham);
        }





        //ACTION UPDATE
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] tbSanPham sanPham)
        {
            if (id != sanPham.MASANPHAM)
            {
                return BadRequest();
            }

            var existingSanPham = await _sanPhamRepo.GetSanPham(id);
            if (existingSanPham == null)
            {
                return NotFound();
            }

            // API giờ chỉ việc nhận dữ liệu và gọi repository để cập nhật
            await _sanPhamRepo.UpdateSanPham(sanPham);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var existingSanPham = await _sanPhamRepo.GetSanPham(id);
            if (existingSanPham == null) return NotFound();

            await _sanPhamRepo.DeleteSanPham(id);
            return NoContent();
        }

        [HttpGet("TheoDanhMuc/{maDanhMuc}")]
        public async Task<IActionResult> GetByDanhMuc(int maDanhMuc)
        {
            return Ok(await _sanPhamRepo.GetSanPhamsByDanhMuc(maDanhMuc));
        }
    }
}
