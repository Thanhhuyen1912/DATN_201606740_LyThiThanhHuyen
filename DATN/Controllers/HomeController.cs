using System;
using System.Diagnostics;
using DATN.Models;
using Microsoft.AspNetCore.Mvc;
using CoreLib.AppDbContext;
using CoreLib.Entity;
using Microsoft.EntityFrameworkCore;

namespace DATN.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly AppDbContext _context;
        public HomeController(AppDbContext context, ILogger<HomeController> logger)
        {
            _context = context;
            _logger = logger;
        }


        public IActionResult Index()
        {
            ViewBag.User = HttpContext.Session.GetString("User");
            ViewBag.tieude1 = "";
            ViewBag.tieude2 = "";

            var result = _context.ChiTietSanPham
       .Include(x => x.SanPham)
       .ToList();

            return View(result);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
