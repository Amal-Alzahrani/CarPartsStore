using CarPartsStore.Data;
using CarPartsStore.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
namespace CarPartsStore.Controllers
{
    public class CartController : Controller
    {
        public readonly ApplicationDbContext _context;

        public CartController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {

            var userId = "TestUser"; 
            var cartItems = _context.CartItems.Where(c => c.UserId == userId).Include(c => c.Product).ToList();
            return View(cartItems);
        }


        [HttpGet]
        public IActionResult Add(int productId)
        {
            var userId = "TestUser"; 
            var cartItem = _context.CartItems.FirstOrDefault(c => c.ProductId == productId && c.UserId == userId);

            if (cartItem == null)
            {
                cartItem = new CartItem { ProductId = productId, UserId = userId, Quantity = 1 };
                _context.CartItems.Add(cartItem);
            }
            else
            {
                cartItem.Quantity++;
            }

            _context.SaveChanges();

            
            var cartCount = _context.CartItems.Where(c => c.UserId == userId).Sum(c => c.Quantity);

            
            return Json(new { success = true, cartCount });
        }



        public IActionResult Checkout()
        {
            if (HttpContext.Session.GetString("CustomerName") == null)
            {
                return RedirectToAction("Register", "Account");
            }

            // متابعة عملية الشراء
            ////return RedirectToAction("Invoice");
            var userId = "TestUser"; 
            var cartItems = _context.CartItems.Where(c => c.UserId == userId).Include(c => c.Product).ToList();

            
            if (!cartItems.Any())
            {
                TempData["Message"] = "السلة فارغة. لا يمكن طباعة فاتورة.";
                return RedirectToAction("Index");
            }

           
            _context.CartItems.RemoveRange(cartItems);
            _context.SaveChanges();

            
            return View("Invoice", cartItems);
        }

        [HttpPost]
        public IActionResult UpdateQuantity(int cartItemId, int quantity)
        {
            var cartItem = _context.CartItems.FirstOrDefault(c => c.Id == cartItemId);

            if (cartItem != null && quantity > 0)
            {
                cartItem.Quantity = quantity;
                _context.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        
        [HttpPost]
        public IActionResult Remove(int cartItemId)
        {
            var cartItem = _context.CartItems.FirstOrDefault(c => c.Id == cartItemId);

            if (cartItem != null)
            {
                _context.CartItems.Remove(cartItem);
                _context.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        public IActionResult Invoice()
        {
            // استرجاع بيانات العميل من الجلسة
            var customerName = HttpContext.Session.GetString("CustomerName");
            var customerEmail = HttpContext.Session.GetString("CustomerEmail");
            var customerPhone = HttpContext.Session.GetString("CustomerPhone");
            var customerAddress = HttpContext.Session.GetString("CustomerAddress");

            // تحقق إذا كان المستخدم مسجلاً دخوله
            if (customerName == null)
            {
                return RedirectToAction("Login", "Account");
            }

            // استرجاع بيانات السلة
            var userId = "TestUser"; // أو استبدله بالـ userId الفعلي
            var cartItems = _context.CartItems.Where(c => c.UserId == userId).Include(c => c.Product).ToList();

            // تحقق إذا كانت السلة فارغة
            if (!cartItems.Any())
            {
                TempData["Message"] = "السلة فارغة. لا يمكن طباعة فاتورة.";
                return RedirectToAction("Index");
            }

            // تمرير المعلومات إلى العرض
            ViewBag.CustomerName = customerName;
            ViewBag.CustomerEmail = customerEmail;
            ViewBag.CustomerPhone = customerPhone;
            ViewBag.CustomerAddress = customerAddress;

            // تمرير cartItems إلى الـ View
            return View(cartItems); // تأكد أن cartItems هو IEnumerable<CartItem>
        }
    }
}

