using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Entity
{
    public class ConstructionStoreDb:DbContext
    {        
        public DbSet<User> Users { get; set; }
        public DbSet<Category> Categorys { get; set; }
        public DbSet<CategoryCharacteristic> CategoryCharacteristics { get; set; }
        public DbSet<Characteristic> Characteristics { get; set; }
        public DbSet<Favorite> Favorites { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductCharacteristic> ProductCharacteristics { get; set; }
        public DbSet<ProductReview> ProductReviews { get; set; }
        public DbSet<ProductOrder> PruductOrders { get; set; }
        public DbSet<TypeCharacteristic> TypeCharacteristics { get; set; }


        public ConstructionStoreDb(DbContextOptions<ConstructionStoreDb> options)
            : base(options)
        {
            Database.EnsureCreated();   // создаем базу данных при первом обращении
            //this.Users.Where(user => user.Id == 123).ToList();
        }

        public ConstructionStoreDb(string conectionString)
            : base(GetOptions(conectionString))
        {
            Database.EnsureCreated();
        }

        private static DbContextOptions GetOptions(string conectionString)
        {
            return NpgsqlDbContextOptionsBuilderExtensions.UseNpgsql(new DbContextOptionsBuilder(), conectionString).Options;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder) 
        {
            modelBuilder.HasDefaultSchema("public");

            modelBuilder.Entity<User>().Property(e => e.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<User>().HasMany(e => e.Orders).WithOne(e => e.User).HasForeignKey(e => e.IdUser).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<User>().HasMany(e => e.ProductReviews).WithOne(e => e.User).HasForeignKey(e => e.IdUser).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<User>().HasMany(e => e.Favorites).WithOne(e => e.User).HasForeignKey(e => e.IdUser).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<User>().HasIndex(e => e.Login);

            modelBuilder.Entity<Order>().Property(e => e.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<Order>().HasMany(e => e.PruductOrders).WithOne(e => e.Order).HasForeignKey(e => e.IdOrder).OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Product>().Property(e => e.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<Product>().HasMany(e => e.ProductReviews).WithOne(e => e.Product).HasForeignKey(e => e.IdProduct).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Product>().HasMany(e => e.ProductCharacteristics).WithOne(e => e.Product).HasForeignKey(e => e.IdProduct).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Product>().HasMany(e => e.Favorites).WithOne(e => e.Product).HasForeignKey(e => e.IdProduct).OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Category>().Property(e => e.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<Category>().HasMany(e => e.Products).WithOne(e => e.Category).HasForeignKey(e => e.IdCategory).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Category>().HasMany(e => e.CategoryCharacteristics).WithOne(e => e.Category).HasForeignKey(e => e.IdCategory).OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<CategoryCharacteristic>().Property(e => e.Id).ValueGeneratedOnAdd();

            modelBuilder.Entity<Characteristic>().Property(e => e.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<Characteristic>().HasMany(e => e.ProductCharacteristics).WithOne(e => e.Characteristic).HasForeignKey(e => e.IdCharacteristic).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Characteristic>().HasMany(e => e.CategoryCharacteristics).WithOne(e => e.Characteristic).HasForeignKey(e => e.IdCharacteristic).OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<TypeCharacteristic>().Property(e => e.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<TypeCharacteristic>().HasMany(e => e.Characteristics).WithOne(e => e.TypeCharacteristic).HasForeignKey(e => e.IdType).OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Favorite>().Property(e => e.Id).ValueGeneratedOnAdd();

            modelBuilder.Entity<Characteristic>().Property(e => e.Id).ValueGeneratedOnAdd();

            modelBuilder.Entity<ProductCharacteristic>().Property(e => e.Id).ValueGeneratedOnAdd();

            modelBuilder.Entity<ProductOrder>().Property(e => e.Id).ValueGeneratedOnAdd();

        }

    }
}
