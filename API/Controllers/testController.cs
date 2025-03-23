using CoreLib.AppDbContext;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class testController : ControllerBase
    {
        private readonly AppDbContext _context;

        public testController(AppDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult Get()
        {
            var list = _context.ThuongHieu.ToList();
            if (list.Count == 0)
            {
                return NotFound(new { code = 1, message = "Danh sách thương hiệu 1 trống" });
            }
            return Ok(new { code = 0, message = "Lấy danh sách thành công", data = list });
        }
    }
}
