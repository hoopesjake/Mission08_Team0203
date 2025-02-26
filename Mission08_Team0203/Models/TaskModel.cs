using System;
using System.ComponentModel.DataAnnotations;

namespace Mission08_Team0203.Models
{
    public class TaskModel
    {
        [Key]
        public int TaskId { get; set; }

        [Required]
        public string TaskName { get; set; }

        public DateTime? DueDate { get; set; }

        [Required]
        public int Quadrant { get; set; }

        // Foreign Key
        [Required]
        public int CategoryId { get; set; }
        public Category Category { get; set; }

        public bool Completed { get; set; } = false;
    }
}