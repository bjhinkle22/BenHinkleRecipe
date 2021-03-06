using BenHinkleRecipes.Interfaces.ServiceInterfaces;
using BenHinkleRecipes.Models;
using BenHinkleRecipes.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Diagnostics;

namespace BenHinkleRecipes.Controllers
{
    public class HomeController : Controller
    {
        private readonly IRecipeService _recipeService;
        private readonly IRecipeVMService _recipeVMService;
        private readonly IUserFavoriteService _userFavoriteService;
        private readonly IIngredientService _ingredientService;
        private readonly IIngredientVMService _ingredientVMService;

        public HomeController(IRecipeService recipeService, IRecipeVMService recipeVMService, 
            IUserFavoriteService userFavoriteService, IIngredientService ingredientService, IIngredientVMService ingredientVMService)
        {
            _recipeVMService = recipeVMService;
            _recipeService = recipeService;
            _userFavoriteService = userFavoriteService;
            _ingredientService = ingredientService;
            _ingredientVMService = ingredientVMService;
        }

        public async Task<IActionResult> Index()
        {
            //Get All Recipes
            var allRecipes = await _recipeService.GetRecipesAsync();

            //Convert List of Repo Models to  List of ViewModels
            var recipeResponse = _recipeVMService.RMListToVMList(allRecipes);

            //Get current username check null, return All Recipes without doing work if null.
            var userName = HttpContext.User.Identity.Name;

            if (userName == null)
            {
                ViewBag.UserNotSignedIn = true;
                return View("Recipes", recipeResponse);
            }

            //Get Current User List of Favorites
            var userFavorites = _userFavoriteService.GetFavoriteRecipes(userName);

            //Select list of RecipeIDs from list of User's Favorite
            List<int> favorites = userFavorites.Select(x => x.recipe_id).ToList();

            //do work to assign favorite to user's favorite recipe
            foreach (RecipeVM recipe in recipeResponse)
            {
                for (int i = 0; i < favorites.Count; i++)
                {
                    if (recipe.RecipeId == favorites[i])
                    {
                        recipe.IsFavorite = true;
                    }
                }
            }
            return View("Recipes", recipeResponse);
        }

