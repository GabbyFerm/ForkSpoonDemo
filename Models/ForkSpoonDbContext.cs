using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ForkSpoonDemo.Models;

public partial class ForkSpoonDbContext : DbContext
{
    public ForkSpoonDbContext()
    {
    }

    public ForkSpoonDbContext(DbContextOptions<ForkSpoonDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Favorite> Favorites { get; set; }

    public virtual DbSet<Recipe> Recipes { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=BB8\\SQLEXPRESS;Database=ForkSpoonDemoDB;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Favorite>(entity =>
        {
            entity.HasKey(e => e.FavoriteId).HasName("PK__Favorite__CE74FAD5C5A59BCF");

            entity.Property(e => e.DateFavorited)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Recipe).WithMany(p => p.Favorites)
                .HasForeignKey(d => d.RecipeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Favorites_Recipes");

            entity.HasOne(d => d.User).WithMany(p => p.Favorites)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Favorites_Users");
        });

        modelBuilder.Entity<Recipe>(entity =>
        {
            entity.HasKey(e => e.RecipeId).HasName("PK__Recipes__FDD988B0621D59D0");

            entity.Property(e => e.Category).HasMaxLength(50);
            entity.Property(e => e.Title).HasMaxLength(100);

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.Recipes)
                .HasForeignKey(d => d.CreatedBy)
                .HasConstraintName("FK_Recipes_Users");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__Users__1788CC4C8F0A9A74");

            entity.Property(e => e.Username).HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
