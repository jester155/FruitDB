using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FruitDB.Models
{
    public class Order
    {

		public Order() {
			ID = Guid.NewGuid();
			OrderLineItems = new List<OrderLineItem>();
			OrderDate = DateTime.Now;
		}

		public Guid ID { get; set; }
		public DateTime OrderDate { get; set; }
		public double Total { get; set; }
		public List<OrderLineItem> OrderLineItems { get; set; }
	}
}
