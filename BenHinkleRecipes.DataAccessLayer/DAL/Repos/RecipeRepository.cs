using BenHinkleRecipes.DataAccessLayer.Context;
using BenHinkleRecipes.DataAccessLayer.Interfaces.RepoInterfaces;
using BenHinkleRecipes.DataAccessLayer.Models.RepoModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using System;

namespace BenHinkleRecipes.DataAccessLayer.Repos
{
    public class RecipeRepository : IRecipeRepository, IDisposable
    {
        private readonly DataContext _context;
        private readonly IMemoryCache _memoryCache;
        public RecipeRepository(DataContext context, IMemoryCache memoryCache)
        {
            _context = context;
            _memoryCache = memoryCache;
        }
        public async Task<IEnumerable<RecipeRepoModel>> GetRecipesAsync()
        {
            var output = _memoryCache.Get<List<RecipeRepoModel>>("recipes");
            if (output is null)
            {
                output = await _context.Recipes.ToListAsync();
                _memoryCache.Set("recipes", output, TimeSpan.FromDays(7));
            }
            return output;
        }
        public RecipeRepoModel GetRecipe(int id)
        {
            return _context.Recipes.Find(id);
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
