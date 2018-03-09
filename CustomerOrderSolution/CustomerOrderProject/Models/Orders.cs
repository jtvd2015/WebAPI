using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CustomerOrderProject.Models
{
    public class Orders
    {
        public int Id { get; set; }
        public int? CustomerId { get; set; }
        [Required] [MaxLength(80, ErrorMessage = "Max Length of Description field is 80 characters.")]
        public string Description { get; set; }
        public decimal Total { get; set; }
        public bool Fulfilled { get; set; }

        public virtual Customers customer { get; set; }

        public Orders()
        {

        }

        public Orders(int id, int customerid, string description, decimal total, bool fulfilled)
        {
            this.CustomerId = customerid;
            this.Description = description;
            this.Total = total;
            this.Fulfilled = fulfilled;
        }
    }
}