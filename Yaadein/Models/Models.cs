using System;
using System.Collections.Generic;

namespace Yaadein.Models
{
    /// <summary>
    /// Represents a person in the memory aid application
    /// </summary>
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
        public DateTime? LastContactDate { get; set; }
        public bool IsFavorite { get; set; }
        public string EmergencyContact { get; set; }

        public Person()
        {
            CreatedDate = DateTime.Now;
            Name = "";
            Relationship = "";
            PhoneNumber = "";
            Email = "";
            Address = "";
            Notes = "";
            PhotoPath = "";
            FavoriteMemory = "";
            ImportantDetails = "";
            EmergencyContact = "";
        }
    }

    /// <summary>
    /// Represents a reminder/notification
    /// </summary>
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
        public int Priority { get; set; } // Changed to int for consistency

        public Reminder()
        {
            CreatedDate = DateTime.Now;
            ReminderTime = DateTime.Now;
            IsActive = true;
            IsCompleted = false;
            IsRecurring = false;
            Recurrence = RecurrenceType.None;
            Priority = 2; // Medium priority
            Title = "";
            Description = "";
            Category = "";
        }
    }

    /// <summary>
    /// Represents a daily routine
    /// </summary>
    public class Routine
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public TimeSpan StartTime { get; set; }
        public bool IsActive { get; set; }
        public string Category { get; set; }
        public DateTime CreatedDate { get; set; }
        public string IconName { get; set; }
        public List<RoutineStep> Steps { get; set; }

        public Routine()
        {
            CreatedDate = DateTime.Now;
            IsActive = true;
            Steps = new List<RoutineStep>();
            Name = "";
            Description = "";
            Category = "";
            IconName = "";
            StartTime = DateTime.Now.TimeOfDay;
        }
    }

    /// <summary>
    /// Represents a step in a routine
    /// </summary>
    public class RoutineStep
    {
        public int Id { get; set; }
        public int RoutineId { get; set; }
        public int StepNumber { get; set; }
        public string Instruction { get; set; }
        public int DurationMinutes { get; set; }
        public bool IsCompleted { get; set; }
        public string ImagePath { get; set; }

        public RoutineStep()
        {
            DurationMinutes = 5;
            IsCompleted = false;
            Instruction = "";
            ImagePath = "";
        }
    }

    /// <summary>
    /// Represents a memory card for important information
    /// </summary>
    public class MemoryCard
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string Category { get; set; }
        public string Icon { get; set; }
        public string ImagePath { get; set; }
        public DateTime CreatedDate { get; set; }

        public MemoryCard()
        {
            CreatedDate = DateTime.Now;
            Title = "";
            Content = "";
            Category = "";
            Icon = "";
            ImagePath = "";
        }
    }

    /// <summary>
    /// Recurrence types for reminders
    /// </summary>
    public enum RecurrenceType
    {
        None = 0,
        Daily = 1,
        Weekly = 2,
        Monthly = 3,
        Yearly = 4
    }

    /// <summary>
    /// Priority levels for reminders (kept for reference but using int in Reminder class)
    /// </summary>
    public enum PriorityLevel
    {
        Low = 1,
        Medium = 2,
        High = 3
    }
}