using MySqlWebApp.Entities;
using MySqlWebApp.Entities.Requests.User;

namespace MySqlWebApp.Services.Interfaces
{
    public interface IUserService
    {
        User CreateUser(CreateRequest createRequest);
        User Delete(int id);
        IEnumerable<User> GetAll();
        User GetById(int id);
    }
}