using CoreLib.AppDbContext;
using CoreLib.Entity;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NongDoController : Controller
    {
        private readonly AppDbContext _context;
        public NongDoController(AppDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        [Route("/NongDo/Danhsach")]
        public IActionResult getAll()
        {
            var list = _context.NongDo.ToList();
            if (list == null)
            {
                return NotFound();
            }
            else
                return Ok(new { message = "Lấy danh sách thành công", code = 0, data = list });
        }
        [HttpGet]
        [Route("/TaiKhoan/ChiTiet")]
        public IActionResult getDetails(int math)
        {
            var th = _context.NongDo.FirstOrDefault(th => th.MaNongDo == math);
            if (th == null)
            {
                return NotFound();
            }
            else
                return Ok(new { message = "Lấy thông tin thành công", code = 0, data = th });
        }
        [HttpPut]
        public IActionResult Update(NongDo th)
        {
            try
            {
                _context.NongDo.Update(th);
                _context.SaveChanges();
                return Ok(new { message = "Cập nhật thông tin thành công", code = 0 });
            }
            catch
            { return BadRequest(); }

        }
        [HttpPost]
        public IActionResult post(NongDo th)
        {
            try
            {
                _context.NongDo.Add(th);
                _context.SaveChanges();
                return Ok(new { message = "Thêm nồng độ thành công", code = 0 });
            }
            catch
            { return BadRequest(); }

        }
        [HttpGet]
        [Route("/NongDo/TrangThai")]
        public IActionResult getByTrangthai(bool tt)
        {
            var th = _context.NongDo.Where(th => th.TrangThai == tt).ToList();
            if (th == null)
            {
                return NotFound();
            }
            else
                return Ok(new { message = "Lấy danh sách thành công", code = 0, data = th });
        }
    }
}