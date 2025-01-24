using CarPartsStore.Data;
using CarPartsStore.Models;
using Microsoft.AspNetCore.Mvc;

namespace CarPartsStore.Controllers
{
    public class CityController : Controller
    {
        public readonly ApplicationDbContext _context;
        public CityController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var cities = _context.Cities.ToList();
            return View(cities);
        }

        public IActionResult Companies(int cityId)
        {
            var companies = _context.Companies.Where(c => c.CityId == cityId).ToList();
            return View(companies);
        }

    }
}

