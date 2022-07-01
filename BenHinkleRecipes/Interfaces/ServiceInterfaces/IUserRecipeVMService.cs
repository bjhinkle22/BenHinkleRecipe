using BenHinkleRecipes.DataAccessLayer.Models.RepoModels;
using BenHinkleRecipes.Models.ViewModels;

namespace BenHinkleRecipes.Interfaces.ServiceInterfaces
{
    public interface IUserRecipeVMService
    {
        List<UserRecipeRepoModel> VMListToRMList(List<UserRecipeVM> userRecipeVMs);
        List<UserRecipeVM> RMListToVMList(List<UserRecipeRepoModel> userRecipeRepos);
        UserRecipeVM RMtoVM(UserRecipeRepoModel userRecipeRepoModel);
        UserRecipeRepoModel VMtoRM(UserRecipeVM userRecipeVM);
        byte[] FileToByteArray(IFormFile fileName);

        UserRecipeRepoModel RecipeRMtoUserRecipeRM(RecipeRepoModel recipe);
    }
}
