namespace AgentsMVC.ViewModel
{
    public enum MissionStatus
    {
        Proposed,
        Assigned,
        Completed
    }
    public class MissionVM
    {
        public int Id { get; set; }
        public AgentVM Agent { get; set; }
        public int AgentId { get; set; }
        public TargetVM Target { get; set; }
        public int TargetId { get; set; }
        public double TimeTillCompletion { get; set; }
        public DateTime TimeOfMission { get; set; }
        public MissionStatus Status { get; set; }
    }
}