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
        public string Category { get; set; }
        public string Priority { get; set; }
        public bool IsActive { get; set; }
        public bool IsCompleted { get; set; }
    }

    public enum RecurrenceType
    {
        None,
        Daily,
        Weekly,
        Monthly
    }

    public static class ReminderPriorities
    {
        public const string High = "1";
        public const string Medium = "2";
        public const string Low = "3";

        public static string[] GetAll()
        {
            return new string[] { High, Medium, Low };
        }
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