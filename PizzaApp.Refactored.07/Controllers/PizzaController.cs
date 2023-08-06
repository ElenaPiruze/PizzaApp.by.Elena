using Microsoft.AspNetCore.Mvc;
using PizzaApp.Refactored._07.Services;
using PizzaApp.Refactored._07.Shared;
using PizzaApp.Refactored._07.ViewModels;

namespace PizzaApp.Refactored._07.Controllers
{
    public class PizzaController : Controller
    {
        private IOrderService _orderService;
        private IUserService _userService;
        private IPizzaService _pizzaService;
        public PizzaController(IOrderService orderService, IUserService userService, IPizzaService pizzaService) //DependencyInjectionHelper -> map
        {
            _orderService = orderService;
            _userService = userService;
            _pizzaService = pizzaService;
        }
        public IActionResult Index()
        {
            PizzaViewModel pizzaViewModel = new PizzaViewModel();
            pizzaViewModel.Name = _pizzaService.GetPizzaOnPromotion();
            return View(pizzaViewModel);
        }

        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return View("BadRequest");
            }
            try
            {
                OrderDetailsViewModel orderDetailsViewModel = _orderService.GetOrderDetails(id.Value);
                return View(orderDetailsViewModel);
            }
            catch (Exception e)
            {
                // We can add loggs here
                return View("ExceptionPage" + e);

            }
        }
    }
}
