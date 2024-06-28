using System;
using System.Collections.Generic;
using ApiLibros.Models.Identity;
using ApiLibros.Models.Library;
using Microsoft.EntityFrameworkCore;

namespace ApiLibros.ContextDB;

public partial class LibrosDbContext : DbContext
{
    public LibrosDbContext()
    {
    }

    public LibrosDbContext(DbContextOptions<LibrosDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AspNetRole> AspNetRoles { get; set; }

    public virtual DbSet<AspNetRoleClaim> AspNetRoleClaims { get; set; }

    public virtual DbSet<AspNetUser> AspNetUsers { get; set; }

    public virtual DbSet<AspNetUserClaim> AspNetUserClaims { get; set; }

    public virtual DbSet<AspNetUserLogin> AspNetUserLogins { get; set; }

    public virtual DbSet<AspNetUserToken> AspNetUserTokens { get; set; }

    public virtual DbSet<Categorium> Categoria { get; set; }

    public virtual DbSet<Libro> Libros { get; set; }

    public virtual DbSet<Resena> Resenas { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseNpgsql("Name=ConnectionStrings:DefaultConnection");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AspNetRole>(entity =>
        {
            entity.HasIndex(e => e.NormalizedName, "RoleNameIndex").IsUnique();

            entity.Property(e => e.Name).HasMaxLength(256);
            entity.Property(e => e.NormalizedName).HasMaxLength(256);
        });

        modelBuilder.Entity<AspNetRoleClaim>(entity =>
        {
            entity.HasIndex(e => e.RoleId, "IX_AspNetRoleClaims_RoleId");

            entity.HasOne(d => d.Role).WithMany(p => p.AspNetRoleClaims).HasForeignKey(d => d.RoleId);
        });

        modelBuilder.Entity<AspNetUser>(entity =>
        {
            entity.HasIndex(e => e.NormalizedEmail, "EmailIndex");

            entity.HasIndex(e => e.NormalizedUserName, "UserNameIndex").IsUnique();

            entity.Property(e => e.Email).HasMaxLength(256);
            entity.Property(e => e.NormalizedEmail).HasMaxLength(256);
            entity.Property(e => e.NormalizedUserName).HasMaxLength(256);
            entity.Property(e => e.UserName).HasMaxLength(256);

            entity.HasMany(d => d.Roles).WithMany(p => p.Users)
                .UsingEntity<Dictionary<string, object>>(
                    "AspNetUserRole",
                    r => r.HasOne<AspNetRole>().WithMany().HasForeignKey("RoleId"),
                    l => l.HasOne<AspNetUser>().WithMany().HasForeignKey("UserId"),
                    j =>
                    {
                        j.HasKey("UserId", "RoleId");
                        j.ToTable("AspNetUserRoles");
                        j.HasIndex(new[] { "RoleId" }, "IX_AspNetUserRoles_RoleId");
                    });
        });

        modelBuilder.Entity<AspNetUserClaim>(entity =>
        {
            entity.HasIndex(e => e.UserId, "IX_AspNetUserClaims_UserId");

            entity.HasOne(d => d.User).WithMany(p => p.AspNetUserClaims).HasForeignKey(d => d.UserId);
        });

        modelBuilder.Entity<AspNetUserLogin>(entity =>
        {
            entity.HasKey(e => new { e.LoginProvider, e.ProviderKey });

            entity.HasIndex(e => e.UserId, "IX_AspNetUserLogins_UserId");

            entity.HasOne(d => d.User).WithMany(p => p.AspNetUserLogins).HasForeignKey(d => d.UserId);
        });

        modelBuilder.Entity<AspNetUserToken>(entity =>
        {
            entity.HasKey(e => new { e.UserId, e.LoginProvider, e.Name });

            entity.HasOne(d => d.User).WithMany(p => p.AspNetUserTokens).HasForeignKey(d => d.UserId);
        });

        modelBuilder.Entity<Categorium>(entity =>
        {
            entity.HasKey(e => e.Idcategoria).HasName("Categoria_pkey");

            entity.Property(e => e.Idcategoria)
                .UseIdentityAlwaysColumn()
                .HasColumnName("idcategoria");
            entity.Property(e => e.Nombrecategoria)
                .HasMaxLength(255)
                .HasColumnName("nombrecategoria");
        });

        modelBuilder.Entity<Libro>(entity =>
        {
            entity.HasKey(e => e.Idlibro).HasName("Libro_pkey");

            entity.ToTable("Libro");

            entity.Property(e => e.Idlibro)
                .UseIdentityAlwaysColumn()
                .HasColumnName("idlibro");
            entity.Property(e => e.Autorlibro)
                .HasMaxLength(255)
                .HasColumnName("autorlibro");
            entity.Property(e => e.Idcategoria).HasColumnName("idcategoria");
            entity.Property(e => e.Resumenlibro)
                .HasMaxLength(255)
                .HasColumnName("resumenlibro");
            entity.Property(e => e.Titulolibro)
                .HasMaxLength(255)
                .HasColumnName("titulolibro");

            entity.HasOne(d => d.IdcategoriaNavigation).WithMany(p => p.Libros)
                .HasForeignKey(d => d.Idcategoria)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("foraneacategoria");
        });

        modelBuilder.Entity<Resena>(entity =>
        {
            entity.HasKey(e => e.Idresena).HasName("Resena_pkey");

            entity.ToTable("Resena");

            entity.Property(e => e.Idresena)
                .UseIdentityAlwaysColumn()
                .HasColumnName("idresena");
            entity.Property(e => e.Calificacionresena).HasColumnName("calificacionresena");
            entity.Property(e => e.Fecharesena).HasColumnName("fecharesena");
            entity.Property(e => e.Idlibro).HasColumnName("idlibro");

            entity.HasOne(d => d.IdlibroNavigation).WithMany(p => p.Resenas)
                .HasForeignKey(d => d.Idlibro)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("foraneaLibro");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
