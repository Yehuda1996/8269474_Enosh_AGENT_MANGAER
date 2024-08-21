using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace AgentsRest.Models
{
    public enum Status
    {
        Active,
        Inactive
    }
    public class AgentModel
    {
        public int Id { get; set; }
        public string Nickname { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public Status Status { get; set; }
        public string Image { get; set; }
    }
}
