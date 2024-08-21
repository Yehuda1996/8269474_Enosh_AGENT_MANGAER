namespace AgentsRest.Models
{
    public enum TargetStatus
    {
        Alive,
        Eliminated
    }
    public class TargetModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Position { get; set; }
        public int Coordinate_x { get; set; }
        public int Coordinate_y { get; set; }
        public TargetStatus Status { get; set; }
        public string Image { get; set; }
    }
}
