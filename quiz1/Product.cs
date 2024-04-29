using System;
using System.Collections.Generic;
using System.Linq;

namespace quiz1
{
	internal class Product
	{
		public string Name { get; set; }
		public double Price { get; set; }
		public int Stock { get; set; }

		public Product(string name, double price, int stock)
		{
			Name = name;
			Price = price;
			Stock = stock;
		}

		public static void allProducts(List<Product> products, string order)
		{
			var orderedProducts = order.ToLower() switch
			{
				"stock" => products.OrderBy(p => p.Stock),
				"price" => products.OrderBy(p => p.Price),
				_ => products.OrderBy(p => p.Name)
			};

			Console.WriteLine("\n=== Produk yang tersedia saat ini ===");
			Console.WriteLine("|-----------------------------------------------|");
			Console.WriteLine("|   Nama      |     Harga      |      Stok      |");
			Console.WriteLine("|-----------------------------------------------|");
			foreach (var product in orderedProducts)
			{
				Console.WriteLine($"| {product.Name.Substring(10),-11} | {product.Price,-14} | {product.Stock,-14} |");
			}
			Console.WriteLine("|-----------------------------------------------|");
		}

	}
}
