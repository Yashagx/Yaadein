using System;

namespace Yaadein.Models
{
    public class Person
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Relationship { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Notes { get; set; }
        public string PhotoPath { get; set; }
        public DateTime? Birthday { get; set; }
        public string FavoriteMemory { get; set; }
        public string ImportantDetails { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime LastContactDate { get; set; }
        public bool IsFavorite { get; set; }
        public string EmergencyContact { get; set; }

        public Person()
        {
            CreatedDate = DateTime.Now;
            LastContactDate = DateTime.Now;
            IsFavorite = false;
            EmergencyContact = "No";
        }
    }
}