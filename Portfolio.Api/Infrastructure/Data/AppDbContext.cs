using Microsoft.EntityFrameworkCore;
using Portfolio.Api.Core.Entities;

public class AppDbContext : DbContext, IAppDbContext
{
    /// <summary>
    /// 
    /// </summary>
    public DbSet<PortfolioUser> Users => Set<PortfolioUser>();
    public DbSet<Message> Messages => Set<Message>();
    public DbSet<PortfolioArticle> PortfolioArticles => Set<PortfolioArticle>();

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<PortfolioUser>(entity =>
        {
            entity.HasIndex(u => u.Email).IsUnique();
            entity.Property(u => u.CreatedAt)
                  .HasDefaultValueSql("CURRENT_TIMESTAMP");
        });

        modelBuilder.Entity<Message>(entity =>
        {
            entity.Property(m => m.CreatedAt)
                  .HasDefaultValueSql("CURRENT_TIMESTAMP");

            entity.HasOne(m => m.User)
                  .WithMany(u => u.Messages)
                  .HasForeignKey(m => m.UserId);
        });
        modelBuilder.Entity<PortfolioArticle>(entity =>
        {
            entity.HasIndex(p => p.Id).IsUnique();

        });
    }
}

public interface IAppDbContext
{
    DbSet<PortfolioUser> Users { get; }
    DbSet<Message> Messages { get; }
    DbSet<PortfolioArticle> PortfolioArticles { get; }

    DbSet<TEntity> Set<TEntity>() where TEntity : class;
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    int SaveChanges();
}
