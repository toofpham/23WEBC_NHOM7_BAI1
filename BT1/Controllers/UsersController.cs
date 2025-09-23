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

        public IActionResult Index(int page = 1, int pageSize = 5)
        {
            // Validate page number
            if (page < 1) page = 1;
            if (pageSize < 1) pageSize = 5;
            if (pageSize > 50) pageSize = 50;
            // Lấy danh sách user từ service (Tran Tuan Kiet_4:37 21/09/2025)
            var allUsers = _userService.GetUsers();

            // Tạo phân trang
            var paginatedUsers = PaginatedList<User>.Create(allUsers, page, pageSize);

            // Truyền thông tin phân trang qua ViewBag để sử dụng trong View
            ViewBag.CurrentPage = page;
            ViewBag.PageSize = pageSize;

            return View(paginatedUsers);
        }
    }
}
