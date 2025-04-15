using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace SANPHAM.Authorize
{
    public class RequiredLoginAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var httpContext = context.HttpContext;
            var session = httpContext.Session;
            var user = context.HttpContext.Session.GetString("Role");

            if (string.IsNullOrEmpty(user))
            {
                // Lưu URL hiện tại vào session
                var returnUrl = httpContext.Request.Path + httpContext.Request.QueryString;
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
