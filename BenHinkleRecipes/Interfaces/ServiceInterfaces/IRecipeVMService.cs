using BenHinkleRecipes.DataAccessLayer.Models.RepoModels;
using BenHinkleRecipes.Models.ViewModels;

namespace BenHinkleRecipes.Interfaces.ServiceInterfaces
{
    public interface IRecipeVMService
    {
        List<RecipeRepoModel> VMListToRMList(List<RecipeVM> recipeVMs);
        List<RecipeVM> RMListToVMList(List<RecipeRepoModel> recipeRepos);
        RecipeVM RMtoVM(RecipeRepoModel recipeRepoModel);
        RecipeRepoModel VMtoRM(RecipeVM recipeVM);
        byte[] FileToByteArray(IFormFile fileName);
    }
}
