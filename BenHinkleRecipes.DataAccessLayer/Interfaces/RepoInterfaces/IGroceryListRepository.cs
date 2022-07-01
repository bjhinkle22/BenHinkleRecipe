using BenHinkleRecipes.DataAccessLayer.Models.RepoModels;

namespace BenHinkleRecipes.DataAccessLayer.Interfaces.RepoInterfaces
{
    public interface IGroceryListRepository
    {
        IEnumerable<GroceryListRepoModel> GetGroceryListItems(string userName);
        void InsertGroceryListItem(List<GroceryListRepoModel> groceryListItems);
        void UpdateGroceryList(List<GroceryListRepoModel> groceryListItems);
        void ClearGroceryList(string userName);
        void Save();
    }
}
