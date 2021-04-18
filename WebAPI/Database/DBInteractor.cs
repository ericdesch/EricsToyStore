using System;
using Microsoft.EntityFrameworkCore;
using WebAPI.Models;

namespace WebAPI
{
	public class DBInteractor : DbContext
	{

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseSqlite(@"Data Source = Database/ToyStore.db;");
		}

		public DbSet<Toy> Toy { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Toy>().HasData(
				new Toy() { Id = 1, Name = "Jack in the Box", Price = 999 },
				new Toy() { Id = 2, Name = "Buzz Lightyear", Price = 2499 },
				new Toy() { Id = 3, Name = "Etch-A-Sketch", Price = 1998 }
			);
		}
	}
}