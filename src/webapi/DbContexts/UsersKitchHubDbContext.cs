using Microsoft.EntityFrameworkCore;

namespace webapi;

public class UsersKitchHubDbContext : DbContext
{
    public UsersKitchHubDbContext(DbContextOptions<UsersKitchHubDbContext> options)
        : base(options)
    {
        Database.EnsureCreated();
    }

    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>().ToTable("Users");
    }
}
