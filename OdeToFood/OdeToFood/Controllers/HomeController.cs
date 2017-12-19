using Microsoft.AspNetCore.Mvc;
using OdeToFood.Models;
using OdeToFood.Services;
using OdeToFood.ViewModels;

namespace OdeToFood.Controllers
{
    public class HomeController : Controller
    {
        private IRestaurantData _restaurantDataObject;
        private IGreeter _greeter;

        public HomeController(IRestaurantData restaurantData, 
                              IGreeter greeter)
        {
            _restaurantDataObject = restaurantData;
            _greeter = greeter;
        }
        public IActionResult Index()
        {
            HomeIndexViewModel model = new HomeIndexViewModel();
            model.Restaurants = _restaurantDataObject.GetAll();
            model.CurrentMessage = _greeter.GetMessageOfTheDay();

            return View(model);
        }

        public IActionResult Details(int id)
        {
            var model = _restaurantDataObject.Get(id);
            if (model == null)
            {
                return NotFound();
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        //public IActionResult Create([Bind("Name, Cuisine")] Restaurant model)
        public IActionResult Create(Restaurant model)
        {
            if (ModelState.IsValid)
            {
                var newRestaurant = new Restaurant();
                newRestaurant.Name = model.Name;
                newRestaurant.Cuisine = model.Cuisine;

                newRestaurant = _restaurantDataObject.Add(newRestaurant);

                return RedirectToAction(nameof(Details), new { id = newRestaurant.Id });
            }
            else
            {
                return View();
            }
        }
    }
}
