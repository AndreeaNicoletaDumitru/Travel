using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Travel.Services;
using Travel.ViewModels;

namespace Travel.Controllers.Web
{
    public class AppController : Controller
    {
        private IMailService mailService;
        private IConfigurationRoot config;

        public AppController(IMailService mailService, IConfigurationRoot config)
        {
            this.mailService = mailService;
            this.config = config;
        }

        public IActionResult Index()
        {
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
                //to display the error on model side, not for a specific controller
                ModelState.AddModelError("", "We don't support AOL");
            //ModelState.AddModelError("Email", "We don't support AOL");

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
