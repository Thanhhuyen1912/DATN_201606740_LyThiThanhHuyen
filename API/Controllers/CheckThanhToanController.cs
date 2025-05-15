using CoreLib.AppDbContext;
using CoreLib.Entity;
using Microsoft.AspNetCore.Mvc;
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
                await System.IO.File.AppendAllTextAsync("webhook_log.txt", payload.ToString() + "\n");
                var dataArray = payload.GetProperty("data").EnumerateArray();
                foreach (var item in dataArray)
                {
                    var soTien = item.GetProperty("amount").GetDecimal(); 
                    var noiDung = item.GetProperty("description").GetString();
                    var thoiGianStr = item.GetProperty("when").GetString();
                    var stkgui = item.GetProperty("corresponsiveAccount").GetString();
                    var thoiGian = DateTime.Parse(thoiGianStr); 

                    var thanhToan = new ThanhToan
                    {
                        SoTienGiaoDich = soTien,
                        NoiDungChuyenKhoan = noiDung,
                        ThoiGian = thoiGian,
                        STKGui = stkgui
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
