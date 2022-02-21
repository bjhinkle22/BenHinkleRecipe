using BenHinkleRecipes.DAL.Context;
using BenHinkleRecipes.Interfaces.RepoInterfaces;
using BenHinkleRecipes.Models.RepoModels;

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
        public void UpdateRecipe(string userName, int recipeId)
        {
            throw new NotImplementedException();
        }

        public void DeleteFavoriteRecipe(string userName, int recipeId)
        {
            throw new NotImplementedException();
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
