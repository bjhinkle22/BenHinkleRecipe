using BenHinkleRecipes.Interfaces.ServiceInterfaces;
using BenHinkleRecipes.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace BenHinkleRecipes.Controllers
{
    public class UserRecipeController : Controller
    {
        private readonly IRecipeService _recipeService;
        private readonly IUserRecipeVMService _userRecipeVMService;
        private readonly IUserFavoriteService _userFavoriteService;
        private readonly IUserRecipeService _userRecipeService;
        private readonly IIngredientService _ingredientService;
        private readonly IIngredientVMService _ingredientVMService;

        public UserRecipeController(IRecipeService recipeService, IUserFavoriteService userFavoriteService, IUserRecipeService userRecipeService, 
            IUserRecipeVMService userRecipeVMService, IIngredientVMService ingredientVMService, IIngredientService ingredientService)
        {
            _recipeService = recipeService;
            _userFavoriteService = userFavoriteService;
            _userRecipeService = userRecipeService;
            _userRecipeVMService = userRecipeVMService;
            _ingredientVMService = ingredientVMService;
            _ingredientService = ingredientService;
        }
        public IActionResult Index()
        {
            return View();
        }
        public ActionResult GetUserRecipes()
        {
            //Get All User Recipes
            var allRecipes = _userRecipeService.GetUserRecipes(HttpContext.User.Identity.Name);

            //Convert List of Repo Models to  List of ViewModels
            var recipeResponse = _userRecipeVMService.RMListToVMList(allRecipes);

            //Get Current User List of Favorites
            var userFavorites = _userFavoriteService.GetFavoriteRecipes(HttpContext.User.Identity.Name);

            //Select list of RecipeIDs from list of User's Favorite
            List<int> favorites = userFavorites.Select(x => x.recipe_id).ToList();

            //do work to assign favorite to user's favorite recipe
            foreach (UserRecipeVM recipe in recipeResponse)
            {
                for (int i = 0; i < favorites.Count; i++)
                {
                    if (recipe.RecipeId == favorites[i])
                    {
                        recipe.IsFavorite = true;
                    }
                }
            }
            if (recipeResponse.Count > 0)
            {
                return View("_UserRecipes", recipeResponse);

            }
            return View("_EmptyUserRecipes");
        }

        [HttpGet]
        public ActionResult<UserRecipeVM> GetUserRecipeById(int id)
        {
            //Get User Recipe by Id
            var recipeRequest = _userRecipeService.GetRecipe(id);

            //Convert Repo Model to View Model
            var recipeResult = _userRecipeVMService.RMtoVM(recipeRequest);

            //Get Current User List of Favorites
            var userFavorites = _userFavoriteService.GetFavoriteRecipes(HttpContext.User.Identity.Name);

            //Select list of RecipeIDs from list of User's Favorite
            List<int> favorites = userFavorites.Select(x => x.recipe_id).ToList();

            //Adding IsFavorite to Appropriate Recipes
            for (int i = 0; i < favorites.Count; i++)
            {
                if (recipeResult.RecipeId == favorites[i])
                {
                    recipeResult.IsFavorite = true;
                }
            }
            //get recipe ingredients
            var ingredients = _ingredientService.GetRecipeIngredients(recipeResult.RecipeId).ToList();

            var ingredientVMs = _ingredientVMService.RMListToVMList(ingredients);

            recipeResult.Ingredients = ingredientVMs;

            return View("_UserRecipeDetails", recipeResult);
        }
        [HttpGet]
        public ActionResult<UserRecipeVM> GetUserRecipeFrontById(int id)
        {
            //Get User Recipe by Id
            var recipeRequest = _userRecipeService.GetRecipe(id);

            //Convert Repo Model to View Model
            var recipeResult = _userRecipeVMService.RMtoVM(recipeRequest);

            //Get Current User List of Favorites
            var userFavorites = _userFavoriteService.GetFavoriteRecipes(HttpContext.User.Identity.Name);

            //Select list of RecipeIDs from list of User's Favorite
            List<int> favorites = userFavorites.Select(x => x.recipe_id).ToList();

            //Adding IsFavorite to Appropriate Recipes
            for (int i = 0; i < favorites.Count; i++)
            {
                if (recipeResult.RecipeId == favorites[i])
                {
                    recipeResult.IsFavorite = true;
                }
            }

            //get recipe ingredients
            var ingredients = _ingredientService.GetRecipeIngredients(id).ToList();

            var ingredientVMs = _ingredientVMService.RMListToVMList(ingredients);

            recipeResult.Ingredients = ingredientVMs;

            return View("_UserRecipeFrontDetails", recipeResult);
        }

        [HttpGet]
        public ActionResult<UserRecipeVM> UpdateUserRecipe(int id)
        {
            //Get User Recipe by Id
            var recipeRequest = _userRecipeService.GetRecipe(id);

            //Convert User Recipe RM to VM
            var recipeResult = _userRecipeVMService.RMtoVM(recipeRequest);

            //Get current username check null, return All Recipes without doing work if null.
            var userName = HttpContext.User.Identity.Name;

            if (userName == null)
            {
                return View("_RecipeEdit", recipeResult);
            }

            //Get Current User List of Favorites
            var userFavorites = _userFavoriteService.GetFavoriteRecipes(userName);

            //Select list of RecipeIDs from list of User's Favorite
            List<int> favorites = userFavorites.Select(x => x.recipe_id).ToList();

            //Adding IsFavorite to Appropriate Recipes
            for (int i = 0; i < favorites.Count; i++)
            {
                if (recipeResult.RecipeId == favorites[i])
                {
                    recipeResult.IsFavorite = true;
                }
            }
            return View("_UserRecipeEdit", recipeResult);
        }

        [HttpPost]
        public ActionResult<UserRecipeVM> UpdateUserRecipe(UserRecipeVM request)
        {
            //Convert VM to RM
            var recipeRequest = _userRecipeVMService.VMtoRM(request);

            recipeRequest.userName = HttpContext.User.Identity.Name;

            //Call service with RM
            _userRecipeService.UpdateUserRecipe(recipeRequest);

            //Get Updated Recipe to Return
            var updatedRecipe = _userRecipeService.GetRecipe(request.UserRecipe_ID);

            //Convert updated RM to VM
            var recipeResult = _userRecipeVMService.RMtoVM(updatedRecipe);

            //get recipe ingredients
            var ingredients = _ingredientService.GetRecipeIngredients(request.RecipeId).ToList();

            var ingredientVMs = _ingredientVMService.RMListToVMList(ingredients);

            recipeResult.Ingredients = ingredientVMs;

            //Assign Favorties to VMs
            if (request.IsFavorite == true && HttpContext.User.Identity.Name != null)
            {

                //Get Current User List of Favorites
                var userFavorites = _userFavoriteService.GetFavoriteRecipes(HttpContext.User.Identity.Name);

                //Select list of RecipeIDs from list of User's Favorite
                List<int> favorites = userFavorites.Select(x => x.recipe_id).ToList();

                foreach (var favorite in favorites)
                {
                    if (favorite.Equals(request.RecipeId))
                    {
                        return View("_UserRecipeDetails", recipeResult);
                    }
                }
                _userFavoriteService.UpdateFavorite(request.RecipeId, request.IsFavorite, HttpContext.User.Identity.Name);
                recipeResult.IsFavorite = true;
            }
            else if (request.IsFavorite == false && HttpContext.User.Identity.Name != null)
            {
                _userFavoriteService.UpdateFavorite(request.RecipeId, request.IsFavorite, HttpContext.User.Identity.Name);
                recipeResult.IsFavorite = false;
            }
            return View("_UserRecipeDetails", recipeResult);
        }

        public ActionResult<UserRecipeVM> InsertUserRecipe(int id)
        {
            //Get Recipe by id
            var recipe = _recipeService.GetRecipe(id);

            //Check if user already has added this Recipe to their list
            var userRecipes = _userRecipeService.GetUserRecipes(HttpContext.User.Identity.Name);

            var userRecipesVMs = _userRecipeVMService.RMListToVMList(userRecipes);

            foreach (var item in userRecipesVMs)
            {
                if (recipe.Id == item.RecipeId)
                {
                    ViewBag.UserRecipeAlreadyExists = true;
                    return View("_UserRecipes", userRecipesVMs);
                }
            }

            //Convert from Recipe RM to UserRecipe RM
            var userRecipe = _userRecipeVMService.RecipeRMtoUserRecipeRM(recipe);

            //Get Username
            userRecipe.userName = HttpContext.User.Identity.Name;

            //Add RecipeRM to User Recipe Table
            _userRecipeService.InsertUserRecipe(userRecipe);

            //Get All User Recipes
            var allRecipes = _userRecipeService.GetUserRecipes(HttpContext.User.Identity.Name);

            //Convert List of Repo Models to  List of ViewModels
            var recipeResponse = _userRecipeVMService.RMListToVMList(allRecipes);

            //Get Current User List of Favorites
            var userFavorites = _userFavoriteService.GetFavoriteRecipes(HttpContext.User.Identity.Name);

            //Select list of RecipeIDs from list of User's Favorite
            List<int> favorites = userFavorites.Select(x => x.recipe_id).ToList();

            //do work to assign favorite to user's favorite recipe
            foreach (UserRecipeVM userRecipe1 in recipeResponse)
            {
                for (int i = 0; i < favorites.Count; i++)
                {
                    if (userRecipe1.RecipeId == favorites[i])
                    {
                        userRecipe1.IsFavorite = true;
                    }
                }
            }
            return View("_UserRecipes", recipeResponse);
        }

        [HttpPost]
        public ActionResult<UserRecipeVM> DeleteUserRecipe(int id)
        {
            _userRecipeService.DeleteUserRecipe(id);
            return RedirectToAction("GetUserRecipes");
        }
    }
}
