using BenHinkleRecipes.DataAccessLayer.Models.RepoModels;

namespace BenHinkleRecipes.Interfaces.ServiceInterfaces
{
    public interface IGroceryListService
    {
        IEnumerable<GroceryListRepoModel> GetGroceryListItems(string userName);
        void InsertGroceryListItem(List<GroceryListRepoModel> groceryListItems);

        void UpdateGroceryList(List<GroceryListRepoModel> groceryListItems);
        void ClearGroceryList(string userName);
    }
}
