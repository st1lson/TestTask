using System;
using System.ComponentModel.DataAnnotations;

namespace TestTaskData.Models
{
    public class Project
    {
        [Key]
        public string Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public DateTime DateStart { get; set; }

        public DateTime DateEnd { get; set; }
    }
}
