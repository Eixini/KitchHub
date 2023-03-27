using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace webapi;

public class KitchHubDbContext : DbContext
{
    public KitchHubDbContext()
    {
    }

    public KitchHubDbContext(DbContextOptions<KitchHubDbContext> options)
        : base(options)
    {
		Database.EnsureCreated();
	}

    public DbSet<DishType> DishTypes { get; set; }

    public DbSet<Ingredient> Ingredients { get; set; }

    public DbSet<IngredientsType> IngredientsTypes { get; set; }

    public DbSet<NationalKitch> NationalKitches { get; set; }

    public DbSet<Recipe> Recipes { get; set; }

 //   protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
   //     => optionsBuilder.UseSqlite("Data Source=Data\\KitchHubDB.db");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<DishType>().ToTable("DishTypes");
		modelBuilder.Entity<Ingredient>().ToTable("Ingredients");
		modelBuilder.Entity<IngredientsType>().ToTable("IngredientsType");
		modelBuilder.Entity<NationalKitch>().ToTable("NationalKitch");
		modelBuilder.Entity<Recipe>().ToTable("Recipes");
	}
}
