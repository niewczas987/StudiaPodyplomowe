using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ServerAPI.Models
{
    public partial class ProductsAndServicesContext : DbContext
    {
        public ProductsAndServicesContext()
        {
        }

        public ProductsAndServicesContext(DbContextOptions<ProductsAndServicesContext> options)
            : base(options)
        {
        }

        public virtual DbSet<EmployeeDepartment> EmployeeDepartment { get; set; }
        public virtual DbSet<EmployeeGrade> EmployeeGrade { get; set; }
        public virtual DbSet<EmployeePosition> EmployeePosition { get; set; }
        public virtual DbSet<EmployeeRise> EmployeeRise { get; set; }
        public virtual DbSet<Employees> Employees { get; set; }
        public virtual DbSet<Item> Item { get; set; }
        public virtual DbSet<ItemCategory> ItemCategory { get; set; }
        public virtual DbSet<ItemType> ItemType { get; set; }
        public virtual DbSet<Transactions> Transactions { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=ProductsAndServices;Integrated Security=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EmployeeDepartment>(entity =>
            {
                entity.Property(e => e.DepartmentName).HasMaxLength(50);
            });

            modelBuilder.Entity<EmployeeGrade>(entity =>
            {
                entity.Property(e => e.Idemployee).HasColumnName("IDEmployee");

                entity.Property(e => e.TimeStamp).HasColumnType("datetime");

                entity.HasOne(d => d.IdemployeeNavigation)
                    .WithMany(p => p.EmployeeGrade)
                    .HasForeignKey(d => d.Idemployee)
                    .HasConstraintName("FK__EmployeeG__IDEmp__32E0915F");
            });

            modelBuilder.Entity<EmployeePosition>(entity =>
            {
                entity.Property(e => e.PositionName).HasMaxLength(50);
            });

            modelBuilder.Entity<EmployeeRise>(entity =>
            {
                entity.Property(e => e.Idemployee).HasColumnName("IDEmployee");

                entity.Property(e => e.TimeStamp).HasColumnType("datetime");

                entity.HasOne(d => d.IdemployeeNavigation)
                    .WithMany(p => p.EmployeeRise)
                    .HasForeignKey(d => d.Idemployee)
                    .HasConstraintName("FK__EmployeeR__IDEmp__33D4B598");
            });

            modelBuilder.Entity<Employees>(entity =>
            {
                entity.Property(e => e.Address).HasMaxLength(50);

                entity.Property(e => e.City).HasMaxLength(50);

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.Property(e => e.Salary).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.Surname).HasMaxLength(50);

                entity.HasOne(d => d.DepartmentNavigation)
                    .WithMany(p => p.Employees)
                    .HasForeignKey(d => d.Department)
                    .HasConstraintName("FK__Employees__Depar__35BCFE0A");

                entity.HasOne(d => d.PositionNavigation)
                    .WithMany(p => p.Employees)
                    .HasForeignKey(d => d.Position)
                    .HasConstraintName("FK__Employees__Posit__34C8D9D1");
            });

            modelBuilder.Entity<Item>(entity =>
            {
                entity.Property(e => e.ItemName).HasMaxLength(50);

                entity.Property(e => e.ItemPrice).HasColumnType("decimal(18, 0)");

                entity.HasOne(d => d.ItemCategoryNavigation)
                    .WithMany(p => p.Item)
                    .HasForeignKey(d => d.ItemCategory)
                    .HasConstraintName("FK__Item__ItemCatego__36B12243");

                entity.HasOne(d => d.ItemTypeNavigation)
                    .WithMany(p => p.Item)
                    .HasForeignKey(d => d.ItemType)
                    .HasConstraintName("FK__Item__ItemType__37A5467C");
            });

            modelBuilder.Entity<ItemCategory>(entity =>
            {
                entity.Property(e => e.CategoryName).HasMaxLength(50);
            });

            modelBuilder.Entity<ItemType>(entity =>
            {
                entity.Property(e => e.TypeName).HasMaxLength(50);
            });

            modelBuilder.Entity<Transactions>(entity =>
            {
                entity.Property(e => e.TimeStamp).HasColumnType("datetime");

                entity.Property(e => e.TotalPrice).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.UnitPrice).HasColumnType("decimal(18, 0)");

                entity.HasOne(d => d.IdItemNavigation)
                    .WithMany(p => p.Transactions)
                    .HasForeignKey(d => d.IdItem)
                    .HasConstraintName("FK__Transacti__IdIte__38996AB5");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
