using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KomodoCafe.UI
{
    public class ProgramUI
    {
        private readonly CafeMenuRepository _cafeRepo = new CafeMenuRepository();

        public void Run()
        {
            SeedContent();

            RunMenu();
        }

        private void RunMenu()
        {
            bool continueToRun = true;
            while (continueToRun)
            {
                Console.Clear();
                Console.WriteLine(
                    "Enter the number of the option you'd like to select: \n" +
                    "1. Show all menu items\n" +
                    "2. Find menu item by name\n" +
                    "3. Add new menu item\n" +
                    "4. Remove menu item\n" +
                    "5. Exit");

                string userInput = Console.ReadLine();

                switch (userInput)
                {
                    case "1":
                    case "one":
                        ShowAllItems();
                        break;
                    case "2":
                    case "two":
                        ShowItemByName();
                        break;
                    case "3":
                    case "three":
                        CreateNewItem();
                        break;
                    case "4":
                    case "four":
                        RemoveItemFromMenu();
                        break;
                    case "5":
                    case "five":
                    case "exit":
                        continueToRun = false;
                        break;
                    default:
                        Console.WriteLine("Please enter a vaild number between 1 and 5.\n");
                        AnyKey();
                        break;
                }
            }
        }

        private void CreateNewItem()
        {
            Console.Clear();

            CafeMenu item = new CafeMenu();

            Console.WriteLine("Please enter a name:");
            item.Name = Console.ReadLine();

            Console.WriteLine("Please enter a menu number:\n" +
                "1. Number1\n" +
                "2. Number2\n" +
                "3. Number3\n" +
                "4. Number4\n" +
                "5. Number5\n" +
                "6. Number6\n" +
                "7. Number7\n");

            string numberInput = Console.ReadLine();

            int mealNumber = int.Parse(numberInput);

            item.NumberOfMeal = (MealNumber)mealNumber;

            Console.WriteLine("Please enter a description:");
            item.Description = Console.ReadLine();

            Console.WriteLine("Please enter in the ingredients:");
            item.Ingredients = Console.ReadLine();

            Console.WriteLine("Please enter a price: $");
            item.Price = Convert.ToDouble(Console.ReadLine());

            if(_cafeRepo.AddItemsToMenu(item))
            {
                Console.WriteLine("Item added!");
                AnyKey();
            }
            else
            {
                Console.WriteLine("Content wasn't added!");
                AnyKey();
            }
        }

        private void ShowAllItems()
        {
            Console.Clear();

            List<CafeMenu> listOfItems = _cafeRepo.GetMenuItems();

            foreach(CafeMenu item in listOfItems)
            {
                DisplayItem(item);
            }
            AnyKey();
        }

        private void ShowItemByName()
        {
            Console.Clear();
            Console.Write("Enter a menu item name:");
            string name = Console.ReadLine();

            CafeMenu item = _cafeRepo.GetMenuItemsByName(name);

            if (item != null)
            {
                DisplayItem(item);
            }
            else
                Console.WriteLine("Invalid name, item not found.");

            AnyKey();
        }

        private void RemoveItemFromMenu()
        {
            Console.Clear();

            Console.WriteLine("Which item would you like to remove?\n");

            List<CafeMenu> currentItem = _cafeRepo.GetMenuItems();

            int count = 0;
            foreach (CafeMenu item in currentItem)
            {
                count++;
                Console.WriteLine($"{count}. {item.Name}");
            }

            int targetItemName = int.Parse(Console.ReadLine());
            int targetNumber = targetItemName - 1;

            if (targetNumber >= 0 && targetNumber < currentItem.Count)
            {
                CafeMenu desiredItem = currentItem[targetNumber];

                if (_cafeRepo.DeleteExistingMenuItems(desiredItem))
                {
                    Console.WriteLine($"{desiredItem.Name} was deleted");
                }
                else
                {
                    Console.WriteLine("Deletion failed");
                    AnyKey();
                }
            }
            else
                Console.WriteLine("No item with that name");
        }

        private void DisplayItem(CafeMenu item)
        {
            Console.WriteLine($"Name: {item.Name}\n" +
                $"Meal Number: {item.NumberOfMeal}\n" +
                $"Description: {item.Description}\n" +
                $"Ingredients: {item.Ingredients}\n" +
                $"Price: ${item.Price}\n");
        }

        private void AnyKey()
        {
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }

        private void SeedContent()
        {
            CafeMenu sandwich = new CafeMenu("Sandwich", MealNumber.Number1, "It's a sandwich", "There are ingredients.", 7.49);
            CafeMenu soup = new CafeMenu("Soup", MealNumber.Number2, "Good soup", "Hot broth with floaty bits", 4.27);
            CafeMenu fruitSalad = new CafeMenu("Fruit Salad", MealNumber.Number3, "It's like a salad but with fruit", "fruit", 8.54);

            _cafeRepo.AddItemsToMenu(sandwich);
            _cafeRepo.AddItemsToMenu(soup);
            _cafeRepo.AddItemsToMenu(fruitSalad);

        }
    }
}
