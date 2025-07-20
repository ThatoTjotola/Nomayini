using Microsoft.EntityFrameworkCore;
using Users.Apis.Core.Entities;

public class AppDbContext : DbContext ,IAppDbContext
{
    public DbSet<User> Users => Set<User>();
    public DbSet<Message> Messages => Set<Message>();

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>(entity =>
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
    }
}

public interface IAppDbContext {

    DbSet<User> Users { get; }
    DbSet<Message> Messages { get; }

    DbSet<TEntity> Set<TEntity>() where TEntity : class;
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    int SaveChanges();


}
