using Microsoft.EntityFrameworkCore;
using PizzaApp.Refactored._07.DataAccess.Data;
using PizzaApp.Refactored._07.Domain;
using PizzaApp.Refactored._07.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaApp.Refactored._07.DataAccess.Repositories
{
    public class OrderEFRepository : IRepository<Order>
    {
        private readonly PizzaDbContext _pizzaDbContext;
        public OrderEFRepository(PizzaDbContext pizzaDbContext)
        {
            _pizzaDbContext = pizzaDbContext;
        }
        public void DeleteById(int id)
        {
            Order orderDb = _pizzaDbContext.Orders.FirstOrDefault(o => o.Id == id);
            if (orderDb == null)
            {
                throw new ResourceNotFoundException($"The order with id {id} was not found!");
            }
            _pizzaDbContext.Orders.Remove(orderDb);
            _pizzaDbContext.SaveChanges();
        }

        public List<Order> GetAll()
        {
            //select* from Orders o
            //inner join PizzaOrder po
            //on o.Id = po.OrderId
            //inner join Pizzas p
            //on po.PizzaId = p.Id
            //inner join Users u
            //on u.Id = o.UserId

            return _pizzaDbContext.Orders.Include(x => x.PizzaOrders)
                    .ThenInclude(x => x.Pizza)
                    .Include(x => x.User)
                    .ToList();
        }

        public Order GetById(int id)
        {
            return _pizzaDbContext.Orders
                    .Include(x => x.PizzaOrders)
                    .ThenInclude(x => x.Pizza)
                    .Include(x => x.User)
                    .FirstOrDefault(x => x.Id == id);
        }

        public int Insert(Order entity)
        {
            _pizzaDbContext.Orders.Add(entity);
            _pizzaDbContext.SaveChanges();
            return entity.Id;
        }

        public void Update(Order entity)
        {
            _pizzaDbContext.Orders.Update(entity);
            _pizzaDbContext.SaveChanges();
        }
    }
}
