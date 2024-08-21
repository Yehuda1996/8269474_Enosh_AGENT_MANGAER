namespace AgentsRest.Models
{
    public enum Status
    {
        Dead,
        Alive
    }
    public class TargetModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Role { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public Status Status { get; set; }
    }
}
