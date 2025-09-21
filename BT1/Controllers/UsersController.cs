using Microsoft.AspNetCore.Mvc;
using BT1.Models;
using BT1.Services;

namespace BT1.Controllers
{
    public class UsersController : Controller
    {
        // Khai báo interface IUserService để làm việc với dữ liệu User
        private readonly IUserService _userService;

        // Hàm khởi tạo, ASP.NET Core sẽ tự inject UserService vào đây
        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        public IActionResult Index()
        {
            // Lấy danh sách user từ service (Tran Tuan Kiet_4:37 21/09/2025)
            var allUsers = _userService.GetUsers(); 

            return View();
        }
    }
}
