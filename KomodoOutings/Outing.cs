using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KomodoOutings
{
    public enum EventType
    {
        Golf = 1,
        Bowling,
        Amusement_Park,
        Concert
    }
    public class Outing
    {
        public Outing() { }

        public Outing(EventType eventType, int people, DateTime dateOfOuting, decimal costPerPerson)
        {
            TypeOfEvent = eventType;
            NumberOfPeople = people;
            DateOfOuting = dateOfOuting;
            CostPerPerson = costPerPerson;
        }
        
        
        public EventType TypeOfEvent { get; set; }
        public int NumberOfPeople { get; set; }
        public DateTime DateOfOuting { get; set; }
        public decimal CostPerPerson { get; set; }
        public decimal TotalEventCost
        {
            get
            {
                return NumberOfPeople * CostPerPerson;
            }

        } 
        public int YearsOfOuting
        {
            get
            {
                TimeSpan outingsSpan = DateTime.Now - DateOfOuting;
                double yearOutingSpan = outingsSpan.TotalDays / 365.25;
                int yearsOfOuting = Convert.ToInt32(Math.Floor(yearOutingSpan));
                return yearsOfOuting;
            }
        }
        
    }
}
