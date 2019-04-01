using System;
using System.Collections.Generic;

namespace WebApp.Models
{
    public class Order
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDay { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
