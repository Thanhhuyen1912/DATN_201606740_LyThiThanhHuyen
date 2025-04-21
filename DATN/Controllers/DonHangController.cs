using Microsoft.AspNetCore.Mvc;

namespace DATN.Controllers
{
    public class DonHangController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult DatHang()
        {
            return View();
        }
    }
}
