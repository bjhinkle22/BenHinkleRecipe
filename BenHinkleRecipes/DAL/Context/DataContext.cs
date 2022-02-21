using BenHinkleRecipes.Models.RepoModels;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BenHinkleRecipes.DAL.Context
{
    public class DataContext : IdentityDbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }
        
        public DbSet<RecipeRepoModel> Recipes { get; set; }
        public DbSet<UserFavoriteRepoModel> UserFavorites { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Write Fluent API configurations here

            modelBuilder.Entity<UserFavoriteRepoModel>()
                    .HasNoKey();
            base.OnModelCreating(modelBuilder);
        }
    }
}
