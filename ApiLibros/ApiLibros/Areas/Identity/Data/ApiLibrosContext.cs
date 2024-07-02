using ApiLibros.Areas.Identity.Data;
using ApiLibros.Models.Library.Category;
using ApiLibros.Models.Library.Libros;
using ApiLibros.Models.Library.Resenas;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ApiLibros.Data;

public class ApiLibrosContext : IdentityDbContext<ApiLibrosUser>
{
    public ApiLibrosContext(DbContextOptions<ApiLibrosContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Categorium> Categoria { get; set; }
    public virtual DbSet<Libro> Libros { get; set; }

    public virtual DbSet<Resena> Resenas { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseNpgsql("Name=ConnectionStrings:DefaultConnection");
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        // Customize the ASP.NET Identity model and override the defaults if needed.
        // For example, you can rename the ASP.NET Identity table names and more.
        // Add your customizations after calling base.OnModelCreating(builder);
        builder.Entity<Categorium>(entity =>
        {
            entity.HasKey(e => e.Idcategoria).HasName("Categoria_pkey");

            entity.Property(e => e.Idcategoria)
                .UseIdentityAlwaysColumn()
                .HasColumnName("idcategoria");
            entity.Property(e => e.Nombrecategoria)
                .HasMaxLength(255)
                .HasColumnName("nombrecategoria");
        });

        builder.Entity<Libro>(entity =>
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

        builder.Entity<Resena>(entity =>
        {
            entity.HasKey(e => e.Idresena).HasName("Resena_pkey");

            entity.ToTable("Resena");

            entity.Property(e => e.Idresena)
                .UseIdentityAlwaysColumn()
                .HasColumnName("idresena");
            entity.Property(e => e.Calificacionresena).HasColumnName("calificacionresena");
            entity.Property(e => e.Fecharesena).HasColumnName("fecharesena");
            entity.Property(e => e.Idlibro).HasColumnName("idlibro");
            entity.Property(e => e.Descripcionresena)
                .HasMaxLength(256)
                .HasColumnName("descripcionresena");
            entity.Property(e => e.Aspnetuser)
                .HasMaxLength(256)
                .HasColumnName("aspnetuser");
            entity.HasOne(d => d.IdlibroNavigation).WithMany(p => p.Resenas)
                .HasForeignKey(d => d.Idlibro)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("foraneaLibro");
        });
    }
}
