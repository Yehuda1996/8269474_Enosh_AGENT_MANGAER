using AgentsRest.Dto;
using AgentsRest.Models;

namespace AgentsRest.Services
{
    public interface IMissionService
    {
        public Task<MissionModel> CreateMissionAsync(MissionDto missionDto, AgentModel agentModel, TargetModel targetModel);
        public Task<List<MissionModel>> GetAllMissionsAsync();
        public Task<double> CalcDistance(AgentModel agentModel, TargetModel targetModel);
        public Task MoveAgentTowardsTarget(MissionModel missionModel, AgentModel agentModel, TargetModel targetModel);
    }
}
