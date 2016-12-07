using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FruitDB.Models
{
    public class Cart
    {
		public DateTime UpdatedAt { get; set; }
		public List<CartItem> CartItems { get; set; }
		public Cart() {
			this.CartItems = new List<CartItem>();
		}
	}

	public class CartItem {
		public Guid ID { get; set; }
		public int Quantity { get; set; }
	}
}
