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
    public class EmployeesController : Controller
    {
        private AppDbContext db = new AppDbContext();  //this is how we allow EF to access

        public ActionResult ActiveEmployees()  //this will get a list of active employees
        {
            List<Employee> employees = db.Employees.Where(e => e.Active).ToList();  //e => e.Active can be set to e => e.Active == true); but that is a rookie way of doing it and what it is set to now, e => e.Active is a bool and it will result in true, thus returning the active employees
            return Json(employees, JsonRequestBehavior.AllowGet);
        }

        public ActionResult List()
        {
            return Json(db.Employees.ToList(), JsonRequestBehavior.AllowGet);  //this line needs to be included so that the Json data can be accessed
        }

        // /Employees/Get/5
        public ActionResult Get(int? id)
        {
            if (id == null)
            {
                return Json(new JsonMessage("Failure", "Id is null"), JsonRequestBehavior.AllowGet);
            }
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return Json(new JsonMessage("Failure", "Id is not found"), JsonRequestBehavior.AllowGet);
            }
            return Json(employee, JsonRequestBehavior.AllowGet);
        }

        // /Employees/Create [POST]
        public ActionResult Create([FromBody] Employee employee)  //this will pass an entire Employee instance with Name, Salary, Active data; [System.Web.Http.FromBody] is an attribute because it is in [ ], FromBody existed in 2 different namespaces: Http and MVC, this tells MVC that the data for the employee is in the body
        {
            if (!ModelState.IsValid)  //!ModelState.IsValid means not valid
            {
                return Json(new JsonMessage("Failure", "ModelState is not valid"), JsonRequestBehavior.AllowGet);  //any required field(s), string values that exceed max characters will generate this error message
            }
            db.Employees.Add(employee);
            try
            {
                db.SaveChanges();  //this adds the new data to the database; put in a try, catch block to ensure that if there are issues with the update, the error is caught and shown to the user
            }
            catch (Exception ex)
            {
                return Json(new JsonMessage("Failure", ex.Message), JsonRequestBehavior.AllowGet);
            }
            return Json(new JsonMessage("Success", "Employee was created"));
        }

        // /Employees/Change [POST]
        public ActionResult Change([FromBody] Employee employee)
        {
            Employee employee2 = db.Employees.Find(employee.Id);  //this was named employee2 because there was already a parameter of employee
            if (employee2 == null)
            {
                return Json(new JsonMessage("Failure", "Record that needs to be changed has been deleted"), JsonRequestBehavior.AllowGet);
            }
            employee2.Name = employee.Name;
            employee2.Salary = employee.Salary;
            employee2.Active = employee.Active;

            try
            {
                db.SaveChanges();  //this adds the new data to the database; put in a try, catch block to ensure that if there are issues with the update, the error is caught and shown to the user
            }
            catch (Exception ex)
            {
                return Json(new JsonMessage("Failure", ex.Message), JsonRequestBehavior.AllowGet);
            }
            return Json(new JsonMessage("Success", "Employee was updated"));
        }

        // /Employees/Remove [POST]
        public ActionResult Remove([FromBody] Employee employee)
        {
            Employee employee2 = db.Employees.Find(employee.Id);  //this was named employee2 because there was already a parameter of employee
            db.Employees.Remove(employee2);
            try
            {
                db.SaveChanges();  //this adds the new data to the database; put in a try, catch block to ensure that if there are issues with the update, the error is caught and shown to the user
            }
            catch (Exception ex)
            {
                return Json(new JsonMessage("Failure", ex.Message), JsonRequestBehavior.AllowGet);
            }
            return Json(new JsonMessage("Success", "Employee was removed"));
        }
    }
}