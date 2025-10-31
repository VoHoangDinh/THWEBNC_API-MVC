using Microsoft.AspNetCore.Mvc;
using QLBH_API.Models;

namespace QLBH_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GioHangController : ControllerBase
    {
        private readonly IGioHangRepository _gioHangRepo;
        public GioHangController(IGioHangRepository gioHangRepo) { _gioHangRepo = gioHangRepo; }

        [HttpGet("{maKhachHang}")]
        public async Task<IActionResult> GetCart(decimal maKhachHang)
        {
            var cartItems = await _gioHangRepo.GetCartByCustomerIdAsync(maKhachHang);
            return Ok(cartItems);
        }

        [HttpPost]
        public async Task<IActionResult> AddToCart([FromBody] tbGioHang item)
        {
            if (item == null) return BadRequest();
            await _gioHangRepo.AddToCartAsync(item);
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateQuantity([FromBody] tbGioHang item)
        {
            if (item == null) return BadRequest();
            await _gioHangRepo.UpdateCartQuantityAsync(item.MAKHACHHANG, item.MASANPHAM, item.SOLUONG);
            return Ok();
        }

        [HttpDelete("{maKhachHang}/{maSanPham}")]
        public async Task<IActionResult> RemoveFromCart(decimal maKhachHang, int maSanPham)
        {
            await _gioHangRepo.RemoveFromCartAsync(maKhachHang, maSanPham);
            return Ok();
        }
    }
}
