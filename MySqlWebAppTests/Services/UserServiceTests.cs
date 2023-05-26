using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MySqlWebApp.Data;
using MySqlWebApp.DataAccess;
using MySqlWebApp.Entities;
using MySqlWebApp.Requests.User;

namespace MySqlWebApp.Services.Tests
{
    [TestClass()]
    public class UserServiceTests
    {
        [TestMethod()]
        public void AddNewUser_CorrectUser_UserExists()
        {
            var options = new DbContextOptionsBuilder<DataContext>()
                        .UseInMemoryDatabase(databaseName: "UsersDatabase")
                        .Options;

            using (var context = new DataContext(options))
            {
                DbInitializer.Initialize(context);
            }

            using (var context = new DataContext(options))
            {
                UserService userService = new UserService(context);
                Assert.IsNotNull(userService);
                string firstName = "MyFirstName";
                string lastName = "MyLastName";
                string email = "MyEmail";
                var createdUser = userService.CreateUser(
                    new CreateRequest { 
                        FirstName = firstName, 
                        LastName = lastName, 
                        Role = Role.Admin.ToString(), 
                        Email = email }
                    );

                Assert.IsNotNull(createdUser);

                var user = userService.GetById(createdUser.Id);
                Assert.IsNotNull(user);
                Assert.AreEqual(createdUser.Id, user.Id);
            }
        }
    }
}