namespace WebApp.API.Models
{
    public class Group
    {
        public Group(string name)
        {
            Name = name;
        }
     
        public string Name { get; set; }

        public ICollection<Connection> Connections { get; set; } = new List<Connection>();
    }
}
