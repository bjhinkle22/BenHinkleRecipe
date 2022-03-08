using BenHinkleRecipes.DAL.Context;
using BenHinkleRecipes.Interfaces.RepoInterfaces;
using BenHinkleRecipes.Models.RepoModels;

namespace BenHinkleRecipes.DAL.Repos
{
    public class IngredientRepository : IIngredientRepository
    {
        private readonly DataContext _context;

        public IngredientRepository(DataContext context)
        {
            _context = context;
        }
        public IEnumerable<IngredientRepoModel> GetRecipeIngredients(int id)
        {
            return _context.Ingredients.Where(u => u.recipe_id == id);
        }

        public IEnumerable<IngredientRepoModel> GetRecipeIngredients(string userName)
        {
            return _context.Ingredients.Where(u => u.userName == userName);
        }

        public IEnumerable<IngredientRepoModel> GetRecipeIngredients(string userName, int id)
        {
            return _context.Ingredients.Where(u => u.recipe_id == id && u.userName == userName);
        }
    }
}
