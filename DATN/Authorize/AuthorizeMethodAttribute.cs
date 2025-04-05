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

        //public override void OnActionExecuting(ActionExecutingContext context)
        //{
        //    var userRole = context.HttpContext.Session.GetString("UserRole");
        //    string[] Roles = userRole.Split(",");

        //    Console.WriteLine($"UserRole from Session: {userRole}"); 

        //    // Nếu không có session hoặc user không có quyền
        //    if (string.IsNullOrEmpty(userRole) || !Roles.Any(role => _allowedRoles.Contains(role)))
        //    {
        //        // Chuyển hướng đến trang đăng nhập
        //        context.Result = new RedirectToActionResult("Index", "Account", null);
        //        return;
        //    }
        //}
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var userRole = context.HttpContext.Session.GetString("UserRole");

            // Nếu chưa đăng nhập thì bỏ qua kiểm tra role
            if (string.IsNullOrEmpty(userRole))
            {
                return; // Cho phép vào trang
            }

            string[] Roles = userRole.Split(",");
            Console.WriteLine($"UserRole from Session: {userRole}");

            // Nếu không có quyền thì chuyển hướng
            if (!_allowedRoles.Any(role => Roles.Contains(role)))
            {
                context.Result = new RedirectToActionResult("Index", "Account", null);
            }
        }

    }


}
