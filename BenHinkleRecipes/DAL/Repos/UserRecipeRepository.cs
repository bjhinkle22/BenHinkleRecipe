using BenHinkleRecipes.DAL.Context;
using BenHinkleRecipes.Interfaces.RepoInterfaces;
using BenHinkleRecipes.Models.RepoModels;
using Microsoft.EntityFrameworkCore;

namespace BenHinkleRecipes.DAL.Repos
{
    public class UserRecipeRepoository : IUserRecipeRepository, IDisposable
    {
        private readonly DataContext _context;
        public UserRecipeRepoository(DataContext context)
        {
            _context = context;
        }
        public IEnumerable<UserRecipeRepoModel> GetUserRecipes(string userName)
        {
            return _context.UserRecipes.Where(u => u.userName == userName);
        }
        public UserRecipeRepoModel GetRecipe(int id)
        {
            return _context.UserRecipes.Find(id);
        }
        public void UpdateUserRecipe(UserRecipeRepoModel userRecipe)
        {
            _context.Entry(userRecipe).State = EntityState.Modified;
            Save();
        }

        public void InsertUserRecipe(UserRecipeRepoModel userRecipe)
        {
            _context.UserRecipes.Add(userRecipe);
            Save();
        }

        public void DeleteUserRecipe(int id)
        {
            UserRecipeRepoModel recipe = _context.UserRecipes.Find(id);
            _context.UserRecipes.Remove(recipe);
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
