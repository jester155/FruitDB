using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FruitDB.Models
{
    public class LineItem
    {
		public LineItem() {
			ID = Guid.NewGuid();
		}

		public Guid ID { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public double Price { get; set; }
	}
}
