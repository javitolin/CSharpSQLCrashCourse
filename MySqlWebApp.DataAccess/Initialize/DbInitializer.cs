using Faker;
using MySqlWebApp.Entities;

namespace MySqlWebApp.DataAccess.Initialize
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
                    FirstName = Name.First(),
                    LastName = Name.Last(),
                    Email = Internet.Email(),
                    Role = RandomNumber.Next(0, 10) > 7 ? Role.Admin : Role.User
                };
            }

            context.Users.AddRange(users);
            context.SaveChanges();
        }
    }
}
