namespace KhaoPiyo
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class MyDBContext : DbContext
    {
        public MyDBContext()
            : base("MyDBContext")
        {
             System.Data.Entity.Database.SetInitializer<MyDBContext>(new CreateDatabaseIfNotExists<MyDBContext>());
        }

        public virtual DbSet<Customer_Master> Customer_Master { get; set; }
        public virtual DbSet<DataCopy> DataCopies { get; set; }
        public virtual DbSet<Item_Master> Item_Master { get; set; }
        public virtual DbSet<Order_Master> Order_Master { get; set; }
        public virtual DbSet<Order_Status> Order_Status { get; set; }
        public virtual DbSet<Reference_Master> Reference_Master { get; set; }
        public virtual DbSet<Rider_Status> Rider_Status { get; set; }
        public virtual DbSet<Temp_Order_Master> Temp_Order_Master { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Customer_Master>()
                .Property(e => e.AutoCode)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Customer_Master>()
                .Property(e => e.Biz_Id)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Customer_Master>()
                .Property(e => e.UPR_Order_Id)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Item_Master>()
                .Property(e => e.AutoCode)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Item_Master>()
                .Property(e => e.Biz_Id)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Item_Master>()
                .Property(e => e.UPR_Order_Id)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Order_Master>()
                .Property(e => e.AutoCode)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Order_Master>()
                .Property(e => e.Biz_Id)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Order_Master>()
                .Property(e => e.UPR_Order_Id)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Order_Status>()
                .Property(e => e.AutoCode)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Order_Status>()
                .Property(e => e.UPR_Order_Id)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Order_Status>()
                .Property(e => e.Order_Status_Pre)
                .IsUnicode(false);

            modelBuilder.Entity<Reference_Master>()
                .Property(e => e.AutoCode)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Rider_Status>()
                .Property(e => e.AutoCode)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Rider_Status>()
                .Property(e => e.UPR_Order_Id)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Rider_Status>()
                .Property(e => e.User_Id)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Temp_Order_Master>()
                .Property(e => e.AutoCode)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Temp_Order_Master>()
                .Property(e => e.Biz_Id)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Temp_Order_Master>()
                .Property(e => e.UPR_Order_Id)
                .HasPrecision(18, 0);
        }
    }
}
