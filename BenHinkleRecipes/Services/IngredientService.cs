using BenHinkleRecipes.Interfaces.RepoInterfaces;
using BenHinkleRecipes.Interfaces.ServiceInterfaces;
using BenHinkleRecipes.Models.RepoModels;
using BenHinkleRecipes.Models.ViewModels;

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

        public void InsertIngredient(IngredientRepoModel ingredientRepo)
        {
           _ingredientRepository.InsertIngredient(ingredientRepo);
        }
    }
}
