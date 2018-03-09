using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EmployeeWebAPIProject.Models
{
    public class Department
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int EmployeeId { get; set; }
        public decimal Budget { get; set; }

        public virtual Employee employee { get; set; }  //an instance of the Employee gets associated with the Department
    }
}