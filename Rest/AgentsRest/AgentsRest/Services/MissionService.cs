using AgentsRest.Data;
using AgentsRest.Dto;
using AgentsRest.Models;
using Microsoft.EntityFrameworkCore;

namespace AgentsRest.Services
{
    public class MissionService(ApplicationDbContext context) : IMissionService
    {

        public async Task<MissionModel> CreateMissionAsync(MissionDto missionDto, AgentModel agentModel, TargetModel targetModel)
        {
            if (await CalcDistance(agentModel, targetModel) < 200)
            {
                MissionModel newMission = new()
                {
                    Status = MissionStatus.Proposed,
                };
                await context.Missions.AddAsync(newMission);
                await context.SaveChangesAsync();
                return newMission;
            }
            return null;
        }

        public async Task<MissionModel?> GetMissionById(int id) =>
            await context.Missions.FirstOrDefaultAsync(x => x.Id == id);

        public async Task<List<MissionModel>> GetAllMissionsAsync() =>
            await context.Missions.ToListAsync();
        public async Task<double> CalcDistance(AgentModel agentModel, TargetModel targetModel)
        {
            double distance = Math.Sqrt(Math.Pow(targetModel.Coordinate_x - agentModel.Coordinate_x, 2)
                + Math.Pow(targetModel.Coordinate_y - agentModel.Coordinate_y, 2));
            return distance;
        }

        public async Task MoveAgentTowardsTarget(MissionModel missionModel, AgentModel agentModel, TargetModel targetModel)
        {
            await GetMissionById(missionModel.Id);

            if(await  CalcDistance(agentModel, targetModel) < 200)
            {
                var assigned = missionModel.Status = MissionStatus.Assigned;
                if (agentModel.Coordinate_x < targetModel.Coordinate_x)
                {
                    agentModel.Coordinate_x++;
                }
                if(agentModel.Coordinate_y < targetModel.Coordinate_y)
                {
                    agentModel.Coordinate_y++;
                }
                if (agentModel.Coordinate_x > targetModel.Coordinate_x)
                {
                    agentModel.Coordinate_x--;
                }
                if (agentModel.Coordinate_y > targetModel.Coordinate_y)
                {
                    agentModel.Coordinate_y--;
                }
            }
        }
    }
}
