﻿using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Mvc;

namespace SANPHAM.Authorize
{
    public class RequiredLoginBuyAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var httpContext = context.HttpContext;
            var session = httpContext.Session;
            var user = context.HttpContext.Session.GetString("Role");
            var url_1 = context.HttpContext.Session.GetInt32("MaSanPham");

            if (string.IsNullOrEmpty(user))
            {
                // Lưu URL hiện tại vào session
                var returnUrl = "/SanPhamKhach/ChiTietSP/?masp=" + url_1;
                session.SetString("ReturnUrl", returnUrl);
                // Lấy TempData để thêm thông báo
                var tempDataFactory = httpContext.RequestServices.GetService<ITempDataDictionaryFactory>();
                var tempData = tempDataFactory.GetTempData(httpContext);
                tempData["LoginMessage"] = "Vui lòng sử dụng tài khoản phân quyền để tiếp tục.";
                // Chuyển hướng đến trang login
                context.Result = new RedirectToActionResult("DangNhap", "TaiKhoan", null);

            }
        }
    }
}
   