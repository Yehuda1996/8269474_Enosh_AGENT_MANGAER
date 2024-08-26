using System.ComponentModel.DataAnnotations;

namespace AgentsMVC.ViewModel
{
    public enum TargetStatus
    {
        Alive,
        Eliminated
    }
    public class TargetVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Position { get; set; }
        public string Image { get; set; }
        public int Coordinate_x { get; set; } = -1;
        public int Coordinate_y { get; set; } = -1;
        public TargetStatus Status { get; set; } = TargetStatus.Alive;
    }
}
