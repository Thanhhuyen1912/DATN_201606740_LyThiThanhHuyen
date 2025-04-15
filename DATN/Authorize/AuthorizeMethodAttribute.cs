using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace SANPHAM.Authorize
{
    public class AuthorizeMethodAttribute : ActionFilterAttribute
    {
        private readonly string[] _allowedRoles;

        public AuthorizeMethodAttribute(string roles)
        {
            _allowedRoles = roles.Split(",");
        }
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var userRole = context.HttpContext.Session.GetString("UserRole");

            // Nếu chưa đăng nhập thì bỏ qua kiểm tra role
            if (string.IsNullOrEmpty(userRole))
            {
                return; // Cho phép vào trang
            }

            string[] Roles = userRole.Split(",");

            // Nếu không có quyền thì chuyển hướng
            if (!_allowedRoles.Any(role => Roles.Contains(role)))
            {
                context.Result = new RedirectToActionResult("Index", "Account", null);
            }
        }

    }


}
