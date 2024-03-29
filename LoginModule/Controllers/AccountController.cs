using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using LoginModule.Models;


namespace LoginModule.Controllers
{
    public class AccountController : Controller
    {
        CustomerDbEntities entities = new CustomerDbEntities();

        // GET: Account
        public ActionResult Login()
        {
            return View();
        }

        public ActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel userLogin)
        {
            bool custExist = entities.customers.Any(x => x.Email == userLogin.Email && x.Password == userLogin.Password);
            customer cus = entities.customers.FirstOrDefault(x => x.Email == userLogin.Email && x.Password == userLogin.Password);

            if (custExist)
            {
                FormsAuthentication.SetAuthCookie(cus.Email, false);
               return RedirectToAction("Index", "Home");
            }
            ModelState.AddModelError("", "Username or password is wrong");
            return View();
        }

        [HttpPost]
        public ActionResult SignUp(customer customerDetails)
        {
            entities.customers.Add(customerDetails);
            entities.SaveChanges();

            MailMessage message = new MailMessage();
            message.From = new MailAddress("demo@gmail.com");
            message.Subject = "User Registration";
            message.To.Add(new MailAddress("demo22@gmail.com"));
            message.Body = "<html><body>Registration Successfull</body></html>";
            message.IsBodyHtml = true;

            var smtpClient = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential("demo@gmail.com", "xxxxx"),
                EnableSsl=true,
            };
            smtpClient.Send(message);
            return RedirectToAction("Login");
            
        }

        public ActionResult SignOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
            //return View();
        }
    }
}