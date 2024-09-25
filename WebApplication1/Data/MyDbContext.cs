using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Data
{
    public class MyDbContext : IdentityDbContext<ApplicationUsers>
    {
        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options) { }

        #region DbSet
        public DbSet<Book>? Books { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }   
        public DbSet<User> Users { get; set; }

        #endregion

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);  // Ensure the base configuration is run

            builder.Entity<Order>(p =>
            {
                p.ToTable("Order");
                p.HasKey(p => p.orderId);
                p.Property(p => p.oderDate).HasDefaultValueSql("GETUTCDATE()");
                p.Property(p => p.receiver).IsRequired().HasMaxLength(100);
            });

            builder.Entity<OrderDetail>(p =>
            {
                p.ToTable("OrderDetail");
                p.HasKey(p => new {p.productId, p.orderId});
                p.HasOne(p => p.Order).WithMany(e => e.OrderDetails).HasForeignKey(p=>p.orderId).HasConstraintName("FK_OrderDetail_Order");
                p.HasOne(p => p.Product).WithMany(e => e.OrderDetails).HasForeignKey(p=>p.productId).HasConstraintName("FK_OrderDetail_Product");
            });

            builder.Entity<User>(c =>
            {
                c.HasIndex(c => c.userName).IsUnique();
                c.Property(c => c.userFullName).IsRequired().HasMaxLength(150);
                c.Property(c => c.userEmail).IsRequired().HasMaxLength(150);
            });
        }
    }
}
