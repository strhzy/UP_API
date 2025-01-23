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

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=147.45.196.64,1433;Database=UP;User Id=sa;Password=12345678Al!;TrustServerCertificate=true;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Client>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Clients__3214EC27AE6FCCD3");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.CarBrand)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.CarModel)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.ClientName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.GovNumber)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.LastVisitDate).HasColumnType("datetime");
            entity.Property(e => e.Patronymic)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Surname)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.TelephoneNumber)
                .HasMaxLength(11)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Employee__3214EC2742E28601");

            entity.ToTable("Employee");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.EmployeeAccountId).HasColumnName("EmployeeAccountID");
            entity.Property(e => e.EmployeeName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Patronymic)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Surname)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.EmployeeAccount).WithMany(p => p.Employees)
                .HasForeignKey(d => d.EmployeeAccountId)
                .HasConstraintName("FK__Employee__Employ__440B1D61");

            entity.HasOne(d => d.Position).WithMany(p => p.Employees)
                .HasForeignKey(d => d.PositionId)
                .HasConstraintName("FK__Employee__Positi__412EB0B6");

            entity.HasOne(d => d.Qualification).WithMany(p => p.Employees)
                .HasForeignKey(d => d.QualificationId)
                .HasConstraintName("FK__Employee__Qualif__4222D4EF");

            entity.HasOne(d => d.ServiceStation).WithMany(p => p.Employees)
                .HasForeignKey(d => d.ServiceStationId)
                .HasConstraintName("FK__Employee__Servic__4316F928");
        });

        modelBuilder.Entity<EmployeeAccount>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Employee__3214EC272035A31F");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Login)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Password)
                .HasMaxLength(64)
                .IsUnicode(false);

            entity.HasOne(d => d.Role).WithMany(p => p.EmployeeAccounts)
                .HasForeignKey(d => d.RoleId)
                .HasConstraintName("FK__EmployeeA__RoleI__38996AB5");
        });

        modelBuilder.Entity<Operation>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Operatio__3214EC27992B1300");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.OperationName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Price).HasColumnType("decimal(10, 2)");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Orders__3214EC2769D9C2C1");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.DateReference).HasColumnType("datetime");
            entity.Property(e => e.RepairDate).HasColumnType("datetime");
            entity.Property(e => e.StatusName)
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.HasOne(d => d.Client).WithMany(p => p.Orders)
                .HasForeignKey(d => d.ClientId)
                .HasConstraintName("FK__Orders__ClientId__48CFD27E");
        });

        modelBuilder.Entity<OrderSparePart>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__OrderSpa__3214EC276B3C402F");

            entity.Property(e => e.Id).HasColumnName("ID");

            entity.HasOne(d => d.Order).WithMany(p => p.OrderSpareParts)
                .HasForeignKey(d => d.OrderId)
                .HasConstraintName("FK__OrderSpar__Order__5070F446");

            entity.HasOne(d => d.SparePart).WithMany(p => p.OrderSpareParts)
                .HasForeignKey(d => d.SparePartId)
                .HasConstraintName("FK__OrderSpar__Spare__4F7CD00D");
        });

        modelBuilder.Entity<Position>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Position__3214EC27BE078A2B");

            entity.ToTable("Position");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.PositionName)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Qualification>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Qualific__3214EC270298CB6C");

            entity.ToTable("Qualification");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.QualificationName)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Roles__3214EC27BC36BEEA");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.RoleName)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<ServiceStation>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__ServiceS__3214EC275F41FE89");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Address)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.TelephoneNumber)
                .HasMaxLength(11)
                .IsUnicode(false);
        });

        modelBuilder.Entity<SparePart>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__SparePar__3214EC27D6E31896");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.PartName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Price).HasColumnType("decimal(10, 2)");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
