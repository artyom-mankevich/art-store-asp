using System;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyWebApp.Data;
using MyWebApp.Services;
using MyWebApp.ViewModels;

namespace MyWebApp.Controllers
{
    public class AppController : Controller
    {
        private readonly IMailService _mailService;
        private readonly IDutchRepository _dutchRepository;
        // private readonly DutchContext _context;

        public AppController(IMailService mailService, IDutchRepository dutchRepository)
        {
            _mailService = mailService;
            _dutchRepository = dutchRepository;
            // _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("contact")]
        public IActionResult Contact()
        {
            ViewBag.Title = "Contact Us";
            return View();
        }

        [HttpPost("contact")]
        public IActionResult Contact(ContactViewModel model)
        {
            if (ModelState.IsValid)
            {
                _mailService.SendMessage("shawn@wildermuth.com", model.Subject,
                    $"From {model.Username} - {model.Email}, Message: {model.Message}");
                ViewBag.UserMessage = "Mail Sent";
                ModelState.Clear();
            }

            return View();
        }

        public IActionResult About()
        {
            ViewBag.Title = "About Us";
            return View();
        }

        [Authorize]
        public IActionResult Shop()
        {
            var results = _dutchRepository.GetAllProducts();
            return View(results);
        }
    }
}