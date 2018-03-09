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
    public class OrderLinesController : Controller
    {
        private AppDbContext db = new AppDbContext();

        //Returns a List of OrderLines
        public ActionResult List()
        {
            return Json(db.OrderLines.ToList(), JsonRequestBehavior.AllowGet);
        }

        // /OrderLines/Get/5
        public ActionResult Get(int? id)
        {
            if (id == null)
            {
                return Json(new Utility.JsonMessage("Failure", "Id is null"), JsonRequestBehavior.AllowGet);
            }
            OrderLines orderLines = db.OrderLines.Find(id);
            if (orderLines == null)
            {
                return Json(new JsonMessage("Failure", "Id is not found"), JsonRequestBehavior.AllowGet);
            }
            return Json(orderLines, JsonRequestBehavior.AllowGet);
        }

        // /OrderLines/Create [POST]
        public ActionResult Create([FromBody] OrderLines orderLines)
        {
            if (!ModelState.IsValid)
            {
                return Json(new JsonMessage("Failure", "ModelState is not valid"), JsonRequestBehavior.AllowGet);
            }
            db.OrderLines.Add(orderLines);
            try
            {
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                return Json(new JsonMessage("Failure", ex.Message), JsonRequestBehavior.AllowGet);
            }
            return Json(new JsonMessage("Success", "Order Line was created"));
        }

        // /OrderLines/Change [POST]
        public ActionResult Change([FromBody] OrderLines orderLines)
        {
            OrderLines orderLines2 = db.OrderLines.Find(orderLines.Id);
            if (orderLines2 == null)
            {
                return Json(new JsonMessage("Failure", "Record that needs to be changed has been deleted"), JsonRequestBehavior.AllowGet);
            }
            orderLines2.OrderId = orderLines.OrderId;
            orderLines2.LineNbr = orderLines.LineNbr;
            orderLines2.Product = orderLines.Product;
            orderLines2.Price = orderLines.Price;
            orderLines2.Quantity = orderLines.Quantity;
            orderLines2.LineTotal = orderLines.LineTotal;

            try
            {
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                return Json(new JsonMessage("Failure", ex.Message), JsonRequestBehavior.AllowGet);
            }
            return Json(new JsonMessage("Success", "Order Lines was updated"));
        }

        // /OrderLines/Remove [POST]
        public ActionResult Remove([FromBody] OrderLines orderLines)
        {
            OrderLines orderLines2 = db.OrderLines.Find(orderLines.Id);
            db.OrderLines.Remove(orderLines2);
            try
            {
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                return Json(new JsonMessage("Failure", ex.Message), JsonRequestBehavior.AllowGet);
            }
            return Json(new JsonMessage("Success", "Order Line was removed"));
        }
    }
}