using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PizzaApp.Refactored._07.DataAccess;
using PizzaApp.Refactored._07.DataAccess.Data;
using PizzaApp.Refactored._07.DataAccess.Repositories;
using PizzaApp.Refactored._07.Domain;
using PizzaApp.Refactored._07.Services;

namespace System // Piggybacking
{
    public static class DependencyInjectionHelper
    {
        public static void InjectServices(this IServiceCollection services)
        {
            services.AddTransient<IOrderService, OrderService>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IPizzaService, PizzaService>();
        }

        public static void InjectRepositories(this IServiceCollection services)
        {
            //Entity framework repositories
            services.AddTransient<IRepository<Order>, OrderEFRepository>();
            services.AddTransient<IRepository<User>, UserEFRepository>();
            services.AddTransient<IRepository<Pizza>, PizzaEFRepository>();

            //services.AddTransient<IRepository<Order>, OrderRepository>();
            //services.AddTransient<IRepository<User>, UserRepository>();
            services.AddTransient<IPizzaRepository, PizzaRepository>();
        }
        public static void InjectDbContext(this IServiceCollection services)
        {
            services.AddDbContext<PizzaDbContext>(options =>
                options.UseSqlServer("Server=localhost;Database=PizzaDbTest;Trusted_Connection=True;TrustServerCertificate=True")
            );
        }
    }
}
