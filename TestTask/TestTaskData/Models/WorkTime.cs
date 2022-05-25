using System;
using System.ComponentModel.DataAnnotations;

namespace TestTaskData.Models
{
    public class WorkTime
    {
        [Key]
        public string Id { get; set; }

        [Required]
        public string UserId { get; set; }

        [Required]
        public string RoleId { get; set; }

        [Required]
        public string ProjectId { get; set; }

        [Required]
        public string ActivityTypeId { get; set; }

        [Required]
        public DateTime Day { get; set; }

        [Required]
        public TimeSpan Time { get; set; }

        public Employee Employee { get; set; }

        public Project Project { get; set; }

        public Role Role { get; set; }

        public ActivityType ActivityType { get; set; }
    }
}
