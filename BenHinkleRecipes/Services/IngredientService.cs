using BenHinkleRecipes.Interfaces.RepoInterfaces;
using BenHinkleRecipes.Interfaces.ServiceInterfaces;
using BenHinkleRecipes.Models.RepoModels;

namespace BenHinkleRecipes.Services
{
    public class IngredientService : IIngredientService
    {
        private readonly IIngredientRepository _ingredientRepository;

        public IngredientService(IIngredientRepository ingredientRepository)
        {
            _ingredientRepository = ingredientRepository;
        }

        public IEnumerable<IngredientRepoModel> GetRecipeIngredients(int id)
        {
            return _ingredientRepository.GetRecipeIngredients(id).ToList();
        }

        public IEnumerable<IngredientRepoModel> GetRecipeIngredients(string userName)
        {
            return _ingredientRepository.GetRecipeIngredients(userName).ToList();
        }

        public IEnumerable<IngredientRepoModel> GetRecipeIngredients(string userName, int id)
        {
            return _ingredientRepository.GetRecipeIngredients(userName, id).ToList();
        }
    }
}
