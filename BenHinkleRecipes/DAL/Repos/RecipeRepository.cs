using BenHinkleRecipes.DAL.Context;
using BenHinkleRecipes.Interfaces.RepoInterfaces;
using BenHinkleRecipes.Models.RepoModels;
using Microsoft.EntityFrameworkCore;

namespace BenHinkleRecipes.DAL.Repos
{
    public class RecipeRepository : IRecipeRepository, IDisposable
    {
        private DataContext _context;
        public RecipeRepository(DataContext context)
        {
            _context = context;
        }
        public IEnumerable<RecipeRepoModel> GetRecipes()
        {
            return _context.Recipes.ToList();
        }
        public RecipeRepoModel GetRecipe(int id)
        {
            return _context.Recipes.Find(id);
        }
        public IEnumerable<RecipeRepoModel> GetFavoriteRecipes()
        {
            return _context.Recipes.Where(predicate => predicate.IsFavorite).ToList();
        }
        public void InsertRecipe(RecipeRepoModel recipe)
        {
            _context.Recipes.Add(recipe);
            Save();
        }
        public void UpdateRecipe(RecipeRepoModel recipe)
        {
            _context.Entry(recipe).State = EntityState.Modified;
            Save();
        }
        public void DeleteRecipe(int id)
        {
            RecipeRepoModel recipe = _context.Recipes.Find(id);
            _context.Recipes.Remove(recipe);
            Save();
        }
        public void Save()
        {
            _context.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
      
    }
}
