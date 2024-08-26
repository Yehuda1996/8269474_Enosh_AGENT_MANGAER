using System.ComponentModel.DataAnnotations;

namespace AgentsMVC.ViewModel
{
    public enum AgentStatus
    {
        Active,
        Inactive
    }
    public class AgentVM
    {
        public int Id { get; set; }
        public  string Nickname { get; set; }
        public  string Image { get; set; }
        public int Coordinate_x { get; set; } = -1;
        public int Coordinate_y { get; set; } = -1;
        public AgentStatus Status { get; set; } = AgentStatus.Inactive;
        public List<MissionVM> Missions { get; set; } = [];
    }
}
