using System;

namespace Yaadein.Models
{
    /// <summary>
    /// Represents a memory card for storing important information
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
            Title = "";
            Content = "";
            Category = "";
            Icon = "";
            ImagePath = "";
            CreatedDate = DateTime.Now;
        }
    }
}