using CarPartsStore.Models;
using Microsoft.AspNetCore.Mvc;

public class AccountController : Controller
{
    public readonly List<Customer> _customers = new List<Customer>(); 

    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Register(string fullName, string email, string phoneNumber, string address)
    {
        var newCustomer = new Customer
        {
            Id = _customers.Count + 1,
            FullName = fullName,
            Email = email,
            PhoneNumber = phoneNumber,
            Address = address
        };

        
        _customers.Add(newCustomer);

       
        HttpContext.Session.SetString("CustomerName", fullName);
        HttpContext.Session.SetString("CustomerEmail", email);
        HttpContext.Session.SetString("CustomerPhone", phoneNumber);
        HttpContext.Session.SetString("CustomerAddress", address);

        return RedirectToAction("Invoice", "Cart");
    }
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Login(string email)
    {
       
        var customer = _customers.FirstOrDefault(c => c.Email == email);
        if (customer == null)
        {
            ViewBag.Message = "البريد الإلكتروني غير صحيح.";
            return View();
        }

       
        HttpContext.Session.SetString("CustomerName", customer.FullName);
        HttpContext.Session.SetString("CustomerEmail", customer.Email);
        HttpContext.Session.SetString("CustomerPhone", customer.PhoneNumber);
        HttpContext.Session.SetString("CustomerAddress", customer.Address);

        return RedirectToAction("Invoice", "Cart");
    }
}
