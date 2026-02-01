using System;
using System.Collections.Generic;

namespace Yaadein.Models
{
    public class Routine
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public TimeSpan StartTime { get; set; }
        public string Category { get; set; }
        public bool IsActive { get; set; }
        public List<RoutineStep> Steps { get; set; }

        public Routine()
        {
            Steps = new List<RoutineStep>();
        }
    }

    public class RoutineStep
    {
        public int StepNumber { get; set; }
        public string Instruction { get; set; }
        public int DurationMinutes { get; set; }
    }

    public static class RoutineCategories
    {
        public const string Morning = "Morning";
        public const string Afternoon = "Afternoon";
        public const string Evening = "Evening";
        public const string Night = "Night";
        public const string Medication = "Medication";
        public const string Exercise = "Exercise";
        public const string Meal = "Meal";
        public const string Personal = "Personal Care";
        public const string Social = "Social";

        public static string[] GetAll()
        {
            return new string[]
            {
                Morning,
                Afternoon,
                Evening,
                Night,
                Medication,
                Exercise,
                Meal,
                Personal,
                Social
            };
        }
    }
}