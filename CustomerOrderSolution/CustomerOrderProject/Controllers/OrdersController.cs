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
    public class OrdersController : Controller
    {
        private AppDbContext db = new AppDbContext();

        //Returns a List of Orders
        public ActionResult List()
        {
            return Json(db.Orders.ToList(), JsonRequestBehavior.AllowGet);
        }

        // /Orders/Get/5
        public ActionResult Get(int? id)
        {
            if (id == null)
            {
                return Json(new Utility.JsonMessage("Failure", "Id is null"), JsonRequestBehavior.AllowGet);
            }
            Orders order = db.Orders.Find(id);
            if (order == null)
            {
                return Json(new JsonMessage("Failure", "Id is not found"), JsonRequestBehavior.AllowGet);
            }
            return Json(order, JsonRequestBehavior.AllowGet);
        }

        // /Orders/Create [POST]
        public ActionResult Create([System.Web.Http.FromBody] Orders orders)
        {
            if (!ModelState.IsValid)
            {
                return Json(new JsonMessage("Failure", "ModelState is not valid"), JsonRequestBehavior.AllowGet);
            }
            db.Orders.Add(orders);
            try
            {
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                return Json(new JsonMessage("Failure", ex.Message), JsonRequestBehavior.AllowGet);
            }
            return Json(new JsonMessage("Success", "Order was created"));
        }

        // /Orders/Change [POST]
        public ActionResult Change([FromBody] Orders orders)
        {
            Orders orders2 = db.Orders.Find(orders.Id);
            if (orders2 == null)
            {
                return Json(new JsonMessage("Failure", "Record that needs to be changed has been deleted"), JsonRequestBehavior.AllowGet);
            }
            orders2.CustomerId = orders.CustomerId;
            orders2.Description = orders.Description;
            orders2.Total = orders.Total;
            orders2.Fulfilled = orders.Fulfilled;

            try
            {
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                return Json(new JsonMessage("Failure", ex.Message), JsonRequestBehavior.AllowGet);
            }
            return Json(new JsonMessage("Success", "Order was updated"));
        }

        // /Orders/Remove [POST]
        public ActionResult Remove([FromBody] Orders orders)
        {
            Orders orders2 = db.Orders.Find(orders.Id);
            db.Orders.Remove(orders2);
            try
            {
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                return Json(new JsonMessage("Failure", ex.Message), JsonRequestBehavior.AllowGet);
            }
            return Json(new JsonMessage("Success", "Order was removed"));
        }
    }
}