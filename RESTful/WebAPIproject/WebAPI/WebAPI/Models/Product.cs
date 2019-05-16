using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Models
{
    public class Product
    {
        public Product()
        {
            OrderItem = new HashSet<OrderItem>();
        }

        public string Id { get; set; }
        public string ProductName { get; set; }
        public int? Size { get; set; }
        public string Variety { get; set; }
        public decimal? Price { get; set; }
        public string Status { get; set; }

        public ICollection<OrderItem> OrderItem { get; set; }
    }
}
