using Microsoft.Identity.Client;

namespace AgentsRest.Models
{
    public enum MissionStatus
    {
        Proposed,
        Assigned,
        Completed
    }
    public class MissionModel
    {
        public int Id { get; set; }
        public AgentModel Agent { get; set; }
        public int AgentId { get; set; }
        public TargetModel Target { get; set; }
        public int TargetId { get; set; }
        public double TimeTillCompletion { get; set; }
        public DateTime TimeOfMission { get; set; }
        public MissionStatus Status { get; set; } 
    }
}
