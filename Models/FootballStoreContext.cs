using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using FootballStoreApp.Models;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace FootballStoreApp.Models
{
    public partial class FootballStoreContext : DbContext
    {
        public FootballStoreContext()
        {
        }

        public FootballStoreContext(DbContextOptions<FootballStoreContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Item> Items { get; set; } = null!;
        public virtual DbSet<Customer> Customers { get; set; } = null!;
        public virtual DbSet<Order> Orders { get; set; } = null!;
        public virtual DbSet<OrderItem> OrderItems { get; set; } = null!;
        public virtual DbSet<Product> Products { get; set; } = null!;
        public virtual DbSet<Category> Categories { get; set; } = null!;
        public virtual DbSet<CategoryDetail> CategoryDetails { get; set; } = null!;


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                // ⚠️ У Production краще налаштовувати через DI та appsettings.json
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>(entity =>
            {
                entity.ToTable("customers");

                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.Email).HasColumnName("email");
                entity.Property(e => e.FullName).HasColumnName("full_name");
                entity.Property(e => e.Phone).HasColumnName("phone");
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.ToTable("orders");

                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.CustomerId).HasColumnName("customer_id");
                entity.Property(e => e.OrderDate).HasColumnName("order_date");
                entity.Property(e => e.Status).HasColumnName("status");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.CustomerId);
            });

            modelBuilder.Entity<OrderItem>(entity =>
            {
                entity.ToTable("order_items");

                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.OrderId).HasColumnName("order_id");
                entity.Property(e => e.ProductId).HasColumnName("product_id");
                entity.Property(e => e.Quantity).HasColumnName("quantity");
                entity.Property(e => e.UnitPrice).HasColumnName("unit_price");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.OrderItems)
                    .HasForeignKey(d => d.OrderId);

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.OrderItems)
                    .HasForeignKey(d => d.ProductId);
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("products");

                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.Category).HasColumnName("category");
                entity.Property(e => e.Description).HasColumnName("description");
                entity.Property(e => e.Name).HasColumnName("name");
                entity.Property(e => e.Price).HasColumnName("price");
                entity.Property(e => e.StockQuantity).HasColumnName("stock_quantity");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        // 🟨 Додаємо SaveChanges() для аудиту
        public override int SaveChanges()
        {
            const int systemUserId = 0;
            const int loggedInUserId = 123; // умовний поточний користувач

            var entries = ChangeTracker.Entries<FullAuditModel>();

            foreach (var entry in entries)
            {
                var now = DateTime.UtcNow;

                // Примусово конвертуємо дати до UTC, якщо не встановлено Kind
                if (entry.Entity is Item item)
                {
                    if (item.PurchasedDate.HasValue && item.PurchasedDate.Value.Kind == DateTimeKind.Unspecified)
                        item.PurchasedDate = DateTime.SpecifyKind(item.PurchasedDate.Value, DateTimeKind.Utc);

                    if (item.SoldDate.HasValue && item.SoldDate.Value.Kind == DateTimeKind.Unspecified)
                        item.SoldDate = DateTime.SpecifyKind(item.SoldDate.Value, DateTimeKind.Utc);
                }

                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedDate = now;
                        entry.Entity.CreatedByUserId = loggedInUserId;
                        entry.Entity.IsActive = true;
                        break;

                    case EntityState.Modified:
                        entry.Entity.LastModifiedDate = now;
                        entry.Entity.LastModifiedUserId = loggedInUserId;
                        break;

                    case EntityState.Deleted:
                        // Soft-delete
                        entry.State = EntityState.Modified;
                        entry.Entity.IsActive = false;
                        entry.Entity.LastModifiedDate = now;
                        entry.Entity.LastModifiedUserId = systemUserId;
                        break;
                }
            }

            return base.SaveChanges();
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
