using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FruitDB.Models
{
    public class Order
    {
		public Guid ID { get; set; }
		public DateTime OrderDate { get; set; }
		public double Total { get; set; }
		public List<LineItem> LineItems { get; set; }
	}
}
