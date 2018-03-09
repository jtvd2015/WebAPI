using EmployeeWebAPIProject.Models;
using EmployeeWebAPIProject.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace EmployeeWebAPIProject.Controllers
{
    public class DepartmentsController : Controller
    {
        private AppDbContext db = new AppDbContext();

        public ActionResult Search(string searchCriteria)  //Postman URL: localhost:59573/Departments/Search?searchCriteria=Marketing
        {
            if (searchCriteria == null)
            {
                return Json(new JsonMessage("Failure", "Search Criteria is null"), JsonRequestBehavior.AllowGet);
            }
            List<Department> departments = db.Departments.Where(d => d.Name.Contains(searchCriteria)).ToList();
            return Json(departments, JsonRequestBehavior.AllowGet);
        }

        public ActionResult List()
        {
            return Json(db.Departments.ToList(), JsonRequestBehavior.AllowGet);
        }

        // /Departments/Get/5
        public ActionResult Get(int? id)
        {
            if (id == null)
            {
                return Json(new JsonMessage("Failure", "Id is null"), JsonRequestBehavior.AllowGet);
            }
            Department department = db.Departments.Find(id);
            if (department == null)
            {
                return Json(new JsonMessage("Failure", "Id is not found"), JsonRequestBehavior.AllowGet);
            }
            return Json(department, JsonRequestBehavior.AllowGet);
        }

        // /Departments/Create [POST]
        public ActionResult Create([FromBody] Department department)
        {
            if (!ModelState.IsValid)
            {
                return Json(new JsonMessage("Failure", "ModelState is not valid"), JsonRequestBehavior.AllowGet);
            }
            db.Departments.Add(department);
            try
            {
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                return Json(new JsonMessage("Failure", ex.Message), JsonRequestBehavior.AllowGet);
            }
            return Json(new JsonMessage("Success", "Department was created"));
        }

        // /Departments/Change [POST]
        public ActionResult Change([FromBody] Department department)
        {
            Department department2 = db.Departments.Find(department.Id);
            if (department2 == null)
            {
                return Json(new JsonMessage("Failure", "Record that needs to be changed has been deleted"), JsonRequestBehavior.AllowGet);
            }
            department2.Name = department.Name;
            department2.Budget = department.Budget;

            try
            {
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                return Json(new JsonMessage("Failure", ex.Message), JsonRequestBehavior.AllowGet);
            }
            return Json(new JsonMessage("Success", "Department was updated"));
        }

        // /Departments/Remove [POST]
        public ActionResult Remove([FromBody] Department department)
        {
            Department department2 = db.Departments.Find(department.Id);
            db.Departments.Remove(department2);
            try
            {
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                return Json(new JsonMessage("Failure", ex.Message), JsonRequestBehavior.AllowGet);
            }
            return Json(new JsonMessage("Success", "Department was removed"));
        }
    }
}