using Microsoft.EntityFrameworkCore;
using MySqlWebApp.DataAccess;
using MySqlWebApp.DataAccess.Initialize;
using MySqlWebApp.Services;
using MySqlWebApp.Services.Interfaces;
using System.Text.Json.Serialization;

namespace MySqlWebApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            var connectionString = builder.Configuration.GetConnectionString("WebApiDatabase");
            builder.Services.AddDbContext<DataContext>(options =>
                options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

            builder.Services.AddControllers().AddJsonOptions(x =>
            {
                // serialize enums as strings in api responses (e.g. Role)
                x.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());

                // ignore omitted parameters on models to enable optional params (e.g. User update)
                x.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
            });

            builder.Services.AddScoped<IUserService, UserService>();

            builder.Services.AddDatabaseDeveloperPageExceptionFilter();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            else
            {
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();
            }

            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                var context = services.GetRequiredService<DataContext>();
                context.Database.EnsureCreated();
                DbInitializer.Initialize(context);
            }

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}