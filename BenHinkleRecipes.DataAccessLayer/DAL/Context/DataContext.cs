using BenHinkleRecipes.DataAccessLayer.Models.RepoModels;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BenHinkleRecipes.DataAccessLayer.Context
{
    public class DataContext : IdentityDbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }
        
        public DbSet<RecipeRepoModel> Recipes { get; set; }
        public DbSet<UserFavoriteRepoModel> UserFavorites { get; set; }
        public DbSet<UserRecipeRepoModel> UserRecipes { get; set; }
        public DbSet<GroceryListRepoModel> GroceryLists { get; set; }
        public DbSet<IngredientRepoModel> Ingredients { get; set; }
    }
}
