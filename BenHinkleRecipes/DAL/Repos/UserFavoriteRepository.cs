using BenHinkleRecipes.DAL.Context;
using BenHinkleRecipes.Interfaces.RepoInterfaces;
using BenHinkleRecipes.Models.RepoModels;
using Microsoft.EntityFrameworkCore;

namespace BenHinkleRecipes.DAL.Repos
{
    public class UserFavoriteRepository : IUserFavoriteRepository
    {
        private DataContext _context;

        public UserFavoriteRepository(DataContext context)
        {
            _context = context;
        }
        public IEnumerable<UserFavoriteRepoModel> GetFavoriteRecipes(string userName)
        {
            return _context.UserFavorites.Where(u => u.userName == userName);
        }
        public void InsertFavoriteRecipe(string userName, int recipeId)
        {
            UserFavoriteRepoModel favorite = new UserFavoriteRepoModel();
            favorite.userName = userName;
            favorite.recipe_id = recipeId;
            _context.UserFavorites.Add(favorite);
            Save();
        }

        public void UpdateFavorite(int id, bool isFavorite, string username)
        {
            UserFavoriteRepoModel favorite = new UserFavoriteRepoModel();

            if(isFavorite == true)
            {
                favorite.userName = username;
                favorite.recipe_id = id;
                _context.UserFavorites.Add(favorite);
                Save();
            }
            if (isFavorite == false)
            {
                var favorites = _context.UserFavorites.Where(u => u.userName == username && u.recipe_id == id);

                foreach (var item in favorites)
                {
                    _context.UserFavorites.Remove(item);
                }
                Save();
            }
        }

        public void DeleteFavoriteRecipe(string userName, int recipeId)
        {
            var favorites = _context.UserFavorites.Where(u => u.userName == userName && u.recipe_id == recipeId);

            foreach(var item in favorites)
            {
                _context.UserFavorites.Remove(item);
            }
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
