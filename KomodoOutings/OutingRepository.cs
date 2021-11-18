using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KomodoOutings
{
    public class OutingRepository
    {
        protected readonly List<Outing> _outingDB = new List<Outing>();

        public bool AddOutingToDB(Outing outing)
        {
            int startingCount = _outingDB.Count;
            _outingDB.Add(outing);

            bool wasAdded = (_outingDB.Count > startingCount) ? true : false;
            return wasAdded;
        }

        public List<Outing> GetOutings()
        {
            return _outingDB;
        }

        public Outing GetOutingByEventType(EventType eventType)
        {
            foreach (Outing outing in _outingDB)
            {
                if (outing.TypeOfEvent == eventType)
                {
                    return outing;
                }
            }
            return null;
        }

        public List<Outing> GetAllOutingByEventType(EventType eventType)
        {
            List<Outing> outings = new List<Outing>();
            foreach (Outing item in _outingDB)
            {
                if (item.TypeOfEvent == eventType)
                {
                    outings.Add(item);
                }
            }
            return outings;
        }

        public bool UpdateExistingOutingByEventType(EventType originalEventType, Outing outing)
        {
            Outing oldOuting = GetOutingByEventType(originalEventType);

            if (oldOuting != null)
            {
                oldOuting.TypeOfEvent = outing.TypeOfEvent;
                oldOuting.NumberOfPeople = outing.NumberOfPeople;
                oldOuting.DateOfOuting = outing.DateOfOuting;
                oldOuting.CostPerPerson = outing.CostPerPerson;

                return true;
            }
            else
                return false;
        }

        public List<Outing> GetAllOutingsFromThisYear()
        {
            List<Outing> outings = new List<Outing>();

            foreach (Outing outing in _outingDB)
            {
                if (outing.YearsOfOuting <= 1)
                {
                    outings.Add(outing);
                }
            }
            return outings;
        }

        public bool DeleteExistingOuting(Outing existingOuting)
        {
            bool deleteResult = _outingDB.Remove(existingOuting);
            return deleteResult;
        }
    }
}
