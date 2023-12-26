using Microsoft.Build.Framework;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectManagerWeb.Models
{
    public class Todo
    {
        public int Id { get; set; }

        [Required]
        public int ProjectId { get; set; }

        [ForeignKey("ProjectId")]
        public Project? Project { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsComplete { get; set; }
        public int? Order { get; set; } = null;

    }
}
