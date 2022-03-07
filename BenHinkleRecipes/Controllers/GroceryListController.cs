using BenHinkleRecipes.Interfaces.ServiceInterfaces;
using Microsoft.AspNetCore.Mvc;

namespace BenHinkleRecipes.Controllers
{
    public class GroceryListController : Controller
    {
        private readonly IRecipeService _recipeService;
        private readonly IUserRecipeService _userRecipeService;
        private readonly IGroceryListService _groceryListService;
        private readonly IGroceryListVMService _groceryListVMService;

        public GroceryListController(IGroceryListService groceryListService, IRecipeService recipeService, IGroceryListVMService groceryListVMService, IUserRecipeService userRecipeService)
        {
            _groceryListService = groceryListService;
            _recipeService = recipeService;
            _groceryListVMService = groceryListVMService;
            _userRecipeService = userRecipeService;
        }
        public IActionResult Index()
        {
            return View();
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

        public IActionResult UserAddToGroceryList(int id)
        {
            //Get Recipe by id
            var recipe = _userRecipeService.GetRecipe(id);

            //Check if user already has added this Recipe to their list
            var groceryListItemsRM = _groceryListService.GetGroceryListItems(HttpContext.User.Identity.Name).ToList();

            var groceryListItems = _groceryListVMService.RMListToVMList(groceryListItemsRM);

            foreach (var item in groceryListItems)
            {
                if (item.recipe_id == recipe.recipe_id)
                {
                    ViewBag.ItemAlreadyInGroceryList = true;
                    return View("GroceryList", groceryListItems);
                }
            }

            var groceryListItem = _groceryListVMService.UserRecipeToGroceryListItem(recipe);

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
    }
}
