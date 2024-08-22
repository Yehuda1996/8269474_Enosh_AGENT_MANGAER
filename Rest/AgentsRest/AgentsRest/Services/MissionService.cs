using AgentsRest.Data;
using AgentsRest.Dto;
using AgentsRest.Models;
using Microsoft.EntityFrameworkCore;

namespace AgentsRest.Services
{
    public class MissionService(ApplicationDbContext context) : IMissionService
    {
        public async Task<MissionModel> CreateMissionAsync(MissionDto missionDto)
        {
            MissionModel newMission = new()
            {
                Status = MissionStatus.Proposed,
            };
            await context.Missions.AddAsync(newMission);
            await context.SaveChangesAsync();
            return newMission;
        }

        public async Task<List<MissionModel>> GetAllMissionsAsync() =>
            await context.Missions.ToListAsync();
    }
}
