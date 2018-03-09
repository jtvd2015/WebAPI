using CustomerOrderProject.Models;
using CustomerOrderProject.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace CustomerOrderProject.Controllers
{
    public class CustomersController : Controller
    {
        private AppDbContext db = new AppDbContext();

        //Returns a List of Customers
        public ActionResult List()
        {
            return Json(db.Customers.ToList(), JsonRequestBehavior.AllowGet);  
        }

        // /Customers/Get/5
        public ActionResult Get(int? id)
        {
            if (id == null)
            {
                return Json(new Utility.JsonMessage("Failure", "Id is null"), JsonRequestBehavior.AllowGet);
            }
            Customers customer = db.Customers.Find(id);
            if (customer == null)
            {
                return Json(new JsonMessage("Failure", "Id is not found"), JsonRequestBehavior.AllowGet);
            }
            return Json(customer, JsonRequestBehavior.AllowGet);
        }

        // /Customers/Create [POST]
        public ActionResult Create([FromBody] Customers customer)
        {
            if (!ModelState.IsValid)
            {
                return Json(new JsonMessage("Failure", "ModelState is not valid"), JsonRequestBehavior.AllowGet);
            }
            db.Customers.Add(customer);
            try
            {
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                return Json(new JsonMessage("Failure", ex.Message), JsonRequestBehavior.AllowGet);
            }
            return Json(new JsonMessage("Success", "Customer was created"));
        }

        // /Customers/Change [POST]
        public ActionResult Change([FromBody] Customers customer)
        {
            Customers customer2 = db.Customers.Find(customer.Id);  
            if (customer2 == null)
            {
                return Json(new JsonMessage("Failure", "Record that needs to be changed has been deleted"), JsonRequestBehavior.AllowGet);
            }
            customer2.Name = customer.Name;
            customer2.CreditLimit = customer.CreditLimit;
            customer2.Active = customer.Active;

            try
            {
                db.SaveChanges();  
            }
            catch (Exception ex)
            {
                return Json(new JsonMessage("Failure", ex.Message), JsonRequestBehavior.AllowGet);
            }
            return Json(new JsonMessage("Success", "Customer was updated"));
        }

        // /Customers/Remove [POST]
        public ActionResult Remove([FromBody] Customers customer)
        {
            Customers customer2 = db.Customers.Find(customer.Id);  
            db.Customers.Remove(customer2);
            try
            {
                db.SaveChanges();  
            }
            catch (Exception ex)
            {
                return Json(new JsonMessage("Failure", ex.Message), JsonRequestBehavior.AllowGet);
            }
            return Json(new JsonMessage("Success", "Customer was removed"));
        }
    }
}