using CoreLib.AppDbContext;
using CoreLib.DTO;
using CoreLib.Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace API.Controllers
{
    [ApiController]
    public class CheckThanhToanController : ControllerBase
    {
        private readonly ILogger<CheckThanhToanController> _logger;
        private readonly AppDbContext _context;

        public CheckThanhToanController(ILogger<CheckThanhToanController> logger, AppDbContext context)
        {
            _logger = logger;
            _context = context;
        }
        [HttpPost]
        [Route("LayTT/NoiDung")]
        public async Task<IActionResult> ReceiveWebhook([FromBody] JsonElement payload)
        {
            try
            {
                _logger.LogInformation("Received Webhook: {0}", payload.ToString());

                var dataArray = payload.GetProperty("data").EnumerateArray();
                foreach (var item in dataArray)
                {
                    var soTien = item.GetProperty("amount").GetDecimal(); 
                    var noiDung = item.GetProperty("description").GetString();
                    var thoiGianStr = item.GetProperty("when").GetString();
                    var thoiGian = DateTime.Parse(thoiGianStr); 

                    var thanhToan = new ThanhToan
                    {
                        SoTienGiaoDich = soTien,
                        NoiDungChuyenKhoan = noiDung,
                        ThoiGian = thoiGian
                    };

                    _context.ThanhToan.Add(thanhToan);
                }

                await _context.SaveChangesAsync();
                return Ok(new { success = true });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Lỗi khi nhận webhook");
                return StatusCode(500, new { success = false, error = ex.Message });
            }
        }      

    }
}
