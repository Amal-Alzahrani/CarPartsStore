using CarPartsStore.Data;
using Microsoft.AspNetCore.Mvc;

namespace CarPartsStore.Controllers
{
    public class CompanyController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public readonly ApplicationDbContext _context;

        public CompanyController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Products(int companyId)
        {
            var products = _context.Products.Where(p => p.CompanyId == companyId).ToList();
            return View(products);
        }
    }
}

