using KomodoClaimsDepartment;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace Testing
{
    [TestClass]
    public class ClaimRepositoryTests
    {
        [TestMethod]
        public void AddClaim_ShouldAddClaimToDirectory()
        {
            Claim claim = new Claim();

            ClaimRepository repository = new ClaimRepository();

            bool addResult = repository.AddClaimToDirectory(claim);

            Assert.IsTrue(addResult);
        }

        [TestMethod]
        public void GetDirectory_ShouldReturnCorrectCollection()
        {
            Claim claim = new Claim();
            ClaimRepository repo = new ClaimRepository();

            repo.AddClaimToDirectory(claim);

            List<Claim> claims = repo.GetClaims();

            bool directoryHasContent = claims.Contains(claim);

            Assert.IsTrue(directoryHasContent);
        }

        private ClaimRepository _repo;
        private Claim _claim1;
        private Claim _claim2;
        private Claim _claim3;

        [TestInitialize]
        public void Arrange()
        {
            _repo = new ClaimRepository();
            _claim1 = new Claim(1, ClaimType.Car, "Car accident on 465.", 400.00, new DateTime(2021, 04, 25), new DateTime(2021, 04, 27));
            _claim2 = new Claim(2, ClaimType.Home, "House fire in kitchen.", 4000.00, new DateTime(2021, 04, 11), new DateTime(2021, 04, 12));
            _claim3 = new Claim(3, ClaimType.Theft, "Stolen pancakes.", 4.00, new DateTime(2021, 04, 27), new DateTime(2021, 06, 01));

            _repo.AddClaimToDirectory(_claim1);
            _repo.AddClaimToDirectory(_claim2);
            _repo.AddClaimToDirectory(_claim3);
        }

        [TestMethod]
        public void GetByID_ShouldReturnCorrectClaim()
        {
            Claim foundClaim = _repo.GetClaimById(2);

            Assert.AreEqual(_claim2, foundClaim);
        }

        [TestMethod]
        public void UpdateExistingClaim_ShouldRetrunTrue()
        {
            Claim newClaim = new Claim(3, ClaimType.Theft, "Stolen pancakes.", 6.00, new DateTime(2021, 04, 27), new DateTime(2021, 06, 01));

            bool updateResult = _repo.UpdateExistingClaimById(3, newClaim);

            Assert.IsTrue(updateResult);
        }

        [TestMethod]
        public void DeleteExistingClaim_ShouldReturnTrue()
        {
            Claim claim = _repo.GetClaimById(3);

            bool removeResult = _repo.DeleteExistingClaim(claim);

            Assert.IsTrue(removeResult);
        }
    }
}
