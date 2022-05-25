using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TestTaskData.Models
{
    public class Employee : IdentityUser
    {
        [Required]
        public string Name { get; set; }

        public bool Sex { get; set; }

        public DateTime Birthday { get; set; }

        public List<WorkTime> Works { get; set; } = new();
    }
}
