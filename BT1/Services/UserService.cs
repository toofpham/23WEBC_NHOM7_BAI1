using BT1.Models;

namespace BT1.Services
{
    // Interface định nghĩa các phương thức làm việc với User (Tran Tuan Kiet_4:37 21/09/2025)
    public interface IUserService
    {
        List<User> GetUsers();
        void AddUser(User user);
        void LoadUsers();
    }

    // Service quản lý danh sách người dùng (Tran Tuan Kiet_4:37 21/09/2025)
    // Được đăng ký như Scoped service trong DI container (Tran Tuan Kiet_4:37 21/09/2025)
    public class UserService : IUserService
    {
        private List<User> _users;

        // Lấy tất cả người dùng (Tran Tuan Kiet_4:37 21/09/2025)
        public List<User> GetUsers()
        {
            return _users;
        }

        public void AddUser(User user)
        {
            _users.Add(user);
        }

        // Load danh sách người dùng từ dữ liệu mẫu (Tran Tuan Kiet_4:37 21/09/2025)
        public void LoadUsers()
        {
            _users = new List<User>
            {
                new User { Id = 1, Username = "admin", Password = "admin123", Role = 1},
                new User { Id = 2, Username = "manager1", Password = "manager123", Role = 2 },
                new User { Id = 3, Username = "manager2", Password = "manager456", Role = 2 },
                new User { Id = 4, Username = "user1", Password = "user123", Role = 3 },
                new User { Id = 5, Username = "user2", Password = "user456", Role = 3 },
                new User { Id = 6, Username = "user3", Password = "user789", Role = 3, },
                new User { Id = 7, Username = "testuser1", Password = "test123", Role = 3 },
                new User { Id = 8, Username = "testuser2", Password = "test456", Role = 3 },
                new User { Id = 9, Username = "testuser3", Password = "test789", Role = 3 },
                new User { Id = 10, Username = "demo", Password = "demo123", Role = 2 }
            };
        }
    }
}
