using BenHinkleRecipes.DataAccessLayer.Context;
using BenHinkleRecipes.DataAccessLayer.Interfaces.RepoInterfaces;
using BenHinkleRecipes.DataAccessLayer.Models.RepoModels;

namespace BenHinkleRecipes.DataAccessLayer.Repos
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

        public void InsertIngredient(IngredientRepoModel ingredientRepo)
        {
            _context.Ingredients.Add(ingredientRepo);
            Save();
        }
        public void Save()
        {
            _context.SaveChanges();
        }

        private bool _disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this._disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this._disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
