using BenHinkleRecipes.DataAccessLayer.Models.RepoModels;


namespace BenHinkleRecipes.DataAccessLayer.Interfaces.RepoInterfaces
{
    public interface IIngredientRepository
    {
        IEnumerable<IngredientRepoModel> GetRecipeIngredients(int id);
        void InsertIngredient(IngredientRepoModel ingredientRepo);
    }
}
