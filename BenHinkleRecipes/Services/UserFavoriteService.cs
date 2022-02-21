using BenHinkleRecipes.Interfaces.RepoInterfaces;
using BenHinkleRecipes.Interfaces.ServiceInterfaces;
using BenHinkleRecipes.Models.RepoModels;

namespace BenHinkleRecipes.Services
{
    public class UserFavoriteService : IUserFavoriteService
    {
        private readonly IUserFavoriteRepository _userFavoriteRepository;

        public UserFavoriteService(IUserFavoriteRepository userFavoriteRepository)
        {
            _userFavoriteRepository = userFavoriteRepository;
        }
        public List<UserFavoriteRepoModel> GetFavoriteRecipes(string userName)
        {
            List<UserFavoriteRepoModel> userFavorites = _userFavoriteRepository.GetFavoriteRecipes(userName).ToList();
            return userFavorites;
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
        public void Save()
        {
            throw new NotImplementedException();
        }
    }
}
