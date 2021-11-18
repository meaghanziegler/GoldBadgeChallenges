using KomodoOutings;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace Testing
{
    [TestClass]
    public class OutingRepositoryTests
    {
        [TestMethod]
        public void AddOuting_ShouldAddOutingToDB()
        {
            Outing outing = new Outing();

            OutingRepository repository = new OutingRepository();

            bool addResult = repository.AddOutingToDB(outing);

            Assert.IsTrue(addResult);
        }

        [TestMethod]
        public void GetDB_ShouldRetrunCorrectCOllection()
        {
            Outing outing = new Outing();
            OutingRepository repo = new OutingRepository();

            repo.AddOutingToDB(outing);

            List<Outing> outings = repo.GetOutings();

            bool dbHasOuting = outings.Contains(outing);

            Assert.IsTrue(dbHasOuting);
        }

        private OutingRepository _repo;
        private Outing _outing1;
        private Outing _outing2;
        private Outing _outing3;
        private Outing _outing4;
        private Outing _outing5;

        [TestInitialize]
        public void Arrange()
        {
            _repo = new OutingRepository();
            _outing1 = new Outing(EventType.Golf, 8, new DateTime(2021, 07, 02), 50.00m);
            _outing2 = new Outing(EventType.Concert, 35, new DateTime(2021, 08, 23), 75.85m);
            _outing3 = new Outing(EventType.Bowling, 26, new DateTime(2021, 02, 11), 32.47m);
            _outing4 = new Outing(EventType.Amusement_Park, 67, new DateTime(2021, 06, 28), 120.42m);
            _outing5 = new Outing(EventType.Golf, 12, new DateTime(2019, 05, 12), 74.23m);

            _repo.AddOutingToDB(_outing1);
            _repo.AddOutingToDB(_outing2);
            _repo.AddOutingToDB(_outing3);
            _repo.AddOutingToDB(_outing4);
            _repo.AddOutingToDB(_outing5);
        }

        [TestMethod]
        public void GetByEvent_ShouldReturnCorrectContent()
        {
            Outing foundOuting = _repo.GetOutingByEventType(EventType.Bowling);

            Assert.AreEqual(_outing3, foundOuting);
        }

        [TestMethod]
        public void GetAllOutingsByEvent_ShouldReturnCorrectContent()
        {
            List<Outing> foundOutings = _repo.GetAllOutingByEventType(EventType.Golf);

            Assert.IsTrue(foundOutings.Contains(_outing1));
            Assert.IsTrue(foundOutings.Contains(_outing5));
        }

        [TestMethod]
        public void UpdateExistingOuting_ShouldReturnTrue()
        {
            Outing newOuting = new Outing(EventType.Bowling, 30, new DateTime(2021, 01, 16), 43.74m);

            bool updateResult = _repo.UpdateExistingOutingByEventType(EventType.Bowling, newOuting);

            Assert.IsTrue(updateResult);
        }

        [TestMethod]
        public void DeleteExistingOuting_ShouldReturnTrue()
        {
            Outing outing = _repo.GetOutingByEventType(EventType.Amusement_Park);

            bool removeResult = _repo.DeleteExistingOuting(outing);

            Assert.IsTrue(removeResult);
        }
    }
}
