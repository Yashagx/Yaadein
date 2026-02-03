using System;

namespace Yaadein.Models
{
    public class Reminder
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime ReminderTime { get; set; }
        public bool IsRecurring { get; set; }
        public RecurrenceType Recurrence { get; set; }
        public bool IsActive { get; set; }
        public bool IsCompleted { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Category { get; set; }
        public int Priority { get; set; }

        public Reminder()
        {
            CreatedDate = DateTime.Now;
            IsActive = true;
            IsCompleted = false;
            Priority = 2; // Medium priority
            Recurrence = RecurrenceType.None;
        }
    }

    public enum RecurrenceType
    {
        None = 0,
        Daily = 1,
        Weekly = 2,
        Monthly = 3
    }

    public static class ReminderCategories
    {
        public const string Medication = "Medication";
        public const string Meal = "Meal";
        public const string Exercise = "Exercise";
        public const string Social = "Social";
        public const string Appointment = "Appointment";
        public const string Task = "Task";
        public const string Event = "Event";
        public const string Call = "Call";

        public static string[] GetAll()
        {
            return new string[]
            {
                Medication,
                Meal,
                Exercise,
                Social,
                Appointment,
                Task,
                Event,
                Call
            };
        }
    }
}