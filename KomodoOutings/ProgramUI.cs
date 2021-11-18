using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KomodoOutings
{
    public class ProgramUI
    {
        private readonly OutingRepository _outingRepo = new OutingRepository();

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
                    "1. Show all Outings\n" +
                    "2. Find Outings by Event Type\n" +
                    "3. Add new Outing\n" +
                    "4. Remove Outing\n" +
                    "5. Show total cost from all Outings\n" +
                    "6. Exit");

                string userInput = Console.ReadLine();

                switch (userInput)
                {
                    case "1":
                    case "one":
                        ShowAllOutings();
                        break;
                    case "2":
                    case "two":
                        ShowOutingsByEventType();
                        break;
                    case "3":
                    case "three":
                        AddNewOuting();
                        break;
                    case "4":
                    case "four":
                        RemoveOutingFromDB();
                        break;
                    case "5":
                    case "five":
                        TotalCostForOutings();
                        break;
                    case "6":
                    case "six":
                        continueToRun = false;
                        break;
                    default:
                        Console.WriteLine("Please enter a valid number between 1 and 7.");
                        AnyKey();
                        break;
                }
            }
        }

        private void AddNewOuting()
        {
            Console.Clear();

            Outing outing = new Outing();

            Console.WriteLine("Please select the number of an event type: \n" +
                "1. Golf\n" +
                "2. Bowling\n" +
                "3. Amusement Park\n" +
                "4. Concert\n");
            string eventInput = Console.ReadLine();
            int eventId = int.Parse(eventInput);
            outing.TypeOfEvent = (EventType)eventId;

            Console.WriteLine("Please enter the number of people that attended:");
            string peopleInput = Console.ReadLine();
            int people = int.Parse(peopleInput);
            outing.NumberOfPeople = people;

            Console.WriteLine("Please enter the date of the outing in the format of YYYY/MM/DD:");
            string dateInput = Console.ReadLine();
            DateTime date = DateTime.Parse(dateInput);
            outing.DateOfOuting = date;

            Console.WriteLine("Please enter the total cost per person: $");
            string costInput = Console.ReadLine();
            decimal cost = decimal.Parse(costInput);
            outing.CostPerPerson = cost;

            if (_outingRepo.AddOutingToDB(outing))
            {
                Console.WriteLine("Outing added!");
                AnyKey();
            }
            else
            {
                Console.WriteLine("Content wasn't added.");
                AnyKey();
            }
        }

        private void ShowAllOutings()
        {
            Console.Clear();

            List<Outing> listOfOutings = _outingRepo.GetOutings();

            foreach (Outing outing in listOfOutings)
            {
                DisplayOuting(outing);
            }
            AnyKey();
        }

        private void ShowOutingsByEventType()
        {
            Console.Clear();

            Console.WriteLine("Please select an event type: \n" +
                "1. Golf\n" +
                "2. Bowling\n" +
                "3. Amusement Park\n" +
                "4. Concert\n");
            string eventInput = Console.ReadLine();
            EventType eventType = (EventType)Enum.Parse(typeof(EventType), eventInput);
            List<Outing> outings = _outingRepo.GetAllOutingByEventType(eventType);
            foreach (Outing outing in outings)
            {
                DisplayOuting(outing);
            }
            AnyKey();
        }

        private void RemoveOutingFromDB()
        {
            Console.Clear();

            Console.WriteLine("Which outing would you like to remove?\n");

            List<Outing> currentOuting = _outingRepo.GetOutings();

            int count = 0;
            foreach (Outing outing in currentOuting)
            {
                count++;
                Console.WriteLine($"{count}. {outing.TypeOfEvent} on {outing.DateOfOuting}");
            }

            int targetOutingDate = int.Parse(Console.ReadLine());
            int targetIndex = targetOutingDate - 1;

            if (targetIndex >= 0 && targetIndex < currentOuting.Count)
            {
                Outing desiredOuting = currentOuting[targetIndex];

                if (_outingRepo.DeleteExistingOuting(desiredOuting))
                {
                    Console.WriteLine($"{desiredOuting.TypeOfEvent} on {desiredOuting.DateOfOuting} was deleted");
                    AnyKey();
                }
                else
                {
                    Console.WriteLine("Deletion falied");
                    AnyKey();
                }
            }
            else
                Console.WriteLine("No outing with that date");
        }

        private readonly Dictionary<string, decimal> cost = new Dictionary<string, decimal>();

        private void TotalCostForOutings()
        {
            Console.Clear();

            List<Outing> outing = _outingRepo.GetAllOutingsFromThisYear();

            decimal yearOutings;
            cost.Add("Golf", 0.00m);
            cost.Add("Bowling", 0.00m);
            cost.Add("Amusement_Park", 0.00m);
            cost.Add("Concert", 0.00m);


            foreach (Outing outingEvent in outing)
            {
                if (outingEvent.YearsOfOuting <= 1)
                {
                    if (outingEvent.TypeOfEvent == EventType.Golf)
                    {
                        cost["Golf"] += outingEvent.TotalEventCost;
                        Console.WriteLine($"The total cost for all Golf outings this year is: ${cost["Golf"]}");
                    }
                    if (outingEvent.TypeOfEvent == EventType.Bowling)
                    {
                        cost["Bowling"] += outingEvent.TotalEventCost;
                        Console.WriteLine($"The total cost for all Bowling outings this year is: ${cost["Bowling"]}");
                    }
                    if (outingEvent.TypeOfEvent == EventType.Amusement_Park)
                    {
                        cost["Amusement_Park"] += outingEvent.TotalEventCost;
                        Console.WriteLine($"The total cost for all Amusement Park outings this year is: ${cost["Amusement_Park"]}");
                    }
                    if (outingEvent.TypeOfEvent == EventType.Concert)
                    {
                        cost["Concert"] += outingEvent.TotalEventCost;
                        Console.WriteLine($"The total cost for all Concert outings this year is: ${cost["Concert"]}");
                    }
                }
            }
            yearOutings = cost["Golf"] + cost["Bowling"] + cost["Amusement_Park"] + cost["Concert"];
            Console.WriteLine($"The total cost for all outings this year is: ${yearOutings}.");
            AnyKey();
        }

        //Helper Methods
        private void DisplayOuting(Outing outing)
        {
            Console.WriteLine($"Event: {outing.TypeOfEvent}\n" +
                $"Number of people attended: {outing.NumberOfPeople}\n" +
                $"Date of event: {outing.DateOfOuting}\n" +
                $"Total cost per person: ${outing.CostPerPerson}\n" +
                $"Total cost for event: ${outing.TotalEventCost}\n");
        }

        private void AnyKey()
        {
            Console.WriteLine("Press any key to continue....");
            Console.ReadKey();
        }

        private void SeedContent()
        {
            Outing _outing1 = new Outing(EventType.Golf, 8, new DateTime(2021, 07, 02), 50.00m);
            Outing _outing2 = new Outing(EventType.Concert, 35, new DateTime(2021, 08, 23), 75.85m);
            Outing _outing3 = new Outing(EventType.Bowling, 26, new DateTime(2021, 02, 11), 32.47m);
            Outing _outing4 = new Outing(EventType.Amusement_Park, 67, new DateTime(2021, 06, 28), 120.42m);
            Outing _outing5 = new Outing(EventType.Golf, 12, new DateTime(2019, 05, 12), 74.23m);

            _outingRepo.AddOutingToDB(_outing1);
            _outingRepo.AddOutingToDB(_outing2);
            _outingRepo.AddOutingToDB(_outing3);
            _outingRepo.AddOutingToDB(_outing4);
            _outingRepo.AddOutingToDB(_outing5);
        }
    }
}
