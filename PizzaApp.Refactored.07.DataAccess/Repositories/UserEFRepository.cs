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
    public class UserEFRepository : IRepository<User>
    {
        private readonly PizzaDbContext _userDbContext;
        public UserEFRepository(PizzaDbContext userDbContext)
        {
            _userDbContext = userDbContext;
        }
        public void DeleteById(int id)
        {
            User userDb = _userDbContext.Users.FirstOrDefault(u => u.Id == id);
            if (userDb == null)
            {
                throw new ResourceNotFoundException($"The order with id {id} was not found!");
            }
            _userDbContext.Users.Remove(userDb);
            _userDbContext.SaveChanges();
        }

        public List<User> GetAll()
        {
            return _userDbContext.Users
                    .Include(x => x.Orders)
                    .ToList();
        }
        public User GetById(int id)
        {
            return _userDbContext.Users
                    .Include(x => x.Orders)
                    .FirstOrDefault(x => x.Id == id);
        }

        public int Insert(User entity)
        {
            _userDbContext.Users.Add(entity);
            _userDbContext.SaveChanges();
            return entity.Id;
        }

        public void Update(User entity)
        {
            _userDbContext.Users.Update(entity);
            _userDbContext.SaveChanges();
        }
    }
}
