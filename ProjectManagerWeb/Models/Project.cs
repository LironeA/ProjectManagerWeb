using Microsoft.Build.Framework;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectManagerWeb.Models
{
    public class Project
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        
        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public User? User { get; set; }
        public List<Todo>? Todos { get; set; }
    }
}
