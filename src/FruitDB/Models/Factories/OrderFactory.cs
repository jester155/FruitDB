using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FruitDB.Models.Factories
{
    public static class OrderFactory
    {
		public static Order CreateOrder(this IEnumerable<LineItem> items) =>
			new Order {
				ID = Guid.NewGuid() ,
				OrderDate = DateTime.Now ,
				LineItems = items.ToList() ,
				Total = items.Sum(i => i.Price)
			};
    }
}
