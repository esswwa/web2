using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Web2API.Models;

namespace Web2API;

public partial class DbcontextContext : DbContext
{
    public DbcontextContext()
    {
        Database.EnsureCreated();
    }

    public DbcontextContext(DbContextOptions<DbcontextContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Child> Children { get; set; }

    public virtual DbSet<Division> Divisions { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySql("server=localhost;user=root;password=1234;database=dbcontext", Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.31-mysql"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_0900_ai_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<Child>(entity =>
        {
            entity.HasKey(e => e.Idchild).HasName("PRIMARY");

            entity.ToTable("child");

            entity.Property(e => e.Idchild)
                .ValueGeneratedNever()
                .HasColumnName("idchild");
            entity.Property(e => e.Date).HasColumnName("date");
            entity.Property(e => e.Gender)
                .HasMaxLength(45)
                .HasColumnName("gender");
            entity.Property(e => e.Name)
                .HasMaxLength(45)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Division>(entity =>
        {
            entity.HasKey(e => e.IdDivision).HasName("PRIMARY");

            entity.ToTable("division");

            entity.Property(e => e.IdDivision)
                .ValueGeneratedNever()
                .HasColumnName("idDivision");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Iduser).HasName("PRIMARY");

            entity.ToTable("user");

            entity.HasIndex(e => e.ChildId, "fk_child_id_idx");

            entity.HasIndex(e => e.DivisionId, "fk_division_id_idx");

            entity.Property(e => e.Iduser)
                .ValueGeneratedNever()
                .HasColumnName("iduser");
            entity.Property(e => e.Adress)
                .HasMaxLength(100)
                .HasColumnName("adress");
            entity.Property(e => e.ChildId).HasColumnName("childId");
            entity.Property(e => e.Dateb).HasColumnName("dateb");
            entity.Property(e => e.DivisionId).HasColumnName("divisionId");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
            entity.Property(e => e.Number).HasColumnName("number");
            entity.Property(e => e.Patronymic)
                .HasMaxLength(100)
                .HasColumnName("patronymic");
            entity.Property(e => e.Post)
                .HasMaxLength(100)
                .HasColumnName("post");
            entity.Property(e => e.Salary).HasColumnName("salary");
            entity.Property(e => e.Surname)
                .HasMaxLength(100)
                .HasColumnName("surname");

            entity.HasOne(d => d.Child).WithMany(p => p.Users)
                .HasForeignKey(d => d.ChildId)
                .HasConstraintName("fk_child_id");

            entity.HasOne(d => d.Division).WithMany(p => p.Users)
                .HasForeignKey(d => d.DivisionId)
                .HasConstraintName("fk_division_id");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
