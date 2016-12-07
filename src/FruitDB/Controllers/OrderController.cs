using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StackExchange.Redis;
using FruitDB.Models.CartModels;
using FruitDB.Data;
using FruitDB.Models;

namespace FruitDB.Controllers {
	[Produces("application/json"), Route("api/Order")]
	public class OrderController : Controller {

		private ConnectionMultiplexer Redis = ConnectionMultiplexer.Connect("localhost");
		private IDatabase RedisDB => Redis.GetDatabase(db: 1);

		[HttpPost, Route("Place/{email}")]
		public async Task<Models.Order> PlaceOrder(string email) {
			var order = new Models.Order();
			var cartController = new CartController();
			var cart = await cartController.GetCart(email);

			using (var context = ApplicationDbContext.Create()) {
				cart.CartItems.ForEach(i => {
					var item = context.LineItems.FirstOrDefault(j => j.ID == i.ID) as OrderLineItem;
					item.Quantity = i.Quantity;
					order.OrderLineItems.Add(item);
				});

				order.Total = order.OrderLineItems.Sum(i => i.Price * i.Quantity);

				await context.SaveChangesAsync();
			}

			return order;
		}
	}
}