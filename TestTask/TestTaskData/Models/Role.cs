using System.ComponentModel.DataAnnotations;

namespace TestTaskData.Models
{
    public class Role
    {
        [Key]
        public string Id { get; set; }

        [Required]
        public string Name { get; set; }
    }
}
