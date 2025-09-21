using BT1.Services;
using Microsoft.AspNetCore.Mvc;

namespace BT1.Middleware
{
    // Middleware đảm bảo danh sách User được load vào UserService (Tran Tuan Kiet_4:37 21/09/2025)
    // Chạy trước khi request được xử lý bởi Controller (Tran Tuan Kiet_4:37 21/09/2025)
    public class UserLoadingMiddleware
    {
        private readonly RequestDelegate _next;

        // Để nhận middleware kế tiếp từ hệ thống, và _next = next để lưu lại, dùng khi request chạy qua (Tran Tuan Kiet_4:37 21/09/2025)
        public UserLoadingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, IUserService userService)
        {
            // Load danh sách user vào service (Tran Tuan Kiet_4:37 21/09/2025)
            // Phương thức LoadUsers() đã có kiểm tra để không load lại nếu đã có dữ liệu (Tran Tuan Kiet_4:37 21/09/2025)
            userService.LoadUsers();

            // Tiếp tục xử lý request (Tran Tuan Kiet_4:37 21/09/2025)
            await _next(context);
        }
    }

    //Load dữ liệu user vào UserService trước khi Controller xử lý (Tran Tuan Kiet_4:37 21/09/2025)
    //Đảm bảo dữ liệu đã sẵn sàng khi Controller cần (Tran Tuan Kiet_4:37 21/09/2025)
}
