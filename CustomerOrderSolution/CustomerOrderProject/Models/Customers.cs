using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CustomerOrderProject.Models
{
    public class Customers
    {
        public int Id { get; set; }
        [Required] [MaxLength(50, ErrorMessage = "Max Length of Name field is 50 characters.")]
        public string Name { get; set; }
        public decimal CreditLimit { get; set; }
        public bool Active { get; set; }

        public Customers()
        {
        }

        public Customers(int id, string name, decimal creditlimit, bool active)
        {
            this.Id = id;
            this.Name = name;
            this.CreditLimit = creditlimit;
            this.Active = active;
        }


    }
}