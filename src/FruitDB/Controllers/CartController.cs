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
	[Produces("application/json"), Route("api/Cart")]
	public class CartController : Controller {
		private ConnectionMultiplexer Redis = ConnectionMultiplexer.Connect("localhost");
		private IDatabase RedisDB => Redis.GetDatabase(db: 1);


		[HttpPost, Route("Create/{email}")]
		public async Task<Cart> Create(string email) {

			if (!RedisDB.KeyExists(email))
				await RedisDB.HashSetAsync(email , "timestamp" , DateTime.Now.ToString());

			return await GetCart(email);
		}

		[HttpPost, Route("Update/{email}")]
		public async Task<Cart> Update(string email , [FromBody] CondensedLineItem lineItem) {
			await RedisDB.HashSetAsync(email , lineItem.ID.ToString() , lineItem.Quantity);
			await RedisDB.HashSetAsync(email , "timestamp" , DateTime.Now.ToString());
			return await GetCart(email);
		}

		[HttpDelete, Route("Delete/{email}")]
		public async Task<Cart> Delete(string email , [FromBody] Guid id) {
			await RedisDB.HashDeleteAsync(email , id.ToString());
			return await GetCart(email);
		}

		[HttpGet, Route("{email}")]
		public async Task<Cart> GetCart(string email) {
			var cartInfo = await RedisDB.HashGetAllAsync(email);
			var result = cartInfo
				.Where(i => i.Name != "timestamp").ToArray();

			var cart = new Cart() {
				UpdatedAt = DateTime.Parse(cartInfo.FirstOrDefault(i => i.Name == "timestamp").Value)
			};

			foreach (var item in result) {
				cart.CartItems.Add(
					new CartItem {
						ID = Guid.Parse(item.Name) ,
						Quantity = Convert.ToInt32(item.Value)
					}
				);
			}

			return cart;
		}
	}
}