using Microsoft.EntityFrameworkCore;

namespace webapi;

public class KitchHubDbContext : DbContext
{
    public KitchHubDbContext(DbContextOptions<KitchHubDbContext> options)
        : base(options)
    {
		Database.EnsureCreated();

        if (!Database.CanConnect())
            InitializeRoles();
    }

    // Данные для рецептов
    public DbSet<DishType> DishTypes { get; set; }

    public DbSet<Ingredient> Ingredients { get; set; }

    public DbSet<IngredientsType> IngredientsTypes { get; set; }

    public DbSet<NationalKitch> NationalKitches { get; set; }

    public DbSet<Recipe> Recipes { get; set; }
    // Данные для пользователей
    public DbSet<User> Users { get; set; }
    public DbSet<UserBan> UserBans { get; set; }
    public DbSet<Role> Roles { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<DishType>().ToTable(nameof(DishTypes));
		modelBuilder.Entity<Ingredient>().ToTable(nameof(Ingredients));
		modelBuilder.Entity<IngredientsType>().ToTable(nameof(IngredientsTypes));
		modelBuilder.Entity<NationalKitch>().ToTable(nameof(NationalKitches));
		modelBuilder.Entity<Recipe>().ToTable(nameof(Recipes));

        modelBuilder.Entity<User>().ToTable(nameof(Users));
        modelBuilder.Entity<UserBan>().ToTable(nameof(UserBans));
        modelBuilder.Entity<Role>().ToTable(nameof(Roles))
                                   .HasData(
                                                new Role(1,"User"),
                                                new Role(2,"Moderator"),
                                                new Role(3,"Administrator")
                                           );
    }

    private void InitializeRoles()
    {
        Roles.Add(new Role(1, "User"));
        Roles.Add(new Role(2, "Moderator"));
        Roles.Add(new Role(3, "Administrator"));
        SaveChanges();
    }
}
