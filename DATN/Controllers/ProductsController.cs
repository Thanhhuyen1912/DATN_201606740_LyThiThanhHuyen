using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SANPHAM.Authorize;


namespace SANPHAM.Controllers
{

    public class ProductsController : Controller
    {
        [HttpGet]
        [RequiredLogin]
        public IActionResult Index()
        {
            string? role = HttpContext.Session.GetString("Role");
            if (role != null && (role.Contains("0") || role.Contains("1")))
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
    }
}
