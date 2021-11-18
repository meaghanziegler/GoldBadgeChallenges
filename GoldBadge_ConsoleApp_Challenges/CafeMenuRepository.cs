using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KomodoCafe
{
    public class CafeMenuRepository
    {
        protected readonly List<CafeMenu> _menu = new List<CafeMenu>();

        public bool AddItemsToMenu(CafeMenu menu)
        {
            int startingCount = _menu.Count;
            _menu.Add(menu);

            bool wasAdded = (_menu.Count > startingCount) ? true : false;
            return wasAdded;
        }

        public List<CafeMenu> GetMenuItems()
        {
            return _menu;
        }

        public CafeMenu GetMenuItemsByName(string name)
        {
            foreach (CafeMenu item in _menu)
            {
                if(item.Name.ToLower() == name.ToLower())
                {
                    return item;
                }
            }
            return null;
        }

        public bool UpdateExistingItemByName(string originalName, CafeMenu item)
        {
            CafeMenu oldItem = GetMenuItemsByName(originalName);

            if(oldItem != null)
            {
                oldItem.Name = item.Name;
                oldItem.NumberOfMeal = item.NumberOfMeal;
                oldItem.Description = item.Description;
                oldItem.Ingredients = item.Ingredients;
                oldItem.Price = item.Price;

                return true;
            }
            else 
                return false;
        }

        public bool DeleteExistingMenuItems(CafeMenu existingItem)
        {
            bool deleteResult = _menu.Remove(existingItem);
            return deleteResult;
        }
    }
}
