using System;
using System.Collections.Generic;
using System.Linq;

namespace quiz1
{
	class Program
	{
		static void Main(string[] args)
		{
			List<Product> products = new List<Product>()
			{
				new Product("Laptop", 3000, 10),
				new Product("Mouse", 20, 50),
				new Product("Keyboard", 50, 30),
				new Product("Monitor", 300, 15),
				new Product("Headphone", 50, 20),
				new Product("Smartphone", 800, 25),
				new Product("Printer", 150, 10),
				new Product("Tablet", 400, 15)
			};

			bool isLoggedIn = false;

			Console.WriteLine("Selamat datang di toko kami!");

			while (!isLoggedIn)
			{
				Console.Write("Username: ");
				string username = Console.ReadLine();

				Console.Write("Password: ");
				string password = Console.ReadLine();

				if (username == User.Username && password == User.Password)
				{
					isLoggedIn = true;
					Console.WriteLine("Login berhasil!");
				}
				else
				{
					Console.WriteLine("Username atau password salah. Silakan coba lagi.");
				}
			}

			int choice;
			do
			{
				Console.WriteLine("\n=== Menu ===");
				Console.WriteLine("1. Cari Produk");
				Console.WriteLine("2. Sortir Produk berdasarkan Stok");
				Console.WriteLine("3. Tambah Produk Baru");
				Console.WriteLine("4. Hapus Produk");
				Console.WriteLine("0. Keluar");
				Console.Write("Pilihan Anda: ");
				choice = int.Parse(Console.ReadLine());

				switch (choice)
				{
					case 1:
						SearchProduct(products);
						break;
					case 2:
						SortProductsByStock(products);
						break;
					case 3:
						AddProduct(products);
						break;
					case 4:
						DeleteProduct(products);
						break;
					case 0:
						Console.WriteLine("Keluar dari aplikasi.");
						break;
					default:
						Console.WriteLine("Pilihan tidak valid.");
						break;
				}
			} while (choice != 0);
		}

		static void SearchProduct(List<Product> products)
		{
			Console.Write("Masukkan nama produk yang ingin dicari: ");
			string searchKeyword = Console.ReadLine();

			Console.Write("Masukkan harga minimum (kosongkan jika tidak ada batasan): ");
			double minPrice;
			bool hasMinPrice = double.TryParse(Console.ReadLine(), out minPrice);

			Console.Write("Masukkan harga maksimum (kosongkan jika tidak ada batasan): ");
			double maxPrice;
			bool hasMaxPrice = double.TryParse(Console.ReadLine(), out maxPrice);

			var searchResult = products.Where(p =>
				p.Name.ToLower().Contains(searchKeyword.ToLower()) &&
				(!hasMinPrice || p.Price >= minPrice) &&
				(!hasMaxPrice || p.Price <= maxPrice)
			);

			Console.WriteLine("\n=== Hasil Pencarian ===");
			foreach (var product in searchResult)
			{
				Console.WriteLine($"Nama: {product.Name}, Harga: {product.Price}, Stok: {product.Stock}");
			}
		}

		static void SortProductsByStock(List<Product> products)
		{
			Product.allProducts(products, "stock");
		}

		static void AddProduct(List<Product> products)
		{
			string input;
			do
			{
				Console.WriteLine("\n(Ketik 0 untuk kembali)");
				Console.Write("Masukkan nama produk baru: ");
				string name = Console.ReadLine();
				if (name == "0")
					return;

				Console.Write("Masukkan harga produk baru: ");
				double price;
				while (!double.TryParse(Console.ReadLine(), out price))
				{
					Console.WriteLine("Masukkan harga dengan format numerik.");
				}
				if (price == 0)
					return;

				Console.Write("Masukkan stok produk baru: ");
				int stock;
				while (!int.TryParse(Console.ReadLine(), out stock))
				{
					Console.WriteLine("Masukkan stok dengan format numerik.");
				}
				if (stock == 0)
					return;

				products.Add(new Product(name, price, stock));
				Console.WriteLine("Produk baru berhasil ditambahkan!");

				Console.Write("Tambahkan produk baru lagi? (y/n): ");
				input = Console.ReadLine();
			} while (input.ToLower() == "y");
		}

		static void DeleteProduct(List<Product> products)
		{
			Product.allProducts(products, "");

			Console.Write("Masukkan nama produk yang ingin dihapus (0: kembali): ");
			string nameToDelete = Console.ReadLine();

			if (nameToDelete == "0")
			{
				return;
			}

			var productToDelete = products.FirstOrDefault(p => p.Name.ToLower() == nameToDelete.ToLower());

			if (productToDelete != null)
			{
				products.Remove(productToDelete);
				Console.WriteLine("Produk berhasil dihapus!");
			}
			else
			{
				Console.WriteLine("Produk tidak ditemukan.");
			}
		}
	}
}
