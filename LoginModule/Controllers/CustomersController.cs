using LoginModule.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace LoginModule.Controllers
{
    public class CustomersController : Controller
    {
        CustomerDbEntities entities = new CustomerDbEntities();
        // GET: Customers
        public async Task<ActionResult> CustomerList()
        {
            var res = entities.customers.ToList();
            return View(res);
        }

        public ActionResult Edit(int id)
        {
            var data = entities.customers.Where(x => x.Id == id).FirstOrDefault();
            return View(data);
          
        }

     

        [HttpPost]
        public ActionResult Edit(customer obj)
        {
            var data = entities.customers.Where(x => x.Id == obj.Id).FirstOrDefault();
            if (data != null)
            {
                data.Id = obj.Id;
                data.FirstName = obj.FirstName;
                data.LastName = obj.LastName;
                data.Email = obj.Email;
                data.PhoneNumber = obj.PhoneNumber;
                data.Password = obj.Password;
                data.LoginUser = obj.LoginUser;
                entities.SaveChanges();
            }

            return RedirectToAction("CustomerList");

        }

        public ActionResult Delete(int id)
        {
            var res = entities.customers.Where(x => x.Id == id).First();
            entities.customers.Remove(res);
            entities.SaveChanges();

            var list = entities.customers.ToList();
            return View("CustomerList", list);
        }
    }
}