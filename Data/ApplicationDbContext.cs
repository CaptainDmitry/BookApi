using BookApi.Data;
using BookApi.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System;
using System.Linq;

namespace BookApi.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Book> Books { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OrderItem>()
                .HasOne(oi => oi.Order)
                .WithMany(o => o.OrderItems)
                .HasForeignKey(oi => oi.OrderId);

            modelBuilder.Entity<OrderItem>()
                .HasOne(oi => oi.Book)
                .WithMany()
                .HasForeignKey(oi => oi.BookId);
        }
    }

    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new ApplicationDbContext(
                serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>()))
            {
                if (context.Books.Any())
                {
                    return;
                }

                var book1 = new Book { Title = "Война и мир", Author = "Лев Толстой", PublicationDate = new DateTime(1869, 1, 1), Price = 1200.50m };
                var book2 = new Book { Title = "Мастер и Маргарита", Author = "Михаил Булгаков", PublicationDate = new DateTime(1967, 1, 1), Price = 950.00m };
                var book3 = new Book { Title = "Преступление и наказание", Author = "Федор Достоевский", PublicationDate = new DateTime(1866, 1, 1), Price = 800.00m };

                context.Books.AddRange(
                    book1,
                    book2,
                    book3
                );
                context.SaveChanges();

                var order1 = new Order { OrderDate = DateTime.UtcNow.AddDays(-7) };
                var order2 = new Order { OrderDate = DateTime.UtcNow.AddDays(-3) };

                context.Orders.AddRange(
                    order1,
                    order2
                );
                context.SaveChanges();

                context.OrderItems.AddRange(
                    new OrderItem { OrderId = order1.Id, BookId = book1.Id, Quantity = 1 },
                    new OrderItem { OrderId = order1.Id, BookId = book2.Id, Quantity = 2 },
                    new OrderItem { OrderId = order2.Id, BookId = book3.Id, Quantity = 1 }
                );
                context.SaveChanges();
            }
        }
    }
}