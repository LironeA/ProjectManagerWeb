namespace ProjectManagerWeb.Models
{
    public class Todo
    {
        public int Id { get; set; }
        public int ProjectId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsComplete { get; set; }

        public int? Order { get; set; } = null;

    }
}
