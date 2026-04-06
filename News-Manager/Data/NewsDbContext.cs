using Microsoft.EntityFrameworkCore;
using News_Manager.Models;

namespace News_Manager.Data;

public class NewsDbContext : DbContext {
    public NewsDbContext(DbContextOptions<NewsDbContext> options) : base(options) {
    }

    public DbSet<News> News { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder) {
        modelBuilder.Entity<News>().ToTable("TBL_NEWS");

        modelBuilder.Entity<News>().HasKey(n => n.Id);

        modelBuilder.Entity<News>()
            .Property(n => n.Title)
            .IsRequired()
            .HasMaxLength(150)
            .HasColumnName("DS_TITULO");

        modelBuilder.Entity<News>()
            .Property(n => n.Author)
            .IsRequired()
            .HasMaxLength(100)
            .HasColumnName("NM_AUTOR");

        modelBuilder.Entity<News>()
            .Property(n => n.PublishDate)
            .IsRequired()
            .HasColumnType("DATE")
            .HasColumnName("DT_PUBLICACAO");

        modelBuilder.Entity<News>()
            .Property(n => n.Category)
            .IsRequired()
            .HasConversion<int>()
            .HasColumnName("NR_CATEGORIA");

        modelBuilder.Entity<News>()
            .Property(n => n.Content)
            .IsRequired()
            .HasColumnType("CLOB")
            .HasColumnName("TX_CONTEUDO");

        modelBuilder.Entity<News>()
            .Property(n => n.IsPublished)
            .HasColumnName("ST_PUBLICADO");

        base.OnModelCreating(modelBuilder);
    }
}