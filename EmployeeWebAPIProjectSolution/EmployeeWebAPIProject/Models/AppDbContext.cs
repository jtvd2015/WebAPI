using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace EmployeeWebAPIProject.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext() : base()  //this sets up a constructor for AppDbContext, base is a constructor for whatever the class is inheriting from
        {
        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Department> Departments { get; set; }
    }
}