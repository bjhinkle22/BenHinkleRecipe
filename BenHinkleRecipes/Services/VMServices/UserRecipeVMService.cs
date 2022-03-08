using BenHinkleRecipes.Interfaces.ServiceInterfaces;
using BenHinkleRecipes.Models.RepoModels;
using BenHinkleRecipes.Models.ViewModels;

namespace BenHinkleRecipes.Services.VMServices
{
    public class UserRecipeVMService : IUserRecipeVMService
    {
        public byte[] FileToByteArray(IFormFile fileName)
        {
            MemoryStream mStream = new();
            fileName.CopyTo(mStream);
            var bytes = mStream.ToArray();
            return bytes;
        }

        public List<UserRecipeVM> RMListToVMList(List<UserRecipeRepoModel> userRecipeRepos)
        {
            List<UserRecipeVM> userRecipeVMs = new();

            foreach (UserRecipeRepoModel userRecipeRepo in userRecipeRepos)
            {
                userRecipeVMs.Add(RMtoVM(userRecipeRepo));
            }
            return userRecipeVMs;
        }

        public UserRecipeVM RMtoVM(UserRecipeRepoModel userRecipeRepoModel)
        {
            //Create new VM to take values from the Repo Model
            UserRecipeVM userRecipeVM = new();

            //Check if there is a image stored in the Repo
            if (userRecipeRepoModel.RecipePhotoFront != null)
            {
                //Convert Byte Array image into Base 64 string
                var base64Front = Convert.ToBase64String(userRecipeRepoModel.RecipePhotoFront);

                //Assign base 64 string to the Display String in the VM, Format it
                userRecipeVM.RecipeFrontDisplay = string.Format("data:image/jpg;base64,{0}", base64Front);

                //Retain original byte array, in case user posts form without image (this way user doesn't have to upload the photo on every edit)
                userRecipeVM.OriginalRecipeFront = userRecipeRepoModel.RecipePhotoFront;
            }
            //Check if there is a image stored in the Repo
            if (userRecipeRepoModel.RecipePhotoBack != null)
            {
                //Convert Byte Array image into Base 64 string
                var base64Back = Convert.ToBase64String(userRecipeRepoModel.RecipePhotoBack);

                //Assign base 64 string to the Display String in the VM, Format it
                userRecipeVM.RecipeBackDisplay = string.Format("data:image/jpg;base64,{0}", base64Back);

                //Retain original byte array, in case user posts form without image (this way user doesn't have to upload the photo on every edit)
                userRecipeVM.OriginalRecipeBack = userRecipeRepoModel.RecipePhotoBack;
            }

            //Assign values from the Repo Model to the VM for displaying
            userRecipeVM.UserRecipe_ID = userRecipeRepoModel.UserRecipe_ID;
            userRecipeVM.RecipeId = userRecipeRepoModel.recipe_id;
            userRecipeVM.RecipeName = userRecipeRepoModel.RecipeName;
            userRecipeVM.Description = userRecipeRepoModel.Description;

            return userRecipeVM;
        }

        public List<UserRecipeRepoModel> VMListToRMList(List<UserRecipeVM> userRecipeVMs)
        {
            List<UserRecipeRepoModel> userRecipeRepos = new();

            foreach (UserRecipeVM userRecipe in userRecipeVMs)
            {
                userRecipeRepos.Add(VMtoRM(userRecipe));
            }
            return userRecipeRepos;
        }

        public UserRecipeRepoModel VMtoRM(UserRecipeVM userRecipeVM)
        {
            //Create new repo model to hold Repo values
            UserRecipeRepoModel userRecipeRepoModel = new();

            //Check null, if null retain original image
            if (userRecipeVM.RecipePhotoFrontFile == null)
            {
                userRecipeRepoModel.RecipePhotoFront = userRecipeVM.OriginalRecipeFront;
            }
            if (userRecipeVM.RecipePhotoBackFile == null)
            {
                userRecipeRepoModel.RecipePhotoBack = userRecipeVM.OriginalRecipeBack;
            }
            //If file is present in the VM, convert it to byte array, and assign it to the Repo Model
            if (userRecipeVM.RecipePhotoFrontFile != null)
            {
                userRecipeRepoModel.RecipePhotoFront = FileToByteArray(userRecipeVM.RecipePhotoFrontFile);
            }
            if (userRecipeVM.RecipePhotoBackFile != null)
            {
                userRecipeRepoModel.RecipePhotoBack = FileToByteArray(userRecipeVM.RecipePhotoBackFile);
            }

            //assign vm values to RM
            userRecipeRepoModel.UserRecipe_ID = userRecipeVM.UserRecipe_ID;
            userRecipeRepoModel.recipe_id = userRecipeVM.RecipeId;
            userRecipeRepoModel.RecipeName = userRecipeVM.RecipeName;
            userRecipeRepoModel.Description = userRecipeVM.Description;

            return userRecipeRepoModel;
        }

        public UserRecipeRepoModel RecipeRMtoUserRecipeRM(RecipeRepoModel recipe)
        {
            UserRecipeRepoModel userRecipeRepoModel = new();

            //Assign values from the Repo Model to the VM for displaying
            userRecipeRepoModel.recipe_id = recipe.Id;
            userRecipeRepoModel.RecipeName = recipe.RecipeName;
            userRecipeRepoModel.Description = recipe.Description;
            userRecipeRepoModel.RecipePhotoFront = recipe.RecipePhotoFront;
            userRecipeRepoModel.RecipePhotoBack = recipe.RecipePhotoBack;

            return userRecipeRepoModel;
        }
    }
}
