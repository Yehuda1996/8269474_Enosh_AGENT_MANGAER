using AgentsRest.Dto;
using AgentsRest.Models;

namespace AgentsRest.Services
{
    public interface IMissionService
    {
        public Task<MissionModel> CreateMissionAsync(MissionDto missionDto);
        public Task<List<MissionModel>> GetAllMissionsAsync();
    }
}
