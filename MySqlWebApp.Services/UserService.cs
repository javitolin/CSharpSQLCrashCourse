using MySqlWebApp.DataAccess;
using MySqlWebApp.Entities;
using MySqlWebApp.Entities.Requests.User;
using MySqlWebApp.Services.Interfaces;

// For password:
// using BCrypt.Net;
// BCrypt.HashPassword(createRequest.Password);

namespace MySqlWebApp.Services
{
    public class UserService : IUserService
    {
        private DataContext _context;

        public UserService(DataContext context)
        {
            _context = context;
        }

        public IEnumerable<User> GetAll()
        {
            return _context.Users;
        }

        public User GetById(int id)
        {
            return getUser(id);
        }

        public User Delete(int id)
        {
            var user = getUser(id);
            _context.Users.Remove(user);
            _context.SaveChanges();

            return user;
        }

        private User getUser(int id)
        {
            var user = _context.Users.Find(id);
            if (user == null) throw new KeyNotFoundException("User not found");
            return user;
        }

        public User CreateUser(CreateRequest createRequest)
        {
            var user = new User
            {
                Email = createRequest.Email,
                FirstName = createRequest.FirstName,
                LastName = createRequest.LastName,
                Role = Enum.Parse<Role>(createRequest.Role)
            };

            _context.Users.Add(user);
            _context.SaveChanges();

            return user;
        }
    }
}
