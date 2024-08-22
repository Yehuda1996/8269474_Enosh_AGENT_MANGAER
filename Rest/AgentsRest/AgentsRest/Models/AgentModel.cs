using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;

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
        [Required]
        [StringLength(20)]
        public required string Nickname { get; set; }
        [Required]
        public required string Image { get; set; }
        public int Coordinate_x { get; set; } = -1;
        public int Coordinate_y { get; set; } = -1;
        public AgentStatus Status { get; set; } = AgentStatus.Inactive;
        public List<MissionModel> Missions { get; set; } = [];
    }
}
