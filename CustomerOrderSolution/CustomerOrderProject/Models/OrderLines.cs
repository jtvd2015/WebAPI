using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CustomerOrderProject.Models
{
    public class OrderLines
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int LineNbr { get; set; }
        public string Product { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public decimal LineTotal { get; set; }

        public virtual Orders order { get; set; }

        public OrderLines()
        {

        }

        public OrderLines(int id, int orderid, int linenbr, string product, decimal price, int quantity, decimal linetotal)
        {
            this.OrderId = orderid;
            this.LineNbr = linenbr;
            this.Product = product;
            this.Price = price;
            this.Quantity = quantity;
            this.LineTotal = linetotal;
        }
    }
}