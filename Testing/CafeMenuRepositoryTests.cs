using KomodoCafe;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace Testing
{
    [TestClass]
    public class CafeMenuRepositoryTests
    {
        [TestMethod]
        public void AddItem_ShouldAddItemToMenu()
        {
            CafeMenu item = new CafeMenu();

            CafeMenuRepository repository = new CafeMenuRepository();

            bool addResult = repository.AddItemsToMenu(item);

            Assert.IsTrue(addResult);
        }

        [TestMethod]
        public void GetMenu_ShouldReturnMenu()
        {
            CafeMenu item = new CafeMenu();
            CafeMenuRepository repo = new CafeMenuRepository();

            repo.AddItemsToMenu(item);

            List<CafeMenu> items = repo.GetMenuItems();

            bool menuHasItems = items.Contains(item);

            Assert.IsTrue(menuHasItems);
        }

        private CafeMenuRepository _repo;
        private CafeMenu _Item1;
        private CafeMenu _Item2;
        private CafeMenu _Item3;
        private CafeMenu _Item4;

        [TestInitialize]
        public void Arrange()
        {
            _repo = new CafeMenuRepository();
            _Item1 = new CafeMenu("Sandwich", MealNumber.Number1, "It's a sandwich", "I mean...it's not gluten free if that's what your asking..", 7.75);
            _Item2 = new CafeMenu("Soup", MealNumber.Number2, "Good soup", "Hot Broth with floaty bits", 5.39);
            _Item3 = new CafeMenu("Salad", MealNumber.Number3, "Chopped vegetables", "All the vegetables", 6.23);
            _Item4 = new CafeMenu("Fruit Salad", MealNumber.Number4, "It's just like the salad, but with fruit..", "Seasonal fruit", 6.95);

            _repo.AddItemsToMenu(_Item1);
            _repo.AddItemsToMenu(_Item2);
            _repo.AddItemsToMenu(_Item3);
            _repo.AddItemsToMenu(_Item4);
        }

        [TestMethod]
        public void GetByItem_ShouldRetunCorrectItem()
        {
            CafeMenu foundItem = _repo.GetMenuItemsByName("Sandwich");

            Assert.AreEqual(_Item1, foundItem);
        }

        [TestMethod]
        public void UpdateExistingItem_ShouldReturnTrue()
        {
            CafeMenu newItem = new CafeMenu("Fruit Salad", MealNumber.Number4, "The better kind of salad", "All the fruits", 9.45);

            bool updateResult = _repo.UpdateExistingItemByName("Fruit Salad", newItem);

            Assert.IsTrue(updateResult);
        }

        [TestMethod]
        public void DeleteExistingItem_ShouldReturnTrue()
        {
            CafeMenu item = _repo.GetMenuItemsByName("Sandwich");

            bool removeResult = _repo.DeleteExistingMenuItems(item);

            Assert.IsTrue(removeResult);
        }
    }
}
