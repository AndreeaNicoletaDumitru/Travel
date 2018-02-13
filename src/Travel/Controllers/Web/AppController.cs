using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Linq;
using Travel.Models;
using Travel.Services;
using Travel.ViewModels;

namespace Travel.Controllers.Web
{
    public class AppController : Controller
    {
        private IMailService mailService;
        private IConfigurationRoot config;
        private TravelContext context;

        public AppController(IMailService mailService, IConfigurationRoot config, TravelContext context)
        {
            this.mailService = mailService;
            this.config = config;
            this.context = context;
            //testing 
        }

        public IActionResult Index()
        {
            var data = context.Trips.ToList();

            return View();
        }

        public IActionResult Contact()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Contact(ContactViewModel contact)
        {
            if (contact.Email.Contains("aol.com"))
                ModelState.AddModelError("", "We don't support AOL");

            if (ModelState.IsValid)
            {
                mailService.SendMail(config["MailSettings:ToAddress"], contact.Email, contact.Name, contact.Message);
                ModelState.Clear();
                ViewBag.UserMessage = "Message Sent";
            }
            return View();
        }

        public IActionResult About()
        {
            return View();
        }
    }
}
