using System;
using System.Collections.Generic;
using API_UP.Models;
using Microsoft.EntityFrameworkCore;

namespace API_UP.Data;

public partial class MyDbContext : DbContext
{
    public MyDbContext()
    {
    }

    public MyDbContext(DbContextOptions<MyDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Client> Clients { get; set; }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<EmployeeAccount> EmployeeAccounts { get; set; }

    public virtual DbSet<Operation> Operations { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<OrderSparePart> OrderSpareParts { get; set; }

    public virtual DbSet<Position> Positions { get; set; }

    public virtual DbSet<Qualification> Qualifications { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<ServiceStation> ServiceStations { get; set; }

    public virtual DbSet<SparePart> SpareParts { get; set; }

    public virtual DbSet<Status> Statuses { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=147.45.196.64,1433;Database=UP;User Id=sa;Password=12345678Al!;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Client>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Clients__3214EC27114B2BB0");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.CarBrand).HasMaxLength(50);
            entity.Property(e => e.CarModel).HasMaxLength(50);
            entity.Property(e => e.ClientName).HasMaxLength(50);
            entity.Property(e => e.GovNumber).HasMaxLength(15);
            entity.Property(e => e.Patronymic).HasMaxLength(50);
            entity.Property(e => e.Surname).HasMaxLength(50);
            entity.Property(e => e.TelephoneNumber).HasMaxLength(15);
        });

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Employee__3214EC2724FD8A92");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.EmployeeAccountId).HasColumnName("EmployeeAccountID");
            entity.Property(e => e.EmployeeName).HasMaxLength(50);
            entity.Property(e => e.Patronymic).HasMaxLength(50);
            entity.Property(e => e.Surname).HasMaxLength(50);

            entity.HasOne(d => d.EmployeeAccount).WithMany(p => p.Employees)
                .HasForeignKey(d => d.EmployeeAccountId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Employees__Emplo__440B1D61");

            entity.HasOne(d => d.Position).WithMany(p => p.Employees)
                .HasForeignKey(d => d.PositionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Employees__Posit__412EB0B6");

            entity.HasOne(d => d.Qualification).WithMany(p => p.Employees)
                .HasForeignKey(d => d.QualificationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Employees__Quali__4222D4EF");

            entity.HasOne(d => d.ServiceStation).WithMany(p => p.Employees)
                .HasForeignKey(d => d.ServiceStationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Employees__Servi__4316F928");
        });

        modelBuilder.Entity<EmployeeAccount>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Employee__3214EC27DE22AAD5");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Email).HasMaxLength(50);
            entity.Property(e => e.Login).HasMaxLength(50);
            entity.Property(e => e.Password).HasMaxLength(255);
            entity.Property(e => e.Telephone).HasMaxLength(15);

            entity.HasOne(d => d.Role).WithMany(p => p.EmployeeAccounts)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__EmployeeA__RoleI__3E52440B");
        });

        modelBuilder.Entity<Operation>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Operatio__3214EC27009E52C8");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.OperationName).HasMaxLength(50);
            entity.Property(e => e.Price).HasColumnType("decimal(10, 2)");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Orders__3214EC27CA28335E");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Description).HasMaxLength(255);
            entity.Property(e => e.Price).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.Client).WithMany(p => p.Orders)
                .HasForeignKey(d => d.ClientId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Orders__ClientId__4E88ABD4");

            entity.HasOne(d => d.Employee).WithMany(p => p.Orders)
                .HasForeignKey(d => d.EmployeeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Orders__Employee__5070F446");

            entity.HasOne(d => d.Operation).WithMany(p => p.Orders)
                .HasForeignKey(d => d.OperationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Orders__Operatio__5165187F");

            entity.HasOne(d => d.ServiceStation).WithMany(p => p.Orders)
                .HasForeignKey(d => d.ServiceStationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Orders__ServiceS__4F7CD00D");

            entity.HasOne(d => d.Status).WithMany(p => p.Orders)
                .HasForeignKey(d => d.StatusId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Orders__StatusId__52593CB8");
        });

        modelBuilder.Entity<OrderSparePart>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__OrderSpa__3214EC27ADDDDDA9");

            entity.Property(e => e.Id).HasColumnName("ID");

            entity.HasOne(d => d.Order).WithMany(p => p.OrderSpareParts)
                .HasForeignKey(d => d.OrderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__OrderSpar__Order__5629CD9C");

            entity.HasOne(d => d.SparePart).WithMany(p => p.OrderSpareParts)
                .HasForeignKey(d => d.SparePartId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__OrderSpar__Spare__5535A963");
        });

        modelBuilder.Entity<Position>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Position__3214EC27FE7F36DD");

            entity.ToTable("Position");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.PositionName).HasMaxLength(50);
        });

        modelBuilder.Entity<Qualification>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Qualific__3214EC2704A0A072");

            entity.ToTable("Qualification");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.QualificationName).HasMaxLength(50);
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Roles__3214EC27124158CA");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.RoleName).HasMaxLength(50);
        });

        modelBuilder.Entity<ServiceStation>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__ServiceS__3214EC27BAFB3C8C");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Address).HasMaxLength(100);
            entity.Property(e => e.Email).HasMaxLength(50);
            entity.Property(e => e.TelephoneNumber).HasMaxLength(15);
        });

        modelBuilder.Entity<SparePart>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__SparePar__3214EC2744EA07E0");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Articul).HasMaxLength(20);
            entity.Property(e => e.PartName).HasMaxLength(50);
            entity.Property(e => e.Price).HasColumnType("decimal(10, 2)");
        });

        modelBuilder.Entity<Status>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Statuses__3214EC274049A662");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.StatusName).HasMaxLength(20);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
