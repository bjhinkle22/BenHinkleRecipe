using BenHinkleRecipes.DataAccessLayer.Interfaces.RepoInterfaces;
using BenHinkleRecipes.Interfaces.ServiceInterfaces;
using BenHinkleRecipes.DataAccessLayer.Models.RepoModels;

namespace BenHinkleRecipes.Services
{
    public class GroceryListService : IGroceryListService
    {
        private readonly IGroceryListRepository _groceryListRepo;

        public GroceryListService(IGroceryListRepository groceryListRepo)
        {
            _groceryListRepo = groceryListRepo;
        }

        public IEnumerable<GroceryListRepoModel> GetGroceryListItems(string userName)
        {
            List<GroceryListRepoModel> groceryListItems = _groceryListRepo.GetGroceryListItems(userName).ToList();
            return groceryListItems;
        }

        public void InsertGroceryListItem(List<GroceryListRepoModel> groceryListItems)
        {
            _groceryListRepo.InsertGroceryListItem(groceryListItems);
        }

        public void ClearGroceryList(string userName)
        {
            _groceryListRepo.ClearGroceryList(userName);
        }
        public void UpdateGroceryList(List<GroceryListRepoModel> groceryListItems)
        {
            _groceryListRepo.UpdateGroceryList(groceryListItems);
        }
    }
}
