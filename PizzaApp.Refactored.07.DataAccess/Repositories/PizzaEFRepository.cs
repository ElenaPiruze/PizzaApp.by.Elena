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
    public class PizzaEFRepository : IRepository<Pizza>
    {
        private readonly PizzaDbContext _pizzaThePizzaDbContext;
        public PizzaEFRepository(PizzaDbContext pizzaThePizzaDbContext)
        {
            _pizzaThePizzaDbContext = pizzaThePizzaDbContext;
        }
        public void DeleteById(int id)
        {
            Pizza pizzaDb = _pizzaThePizzaDbContext.Pizzas.FirstOrDefault(p => p.Id == id);
            if (pizzaDb == null)
            {
                throw new ResourceNotFoundException($"The order with id {id} was not found!");
            }
            _pizzaThePizzaDbContext.Pizzas.Remove(pizzaDb);
            _pizzaThePizzaDbContext.SaveChanges();
        }

        public List<Pizza> GetAll()
        {
            return _pizzaThePizzaDbContext.Pizzas.Include(x => x.PizzaOrders)
                    .ThenInclude(x => x.Pizza)
                    .Include(x => x.Id)
                    .ToList();
        }

        public Pizza GetById(int id)
        {
            return _pizzaThePizzaDbContext.Pizzas
                    .Include(x => x.PizzaOrders)
                    .ThenInclude(x => x.Pizza)
                    .FirstOrDefault(x => x.Id == id);
        }

        public int Insert(Pizza entity)
        {
            _pizzaThePizzaDbContext.Pizzas.Add(entity);
            _pizzaThePizzaDbContext.SaveChanges();
            return entity.Id;
        }

        public void Update(Pizza entity)
        {
            _pizzaThePizzaDbContext.Pizzas.Update(entity);
            _pizzaThePizzaDbContext.SaveChanges();
        }
    }
}
