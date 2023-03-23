using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Library.Models;

namespace Library.Models;

public partial class RepoDbContext : DbContext
{
    public RepoDbContext()
    {
    }

    public RepoDbContext(DbContextOptions<RepoDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Export> Exports { get; set; }

    public virtual DbSet<Import> Imports { get; set; }

    public virtual DbSet<Person> People { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<ProductImport> ProductImports { get; set; }

    public virtual DbSet<ProductImportExport> ProductImportExports { get; set; }

    public virtual DbSet<Repository> Repositories { get; set; }

    public virtual DbSet<Supplier> Suppliers { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DESKTOP-OJM7F1K;Database=RepoDB;trusted_connection=true;encrypt=false;UID=sa;PWD=123;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Export>(entity =>
        {
            entity.ToTable("export");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.DateExport)
                .HasColumnType("datetime")
                .HasColumnName("date_export");
            entity.Property(e => e.IdRepository).HasColumnName("id_repository");
            entity.Property(e => e.IdUser).HasColumnName("id_user");
            entity.Property(e => e.TotalMoney)
                .HasColumnType("money")
                .HasColumnName("totalMoney");
        });

        modelBuilder.Entity<Import>(entity =>
        {
            entity.ToTable("import");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.DateImport)
                .HasColumnType("datetime")
                .HasColumnName("date_import");
            entity.Property(e => e.IdRepository).HasColumnName("id_repository");
            entity.Property(e => e.IdUser).HasColumnName("id_user");
            entity.Property(e => e.TotalMoney)
                .HasColumnType("money")
                .HasColumnName("totalMoney");

            entity.HasOne(d => d.IdRepositoryNavigation).WithMany(p => p.Imports)
                .HasForeignKey(d => d.IdRepository)
                .HasConstraintName("FK_import_repository");

            entity.HasOne(d => d.IdUserNavigation).WithMany(p => p.Imports)
                .HasForeignKey(d => d.IdUser)
                .HasConstraintName("FK_import_user1");
        });

        modelBuilder.Entity<Person>(entity =>
        {
            entity.HasKey(e => e.Name);

            entity.ToTable("person");

            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.Avatar)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("avatar");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("datetime")
                .HasColumnName("createdAt");
            entity.Property(e => e.Id).HasColumnName("id");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.ToTable("product");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.IdSupplier).HasColumnName("id_supplier");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
            entity.Property(e => e.Price)
                .HasColumnType("money")
                .HasColumnName("price");
            entity.Property(e => e.Unit)
                .HasMaxLength(50)
                .HasColumnName("unit");

            entity.HasOne(d => d.IdSupplierNavigation).WithMany(p => p.Products)
                .HasForeignKey(d => d.IdSupplier)
                .HasConstraintName("FK_product_supplier");
        });

        modelBuilder.Entity<ProductImport>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_product_import_1");

            entity.ToTable("product_import");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Amount).HasColumnName("amount");
            entity.Property(e => e.IdImport).HasColumnName("id_import");
            entity.Property(e => e.IdProduct).HasColumnName("id_product");
            entity.Property(e => e.IntoMoney)
                .HasColumnType("money")
                .HasColumnName("intoMoney");
            entity.Property(e => e.Price)
                .HasColumnType("money")
                .HasColumnName("price");

            entity.HasOne(d => d.IdImportNavigation).WithMany(p => p.ProductImports)
                .HasForeignKey(d => d.IdImport)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_product_import_import");

            entity.HasOne(d => d.IdProductNavigation).WithMany(p => p.ProductImports)
                .HasForeignKey(d => d.IdProduct)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_product_import_product");
        });

        modelBuilder.Entity<ProductImportExport>(entity =>
        {
            entity.ToTable("product_import_export");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AmountSold).HasColumnName("amountSold");
            entity.Property(e => e.IdExport).HasColumnName("id_export");
            entity.Property(e => e.IdProductImport).HasColumnName("id_product_import");
            entity.Property(e => e.IntoMoney)
                .HasColumnType("money")
                .HasColumnName("intoMoney");
            entity.Property(e => e.SalePrice)
                .HasColumnType("money")
                .HasColumnName("salePrice");

            entity.HasOne(d => d.IdExportNavigation).WithMany(p => p.ProductImportExports)
                .HasForeignKey(d => d.IdExport)
                .HasConstraintName("FK_product_import_export_export");

            entity.HasOne(d => d.IdProductImportNavigation).WithMany(p => p.ProductImportExports)
                .HasForeignKey(d => d.IdProductImport)
                .HasConstraintName("FK_product_import_export_product_import");
        });

        modelBuilder.Entity<Repository>(entity =>
        {
            entity.ToTable("repository");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Address)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("address");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Supplier>(entity =>
        {
            entity.ToTable("supplier");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Address)
                .HasMaxLength(100)
                .HasColumnName("address");
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("phoneNumber");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable("user");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Password)
                .IsUnicode(false)
                .HasColumnName("password");
            entity.Property(e => e.UserName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("userName");

            entity.HasMany(d => d.IdRepositories).WithMany(p => p.IdUsers)
                .UsingEntity<Dictionary<string, object>>(
                    "RepositoryUser",
                    r => r.HasOne<Repository>().WithMany()
                        .HasForeignKey("IdRepository")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_repository_user_repository"),
                    l => l.HasOne<User>().WithMany()
                        .HasForeignKey("IdUser")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_repository_user_user"),
                    j =>
                    {
                        j.HasKey("IdUser", "IdRepository");
                        j.ToTable("repository_user");
                        j.IndexerProperty<int>("IdUser").HasColumnName("id_user");
                        j.IndexerProperty<int>("IdRepository").HasColumnName("id_repository");
                    });
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
