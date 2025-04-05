using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace SANPHAM.Authorize
{
    public class RequiredLoginAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var httpContext = context.HttpContext;
            var session = httpContext.Session;
            var user = context.HttpContext.Session.GetString("Username");

            if (string.IsNullOrEmpty(user))
            {
                // Lưu URL hiện tại vào session
                var returnUrl = httpContext.Request.Path + httpContext.Request.QueryString;
                session.SetString("ReturnUrl", returnUrl);

                // Chuyển hướng đến trang login
                context.Result = new RedirectToActionResult("Index", "Account", null);
            }
        }
    }

}
