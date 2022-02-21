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
        public void InsertFavoriteRecipe(string currentUserId, int recipeId)
        {
            throw new NotImplementedException();
        }
        public void UpdateRecipe(string currentUserId, int recipeId)
        {
            throw new NotImplementedException();
        }

        public void DeleteFavoriteRecipe(string currentUserId, int recipeId)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
        public void Save()
        {
            throw new NotImplementedException();
        }
    }
}
