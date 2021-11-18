using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KomodoClaimsDepartment
{
    public class ProgramUI
    {
        private readonly ClaimRepository _claimRepo = new ClaimRepository();

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
                    "1. See all claims\n" +
                    "2. Take care of next claim\n" +
                    "3. Enter a new claim\n" +
                    "4. Exit");

                string userInput = Console.ReadLine();
                
                switch (userInput)
                {
                    case "1":
                    case "one":
                        SeeAllClaims();
                        break;
                    case "2":
                    case "two":
                        SeeClaimsById();
                        break;
                    case "3":
                    case "three":
                        TakeCareOfNextClaim();
                        break;
                    case "4":
                    case "four":
                        EnterNewClaim();
                        break;
                    case "5":
                    case "five":
                        RemoveClaimFromList();
                        break;
                    case "6":
                    case "six":
                    case "exit":
                        continueToRun = false;
                        break;
                    default:
                        Console.WriteLine("Please enter a valid number between 1 and 4.");
                        AnyKey();
                        break;
                }
            }
        }

        private void EnterNewClaim()
        {
            Console.Clear();

            Claim claim = new Claim();

            Console.WriteLine("Please enter an id:");
            string idInput = Console.ReadLine();
            int claimId = int.Parse(idInput);
            claim.ClaimID = claimId;

            Console.WriteLine("Please enter the type of claim:\n" +
                "1. Car\n" +
                "2. Home\n" +
                "3. Theft\n");
            string typeInput = Console.ReadLine();
            int typeId = int.Parse(typeInput);
            claim.TypeOfClaim = (ClaimType)typeId;

            Console.WriteLine("Please enter a description:");
            claim.Description = Console.ReadLine();

            Console.WriteLine("Please enter the claim amount: $");
            string amountInput = Console.ReadLine();
            double amount = double.Parse(amountInput);
            claim.ClaimAmount = amount;
            
            Console.WriteLine("Please enter the date of incident using YYYY/MM/DD format:");
            string incidentInput = Console.ReadLine();
            DateTime incident = DateTime.Parse(incidentInput);
            claim.DateOfIncident = incident;

            Console.WriteLine("Please enter the date of claim using YYYY/MM/DD format:");
            string claimInput = Console.ReadLine();
            DateTime claimDate = DateTime.Parse(claimInput);
            claim.DateOfClaim = claimDate;

            if (_claimRepo.AddClaimToDirectory(claim))
            {
                Console.WriteLine("Claim added.");
                AnyKey();
            }
            else
            {
                Console.WriteLine("Content wasn't added.");
                AnyKey();
            }
        }

        private void SeeAllClaims()
        {
            Console.Clear();

            List<Claim> listOfClaim = _claimRepo.GetClaims();

            foreach (Claim claim in listOfClaim)
            {
                DisplayClaim(claim);
            }
            AnyKey();
        }

        private void SeeClaimsById()
        {
            Console.Clear();


            Console.WriteLine("Enter a claim ID:");
            string idInput = Console.ReadLine();
            int claimID = int.Parse(idInput);
            Claim claim = _claimRepo.GetClaimById(claimID);
            claim.ClaimID = claimID;

            if (claim != null)
            {
                DisplayClaim(claim);
            }
            else
                Console.WriteLine("Invalid ID, claim not found.");
            AnyKey();
        }

        private void TakeCareOfNextClaim()
        {
            Console.Clear();

            Claim claim = _claimRepo.NextClaim();
            Console.WriteLine("Here are the details for the next claim to be handled:\n" +
                $"Claim ID: {claim.ClaimID}\n" +
                $"Type Of Claim: {claim.TypeOfClaim}\n" +
                $"Claim Description: {claim.Description}\n" +
                $"Claim Amout: ${claim.ClaimAmount}\n" +
                $"Date Of Incident: {claim.DateOfIncident}\n" +
                $"Date Of Cliam: {claim.DateOfClaim}\n" +
                $"Is Claim Valid: {claim.IsValid}\n");

            Console.WriteLine("Do you want to deal with this claim now (y/n)?");
            string handleClaim = Console.ReadLine();

            if (handleClaim == "y")
            {
                _claimRepo.DeleteExistingClaim(claim);
                Console.WriteLine("This claim has been finished!");
            }
            AnyKey();

        }

        private void RemoveClaimFromList()
        {
            Console.Clear();

            Console.WriteLine("Which claim would you like to remove?\n");

            List<Claim> currentClaim = _claimRepo.GetClaims();

            int count = 0;
            foreach (Claim claim in currentClaim)
            {
                count++;
                Console.WriteLine($"{count}. {claim.ClaimID}");
            }

            int targetClaimID = int.Parse(Console.ReadLine());
            int targetIndex = targetClaimID - 1;

            if (targetIndex >= 0 && targetIndex < currentClaim.Count)
            {
                Claim desiredClaim = currentClaim[targetIndex];

                if (_claimRepo.DeleteExistingClaim(desiredClaim))
                {
                    Console.WriteLine($"{desiredClaim.ClaimID} was deleted");
                    AnyKey();
                }
                else
                {
                    Console.WriteLine("Deletion falied");
                    AnyKey();
                }
            }
            else
                Console.WriteLine("No claim with that ID");
        }

        private void DisplayClaim(Claim claim)
        {
            Console.WriteLine($"Claim ID: {claim.ClaimID}\n" +
                $"Claim Type: {claim.TypeOfClaim}\n" +
                $"Description: {claim.Description}\n" +
                $"Claim Amount: ${claim.ClaimAmount}\n" +
                $"Date Of Incident: {claim.DateOfIncident}\n" +
                $"Date Of Claim: {claim.DateOfClaim}\n" +
                $"Is Valid Claim: {claim.IsValid}\n");
        }

        private void AnyKey()
        {
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }

        private void SeedContent()
        {
            Claim claim1 = new Claim(1, ClaimType.Car, "Car accident on 465.", 400.00, new DateTime(2021, 04, 25), new DateTime(2021, 04, 27));
            Claim claim2 = new Claim(2, ClaimType.Home, "House fire in kitchen.", 4000.00, new DateTime(2021, 04, 11), new DateTime(2021, 04, 12));
            Claim claim3 = new Claim(3, ClaimType.Theft, "Stolen pancakes.", 4.00, new DateTime(2021, 04, 27), new DateTime(2021, 06, 01));

            _claimRepo.AddClaimToDirectory(claim1);
            _claimRepo.AddClaimToDirectory(claim2);
            _claimRepo.AddClaimToDirectory(claim3);
        }
    }
}
