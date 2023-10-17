using Lab3.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Diagnostics;

namespace Lab3.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly PhoneBookService _phoneBook;

        public HomeController(ILogger<HomeController> logger, PhoneBookService phoneBook)
        {
            _logger = logger;
            _phoneBook = phoneBook;
        }

        public IActionResult Index()
        {
            return View(_phoneBook.GetContacts());
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Contact contact)
        {
            if (ModelState.IsValid)
            {
                _phoneBook.Add(contact);
                return RedirectToAction("Index");
            }

            return View();
        }

        public IActionResult Delete(int id)
        {
            var contact = _phoneBook.GetContacts().FirstOrDefault(x => x.Id == id);

            if (contact == null)
            {
                return NotFound();
            }

            _phoneBook.Remove(id);
            return RedirectToAction("Index");
        }


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
