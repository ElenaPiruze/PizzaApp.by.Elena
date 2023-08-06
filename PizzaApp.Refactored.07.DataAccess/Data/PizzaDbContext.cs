using Microsoft.EntityFrameworkCore;
using PizzaApp.Refactored._07.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaApp.Refactored._07.DataAccess.Data
{
    public class PizzaDbContext : DbContext
    {
        public PizzaDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions) { }
        public DbSet<Pizza> Pizzas { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<User> Users { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            modelBuilder.Entity<Pizza>()
                .HasMany(x => x.PizzaOrders)
                .WithOne(x => x.Pizza)
                .HasForeignKey(x => x.PizzaId);

            modelBuilder.Entity<Order>()
                .HasMany(x => x.PizzaOrders)
                .WithOne(x => x.Order)
                .HasForeignKey(x => x.OrderId);

            modelBuilder.Entity<User>()
                .HasMany(x => x.Orders)
                .WithOne(x => x.User)
                .HasForeignKey(x => x.UserId);

            // Seeding is entering data into the database

            modelBuilder.Entity<Pizza>()
                .HasData(new Pizza
                {
                    Id = 1,
                    Name = "Cappricioza",
                    IsOnPromotion = true
                },
                new Pizza
                {
                    Id = 2,
                    Name = "Pepperoni",
                    IsOnPromotion = false
                },
                new Pizza
                {
                    Id = 3,
                    Name = "Margarita",
                    IsOnPromotion = false
                }
                );

            modelBuilder.Entity<User>()
                .HasData(new User
                {
                    Id = 1,
                    FirstName = "Bob",
                    LastName = "Bobsky",
                    Address = "Bob Street 22"
                },
                new User
                {
                    Id = 2,
                    FirstName = "Jill",
                    LastName = "Wayne",
                    Address = "Wayne Street 33"
                });

            modelBuilder.Entity<Order>()
                .HasData(new Order
                {
                    Id = 1,
                    PaymentMethod = PaymentMethodEnum.Card,
                    Delivered = true,
                    Location = "Store1",
                    UserId = 1
                },
                new Order
                {
                    Id = 2,
                    PaymentMethod = PaymentMethodEnum.Cash,
                    Delivered = false,
                    Location = "Store2",
                    UserId = 2
                });

            modelBuilder.Entity<PizzaOrder>()
                .HasData(new PizzaOrder
                {
                    Id = 1,
                    OrderId = 1,
                    PizzaId = 1,
                    PizzaSize = PizzaSizeEnum.Standard
                },
                new PizzaOrder
                {
                    Id = 2,
                    OrderId = 1,
                    PizzaId = 2,
                    PizzaSize = PizzaSizeEnum.Family
                },
                 new PizzaOrder
                 {
                     Id = 3,
                     OrderId = 2,
                     PizzaId = 2,
                     PizzaSize = PizzaSizeEnum.Family
                 });
        }
    }
}
