using System.Text.RegularExpressions;

namespace WebApp.API.Models
{
    public class Connection
    {
        public Connection(string connectionId, string username)
        {
            ConnectionId = connectionId;
            Username = username;
        }
        public string ConnectionId { get; set; }
        public string Username { get; set; }
        public string? GroupName { get; set; }

        public Group? Group { get; set; }
    }
}