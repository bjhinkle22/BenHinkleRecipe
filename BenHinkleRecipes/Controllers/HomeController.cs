using BenHinkleRecipes.Interfaces.ServiceInterfaces;
using BenHinkleRecipes.Models;
using BenHinkleRecipes.Models.ViewModels;
using BenHinkleRecipes.Services.VMServices;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Diagnostics;
using System;
using Microsoft.AspNetCore.Identity;
using BenHinkleRecipes.Models.RepoModels;

namespace BenHinkleRecipes.Controllers
{
    public class HomeController : Controller
    {
        private readonly IRecipeService _recipeService;
        private readonly IRecipeVMService _recipeVMService;
        private readonly IUserRecipeVMService _userRecipeVMService;
        private readonly IUserFavoriteService _userFavoriteService;
        private readonly IUserRecipeService _userRecipeService;
        private readonly IGroceryListService _groceryListService;
        private readonly IGroceryListVMService _groceryListVMService;

        public HomeController(IGroceryListService groceryListService, IRecipeService recipeService, IRecipeVMService recipeVMService, IUserFavoriteService userFavoriteService, IUserRecipeService userRecipeService, IUserRecipeVMService userRecipeVMService, IGroceryListVMService groceryListVMService)
        {
            _groceryListService = groceryListService;
            _recipeVMService = recipeVMService;
            _recipeService = recipeService;
            _userFavoriteService = userFavoriteService;
            _userRecipeService = userRecipeService;
            _userRecipeVMService = userRecipeVMService;
            _groceryListVMService = groceryListVMService;
        }

        public IActionResult Index()
        {
            //Get All Recipes
            var allRecipes = _recipeService.GetRecipes();

            //Convert List of Repo Models to  List of ViewModels
            var recipeResponse = _recipeVMService.RMListToVMList(allRecipes);

            //Get current username check null, return All Recipes without doing work if null.
            var userName = HttpContext.User.Identity.Name;

            if (userName == null)
            {
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
                int test = favorites[i];
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

        public ActionResult<RecipeVM> GetFavoriteRecipes()
        {
            //Get current UserName
            var userName = HttpContext.User.Identity.Name;

            //Get Current User List of Favorites
            var userFavorites = _userFavoriteService.GetFavoriteRecipes(userName);

            List<int> favorites = userFavorites.Select(x => x.recipe_id).ToList();

            var allRecipes = _recipeService.GetRecipes();
            var recipeResponse = _recipeVMService.RMListToVMList(allRecipes);

            var favoriteRecipes = new List<RecipeVM>();

            foreach (RecipeVM recipe in recipeResponse)
            {
                for (int i = 0; i < favorites.Count; i++)
                {
                    int test = favorites[i];
                    if (recipe.RecipeId == favorites[i])
                    {
                        recipe.IsFavorite = true;
                        favoriteRecipes.Add(recipe);
                    }
                }
            }
            if(favoriteRecipes.Count > 0)
            {
                return View("Recipes", favoriteRecipes);
            }
            return View("_EmptyFavorites");
        }

        public void SetFavorite(int recipeId, bool check)
        {
            var userName = HttpContext.User.Identity.Name;
            _userFavoriteService.UpdateFavorite(recipeId, check, userName);
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
            return View("_UserRecipeDetails", recipeResult);
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
                int test = favorites[i];
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
            ViewBag.UserRecipeCreatedSuccess = true;
            return View("_UserRecipes", recipeResponse);
        }

        [HttpPost]
        public ActionResult<UserRecipeVM> DeleteUserRecipe(int id)
        {
            _userRecipeService.DeleteUserRecipe(id);
            return RedirectToAction("GetUserRecipes");
        }
        public IActionResult GetGroceryList()
        {
            var groceryListItemsRM = _groceryListService.GetGroceryListItems(HttpContext.User.Identity.Name).ToList();

            var groceryListItems = _groceryListVMService.RMListToVMList(groceryListItemsRM);

            return View("GroceryList", groceryListItems);
        }

        public IActionResult AddToGroceryList(int id)
        {
            //Get Recipe by id
            var recipe = _recipeService.GetRecipe(id);

            //Check if user already has added this Recipe to their list
            var groceryListItemsRM = _groceryListService.GetGroceryListItems(HttpContext.User.Identity.Name).ToList();

            var groceryListItems = _groceryListVMService.RMListToVMList(groceryListItemsRM);

            foreach (var item in groceryListItems)
            {
                if (item.recipe_id == recipe.Id)
                {
                    ViewBag.ItemAlreadyInGroceryList = true;
                    return View("GroceryList", groceryListItems);
                }
            }

            var groceryListItem = _groceryListVMService.RecipeToGroceryListItem(recipe);

            groceryListItem.userName = HttpContext.User.Identity.Name;

            _groceryListService.InsertGroceryListItem(groceryListItem);

            var updatedGroceryListItemsRM = _groceryListService.GetGroceryListItems(HttpContext.User.Identity.Name).ToList();

            var updatedGroceryListItems = _groceryListVMService.RMListToVMList(updatedGroceryListItemsRM);

            return View("GroceryList", updatedGroceryListItems);

        }

        public IActionResult ClearGroceryList()
        {
            _groceryListService.ClearGroceryList(HttpContext.User.Identity.Name);

            var groceryListItemsRM = _groceryListService.GetGroceryListItems(HttpContext.User.Identity.Name).ToList();

            var groceryListItems = _groceryListVMService.RMListToVMList(groceryListItemsRM);

            return View("GroceryList", groceryListItems);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorVM { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}