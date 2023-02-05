using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace XXiang.Models
{
    public partial class dbXContext : DbContext
    {
        public dbXContext()
        {
        }

        public dbXContext(DbContextOptions<dbXContext> options)
            : base(options)
        {
        }

        public virtual DbSet<TAdvertise> TAdvertises { get; set; } = null!;
        public virtual DbSet<TAorder> TAorders { get; set; } = null!;
        public virtual DbSet<TCategory> TCategories { get; set; } = null!;
        public virtual DbSet<TCorder> TCorders { get; set; } = null!;
        public virtual DbSet<TCorderDetail> TCorderDetails { get; set; } = null!;
        public virtual DbSet<TCoupon> TCoupons { get; set; } = null!;
        public virtual DbSet<TCustomer> TCustomers { get; set; } = null!;
        public virtual DbSet<TEtitle> TEtitles { get; set; } = null!;
        public virtual DbSet<TEvaluation> TEvaluations { get; set; } = null!;
        public virtual DbSet<TManager> TManagers { get; set; } = null!;
        public virtual DbSet<TProduct> TProducts { get; set; } = null!;
        public virtual DbSet<TPsite> TPsites { get; set; } = null!;
        public virtual DbSet<TPsiteRoom> TPsiteRooms { get; set; } = null!;
        public virtual DbSet<TSupplier> TSuppliers { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=dbX;Integrated Security=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TAdvertise>(entity =>
            {
                entity.HasKey(e => e.AdvertiseId)
                    .HasName("PK_Advertise");

                entity.ToTable("tAdvertise");

                entity.Property(e => e.AdvertiseId).HasColumnName("AdvertiseID");

                entity.Property(e => e.DatePrice).HasColumnType("money");

                entity.Property(e => e.Name).HasMaxLength(50);
            });

            modelBuilder.Entity<TAorder>(entity =>
            {
                entity.HasKey(e => e.AorderId)
                    .HasName("PK_AOrders");

                entity.ToTable("tAOrders");

                entity.Property(e => e.AorderId).HasColumnName("AOrderID");

                entity.Property(e => e.AdvertiseId).HasColumnName("AdvertiseID");

                entity.Property(e => e.EndDate).HasColumnType("datetime");

                entity.Property(e => e.OrderDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(sysdatetime())");

                entity.Property(e => e.Price).HasColumnType("money");

                entity.Property(e => e.SupplierId).HasColumnName("SupplierID");

                entity.HasOne(d => d.Advertise)
                    .WithMany(p => p.TAorders)
                    .HasForeignKey(d => d.AdvertiseId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_AOrders_Advertise");

                entity.HasOne(d => d.Supplier)
                    .WithMany(p => p.TAorders)
                    .HasForeignKey(d => d.SupplierId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_AOrders_Suppliers");
            });

            modelBuilder.Entity<TCategory>(entity =>
            {
                entity.HasKey(e => e.CategoryId)
                    .HasName("PK_Category");

                entity.ToTable("tCategory");

                entity.Property(e => e.CategoryId).HasColumnName("CategoryID");

                entity.Property(e => e.Name).HasMaxLength(50);
            });

            modelBuilder.Entity<TCorder>(entity =>
            {
                entity.HasKey(e => e.OrderId)
                    .HasName("PK_COrders");

                entity.ToTable("tCOrders");

                entity.Property(e => e.OrderId).HasColumnName("OrderID");

                entity.Property(e => e.CancelDate).HasColumnType("datetime");

                entity.Property(e => e.CustomerId).HasColumnName("CustomerID");

                entity.Property(e => e.EndDate).HasColumnType("datetime");

                entity.Property(e => e.OrderDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(sysdatetime())");

                entity.Property(e => e.ProductId).HasColumnName("ProductID");

                entity.Property(e => e.ReturnDate).HasColumnType("datetime");

                entity.Property(e => e.TakeDate).HasColumnType("datetime");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.TCorders)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_COrders_Customers");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.TCorders)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_COrders_Products");
            });

            modelBuilder.Entity<TCorderDetail>(entity =>
            {
                entity.HasKey(e => e.OrderId)
                    .HasName("PK_COrderDetail");

                entity.ToTable("tCOrderDetail");

                entity.Property(e => e.OrderId).HasColumnName("OrderID");

                entity.Property(e => e.CouponId).HasColumnName("CouponID");

                entity.Property(e => e.Price).HasColumnType("money");

                entity.Property(e => e.RoomId).HasColumnName("RoomID");

                entity.HasOne(d => d.Coupon)
                    .WithMany(p => p.TCorderDetails)
                    .HasForeignKey(d => d.CouponId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_COrderDetail_Coupons");

                entity.HasOne(d => d.Room)
                    .WithMany(p => p.TCorderDetails)
                    .HasForeignKey(d => d.RoomId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_COrderDetail_PSiteRoom");
            });

            modelBuilder.Entity<TCoupon>(entity =>
            {
                entity.HasKey(e => e.CouponId)
                    .HasName("PK_Coupons");

                entity.ToTable("tCoupons");

                entity.HasIndex(e => e.Code, "UQ_Coupons_Code")
                    .IsUnique();

                entity.Property(e => e.CouponId).HasColumnName("CouponID");

                entity.Property(e => e.Available).HasDefaultValueSql("((1))");

                entity.Property(e => e.Code).HasMaxLength(50);

                entity.Property(e => e.Discount).HasColumnType("money");

                entity.Property(e => e.ExpiryDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<TCustomer>(entity =>
            {
                entity.HasKey(e => e.CustomerId)
                    .HasName("PK_Customers");

                entity.ToTable("tCustomers");

                entity.HasIndex(e => e.CreditCard, "UQ_Customers_CreditCard")
                    .IsUnique();

                entity.HasIndex(e => e.Email, "UQ_Customers_Email")
                    .IsUnique();

                entity.HasIndex(e => e.Phone, "UQ_Customers_Phone")
                    .IsUnique();

                entity.Property(e => e.CustomerId).HasColumnName("CustomerID");

                entity.Property(e => e.Birth).HasColumnType("datetime");

                entity.Property(e => e.BlackListed).HasDefaultValueSql("((0))");

                entity.Property(e => e.CreditCard).HasMaxLength(50);

                entity.Property(e => e.CreditPoints).HasDefaultValueSql("((100))");

                entity.Property(e => e.Email).HasMaxLength(50);

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.Property(e => e.Password).HasMaxLength(50);

                entity.Property(e => e.Phone).HasMaxLength(50);
            });

            modelBuilder.Entity<TEtitle>(entity =>
            {
                entity.HasKey(e => e.TitleId)
                    .HasName("PK_ETitle");

                entity.ToTable("tETitle");

                entity.Property(e => e.TitleId).HasColumnName("TitleID");

                entity.Property(e => e.TitleName).HasMaxLength(50);
            });

            modelBuilder.Entity<TEvaluation>(entity =>
            {
                entity.HasKey(e => e.EvaluationId)
                    .HasName("PK_Evaluations");

                entity.ToTable("tEvaluations");

                entity.Property(e => e.EvaluationId).HasColumnName("EvaluationID");

                entity.Property(e => e.CustomerId).HasColumnName("CustomerID");

                entity.Property(e => e.Date)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(sysdatetime())");

                entity.Property(e => e.Description).HasMaxLength(200);

                entity.Property(e => e.Response).HasMaxLength(200);

                entity.Property(e => e.RoomId).HasColumnName("RoomID");

                entity.Property(e => e.TitleId).HasColumnName("TitleID");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.TEvaluations)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_Evaluations_Customers");

                entity.HasOne(d => d.Room)
                    .WithMany(p => p.TEvaluations)
                    .HasForeignKey(d => d.RoomId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_Evaluations_PSiteRoom");

                entity.HasOne(d => d.Title)
                    .WithMany(p => p.TEvaluations)
                    .HasForeignKey(d => d.TitleId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_Evaluations_ETitle");
            });

            modelBuilder.Entity<TManager>(entity =>
            {
                entity.HasKey(e => e.ManagerId)
                    .HasName("PK_Managers");

                entity.ToTable("tManagers");

                entity.HasIndex(e => e.Email, "UQ_Managers_Email")
                    .IsUnique();

                entity.HasIndex(e => e.Phone, "UQ_Managers_Phone")
                    .IsUnique();

                entity.Property(e => e.ManagerId).HasColumnName("ManagerID");

                entity.Property(e => e.Email).HasMaxLength(50);

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.Property(e => e.Password).HasMaxLength(50);

                entity.Property(e => e.Phone).HasMaxLength(50);
            });

            modelBuilder.Entity<TProduct>(entity =>
            {
                entity.HasKey(e => e.ProductId)
                    .HasName("PK_Products");

                entity.ToTable("tProducts");

                entity.Property(e => e.ProductId).HasColumnName("ProductID");

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.Property(e => e.SupplierId).HasColumnName("SupplierID");

                entity.HasOne(d => d.Supplier)
                    .WithMany(p => p.TProducts)
                    .HasForeignKey(d => d.SupplierId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_Products_Suppliers");
            });

            modelBuilder.Entity<TPsite>(entity =>
            {
                entity.HasKey(e => e.SiteId)
                    .HasName("PK_PSite");

                entity.ToTable("tPSite");

                entity.Property(e => e.SiteId).HasColumnName("SiteID");

                entity.Property(e => e.Address).HasMaxLength(200);

                entity.Property(e => e.Description).HasMaxLength(200);

                entity.Property(e => e.Image).HasMaxLength(50);

                entity.Property(e => e.Latitude).HasMaxLength(50);

                entity.Property(e => e.Longitude).HasMaxLength(50);

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.Property(e => e.OpenTime).HasMaxLength(50);

                entity.Property(e => e.ProductId).HasColumnName("ProductID");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.TPsites)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_PSite_Products");
            });

            modelBuilder.Entity<TPsiteRoom>(entity =>
            {
                entity.HasKey(e => e.RoomId)
                    .HasName("PK_PSiteRoom");

                entity.ToTable("tPSiteRoom");

                entity.Property(e => e.RoomId).HasColumnName("RoomID");

                entity.Property(e => e.CategoryId).HasColumnName("CategoryID");

                entity.Property(e => e.DatePrice).HasColumnType("money");

                entity.Property(e => e.Description).HasMaxLength(200);

                entity.Property(e => e.HourPrice).HasColumnType("money");

                entity.Property(e => e.Image).HasMaxLength(50);

                entity.Property(e => e.SiteId).HasColumnName("SiteID");

                entity.Property(e => e.Status)
                    .HasColumnName("status")
                    .HasDefaultValueSql("((0))");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.TPsiteRooms)
                    .HasForeignKey(d => d.CategoryId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_PSiteRoom_Category");

                entity.HasOne(d => d.Site)
                    .WithMany(p => p.TPsiteRooms)
                    .HasForeignKey(d => d.SiteId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_PSiteRoom_PSite");
            });

            modelBuilder.Entity<TSupplier>(entity =>
            {
                entity.HasKey(e => e.SupplierId)
                    .HasName("PK_Suppliers");

                entity.ToTable("tSuppliers");

                entity.HasIndex(e => e.Email, "UQ_Suppliers_Email")
                    .IsUnique();

                entity.HasIndex(e => e.Phone, "UQ_Suppliers_Phone")
                    .IsUnique();

                entity.Property(e => e.SupplierId).HasColumnName("SupplierID");

                entity.Property(e => e.Address).HasMaxLength(50);

                entity.Property(e => e.BlackListed).HasDefaultValueSql("((0))");

                entity.Property(e => e.CreditPoints).HasDefaultValueSql("((100))");

                entity.Property(e => e.Email).HasMaxLength(50);

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.Property(e => e.Password).HasMaxLength(50);

                entity.Property(e => e.Phone).HasMaxLength(50);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
