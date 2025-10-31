using Microsoft.AspNetCore.Mvc;
using QLBH_API.Models;

namespace QLBH_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DanhMucController : ControllerBase
    {
        private readonly IDanhMucRepository _danhMucRepo;
        public DanhMucController(IDanhMucRepository danhMucRepo) { _danhMucRepo = danhMucRepo; }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _danhMucRepo.GetDanhMucs());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var danhMuc = await _danhMucRepo.GetDanhMuc(id);
            return danhMuc == null ? NotFound() : Ok(danhMuc);
        }
    }
}
