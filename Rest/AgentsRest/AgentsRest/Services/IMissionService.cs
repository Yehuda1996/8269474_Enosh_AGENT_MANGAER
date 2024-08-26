using AgentsRest.Dto;
using AgentsRest.Models;

namespace AgentsRest.Services
{
    public interface IMissionService
    {
        public Task<MissionModel> CreateMissionAsync(MissionDto missionDto);
        public Task<List<MissionModel>> GetAllMissionsAsync();
        public Task<MissionModel?> GetMissionById(int id);
        public Task<MissionModel> UpdateMissionStatusAsync(int id, MissionModel missionModel);
        public Task<double> CalcTimeForMission(AgentModel agentModel, TargetModel targetModel);
        public Task<double> CalcDistance(AgentModel agentModel, TargetModel targetModel);
        public Task MoveAgentTowardsTargetAsync(MissionModel missionModel, AgentModel agentModel, TargetModel targetModel, MoveDto moveDto);
        public Task DeleteMissionByIdAsync(int id);
    }
}
