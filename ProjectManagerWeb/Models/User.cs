namespace ProjectManagerWeb.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public virtual List<Project>? Projects { get; set; }

    }
}