        [HttpGet]
        public ActionResult<RecipeVM> GetRecipeById(int id)
        {
            //Get Recipe by Id
            var recipeRequest = _recipeService.GetRecipe(id);

            //Convert Repo Model to View Model
            var recipeResult = _recipeVMService.RMtoVM(recipeRequest);

            //Get current username check null, return All Recipes without doing work if null.
            var userName = HttpContext.User.Identity.Name;

            //get recipe ingredients
            var ingredients = _ingredientService.GetRecipeIngredients(id).ToList();

            var ingredientVMs = _ingredientVMService.RMListToVMList(ingredients);

            recipeResult.Ingredients = ingredientVMs;

            if (userName == null)
            {
                return View("_RecipeDetails", recipeResult);
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

            return View("_RecipeDetails", recipeResult);
        }

        [HttpGet]
        public ActionResult<RecipeVM> GetRecipeFrontById(int id)
        {
            //Get Recipe by Id
            var recipeRequest = _recipeService.GetRecipe(id);

            //Convert Repo Model to View Model
            var recipeResult = _recipeVMService.RMtoVM(recipeRequest);

            //Get current username check null, return All Recipes without doing work if null.
            var userName = HttpContext.User.Identity.Name;

            //get recipe ingredients
            var ingredients = _ingredientService.GetRecipeIngredients(id).ToList();

            var ingredientVMs = _ingredientVMService.RMListToVMList(ingredients);

            recipeResult.Ingredients = ingredientVMs;

            if (userName == null)
            {
                return View("_RecipeDetailsFront", recipeResult);
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
            return View("_RecipeDetailsFront", recipeResult);
        }

        [HttpGet]
        public ActionResult<RecipeVM> CreateRecipe()
        {
            return View("_RecipeCreate");
        }

        [HttpPost]
        public ActionResult<RecipeVM> CreateRecipe(RecipeVM request)
        {
            var recipeRequest = _recipeVMService.VMtoRM(request);
            if(recipeRequest.RecipePhotoFront == null)
            {
                ViewBag.CreatedWithNullPicture = true;
                return View("_RecipeCreate", request);
            }
            if (recipeRequest.RecipePhotoBack == null)
            {
                ViewBag.CreatedWithNullPicture = true;
                return View("_RecipeCreate", request);
            }

            var recipeResponse = _recipeService.InsertRecipe(recipeRequest);
            var recipeVM = _recipeVMService.RMtoVM(recipeResponse);

            //Check if user checked Favorite & UserName isn't null
            if (request.IsFavorite == true && HttpContext.User.Identity.Name != null)
            {
                //Call Add UserFavorite
                _userFavoriteService.InsertFavoriteRecipe(HttpContext.User.Identity.Name, recipeVM.RecipeId);
                recipeVM.IsFavorite = true;
            }

            return View("_RecipeDetails", recipeVM);
        }

        [HttpGet]
        public ActionResult<RecipeVM> UpdateRecipe(int id)
        {
            var recipeRequest = _recipeService.GetRecipe(id);
            var recipeResult = _recipeVMService.RMtoVM(recipeRequest);

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
            return View("_RecipeEdit", recipeResult);
        }

        [HttpPost]
        public ActionResult<RecipeVM> UpdateRecipe(RecipeVM request)
        {

            var recipeRequest = _recipeVMService.VMtoRM(request);
            _recipeService.UpdateRecipe(recipeRequest);
            var updatedRecipe = _recipeService.GetRecipe(request.RecipeId);
            var recipeResult = _recipeVMService.RMtoVM(updatedRecipe);

            if (request.IsFavorite == true && HttpContext.User.Identity.Name != null)
            {
                //Call Add UserFavorite if Favorite doens't exist
                //Get Current User List of Favorites
                var userFavorites = _userFavoriteService.GetFavoriteRecipes(HttpContext.User.Identity.Name);

                //Select list of RecipeIDs from list of User's Favorite
                List<int> favorites = userFavorites.Select(x => x.recipe_id).ToList();

                foreach(var favorite in favorites)
                {
                    if (favorite.Equals(request.RecipeId))
                    {
                        return View("_RecipeDetails", recipeResult);
                    }
                }

                _userFavoriteService.UpdateFavorite(request.RecipeId, request.IsFavorite, HttpContext.User.Identity.Name);
                recipeResult.IsFavorite = true;
            }
            else if(request.IsFavorite == false && HttpContext.User.Identity.Name != null)
            {
                _userFavoriteService.UpdateFavorite(request.RecipeId, request.IsFavorite, HttpContext.User.Identity.Name);
                recipeResult.IsFavorite = false;
            }
            else
            {
                return View("_RecipeDetails", recipeResult);
            }
            return View("_RecipeDetails", recipeResult);
        }

        [HttpPost]
        public ActionResult<RecipeVM> DeleteRecipe(int id)
        {
            _recipeService.DeleteRecipe(id);
            if (HttpContext.User.Identity != null)
            {
                _userFavoriteService.DeleteFavoriteRecipe(HttpContext.User.Identity.Name, id);
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult<RecipeVM> GetFavoriteRecipes()
        {
            //Get current UserName
            var userName = HttpContext.User.Identity.Name;

            //Get Current User List of Favorites
            var userFavorites = _userFavoriteService.GetFavoriteRecipes(userName);

            List<int> favorites = userFavorites.Select(x => x.recipe_id).ToList();

            var allRecipes = _recipeService.GetRecipesAsync();
            var recipeResponse = _recipeVMService.RMListToVMList(allRecipes.Result);

            var favoriteRecipes = new List<RecipeVM>();

            foreach (RecipeVM recipe in recipeResponse)
            {
                for (int i = 0; i < favorites.Count; i++)
                {
                    if (recipe.RecipeId == favorites[i])
                    {
                        recipe.IsFavorite = true;
                        favoriteRecipes.Add(recipe);
                    }
                }
            }
            if(favoriteRecipes.Count > 0)
            {
                return View("FavoriteRecipes", favoriteRecipes);
            }
            return View("_EmptyFavorites");
        }

        [HttpPost]
        public void SetFavorite(int recipeId, bool check)
        {
            var userName = HttpContext.User.Identity.Name;
            _userFavoriteService.UpdateFavorite(recipeId, check, userName);
        }

        public IActionResult AddRecipeIngredient(int id)
        {
            IngredientVM ingredientVM = new IngredientVM();

            ingredientVM.userName = HttpContext.User.Identity.Name;
            ingredientVM.recipe_id = id;

            return View("IngredientAdd", ingredientVM);
        }
        [HttpPost]
        public IActionResult AddRecipeIngredient(IngredientVM ingredientVM)
        {
            ingredientVM.userName = HttpContext.User.Identity.Name;

            var ingredientRM = _ingredientVMService.VMtoRM(ingredientVM);

            _ingredientService.InsertIngredient(ingredientRM);

            IngredientVM emptyIngredient = new IngredientVM();
            return View("IngredientAdd", emptyIngredient);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorVM { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}