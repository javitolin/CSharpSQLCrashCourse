using MySqlWebApp.DataAccess;
using MySqlWebApp.Entities;
using Faker;

namespace MySqlWebApp.Data
{
    public static class DbInitializer
    {
        public static void Initialize(DataContext context)
        {
            if (context.Users.Any())
            {
                return; // No need
            }

            var numberOfUsersToCreate = 10;
            var users = new User[numberOfUsersToCreate];

            for (int i = 0; i < numberOfUsersToCreate; i++)
            {
                users[i] = new User
                {
                    FirstName = Faker.Name.First(),
                    LastName = Faker.Name.Last(),
                    Email = Faker.Internet.Email(),
                    Role = Faker.RandomNumber.Next(0, 10) > 7 ? Role.Admin : Role.User
                };
            }

            context.Users.AddRange(users);
            context.SaveChanges();
        }
    }
}
