using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace AgentsRest.Models
{
    public enum AgentStatus
    {
        Active,
        Inactive
    }
    public class AgentModel
    {
        public int Id { get; set; }
        public string Nickname { get; set; }
        public int Coordinate_x { get; set; }
        public int Coordinate_y { get; set; }
        public AgentStatus Status { get; set; }
        public string Image { get; set; }
    }
}
