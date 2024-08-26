using AgentsRest.Data;
using AgentsRest.Dto;
using AgentsRest.Models;
using Microsoft.EntityFrameworkCore;

namespace AgentsRest.Services
{
    public class MissionService(ApplicationDbContext context, IAgentService agentService, IMissionService missionService) : IMissionService
    {

        public async Task<MissionModel> CreateMissionAsync(MissionDto missionDto, AgentModel agentModel, TargetModel targetModel)
        {
            if (await CalcDistance(agentModel, targetModel) < 200)
            {
                MissionModel newMission = new()
                {
                    Status = MissionStatus.Proposed,
                    TimeTillCompletion = await CalcTimeForMission(agentModel, targetModel),
                    TargetId = targetModel.Id,
                    AgentId = agentModel.Id,
                    TimeOfMission = DateTime.UtcNow
                };
                await context.Missions.AddAsync(newMission);
                await context.SaveChangesAsync();
                return newMission;
            }
            return null;
        }

        public async Task<MissionModel?> GetMissionById(int id) =>
            await context.Missions.FirstOrDefaultAsync(x => x.Id == id)
            ?? throw new Exception($"There is no mission with the id of {id}");

        public async Task<List<MissionModel>> GetAllMissionsAsync() =>
            await context.Missions.ToListAsync();

        public async Task<MissionModel> UpdateMissionStatusAsync(int id, MissionModel missionModel)
        {
            var statusUpdate =  await missionService.GetMissionById(id);
            if (statusUpdate == null)
            {
                throw new Exception($"There is no mission with the id of {id}");
            }
            statusUpdate.Status = missionModel.Status;
            context.Missions.Update(statusUpdate);
            await context.SaveChangesAsync();
            return statusUpdate;
            
        }
        public async Task<double> CalcTimeForMission(AgentModel agentModel, TargetModel targetModel)
        {
            var time = await CalcDistance(agentModel, targetModel) / 5;
            return time;
        }
        public async Task<double> CalcDistance(AgentModel agentModel, TargetModel targetModel)
        {
            double distance = Math.Sqrt(Math.Pow(targetModel.Coordinate_x - agentModel.Coordinate_x, 2)
                + Math.Pow(targetModel.Coordinate_y - agentModel.Coordinate_y, 2));
            return distance;
        }

        public async Task MoveAgentTowardsTargetAsync(MissionModel missionModel, AgentModel agentModel, TargetModel targetModel, MoveDto moveDto)
        {
            var mission = await GetMissionById(missionModel.Id);
            if (mission == null)
            {
                throw new Exception($"There is no misiion by the id of {missionModel.Id}");
            }


            if(await  CalcDistance(agentModel, targetModel) < 200)
            {
                mission.Status = MissionStatus.Assigned;
                mission.TimeOfMission = DateTime.Now;

                double direction_x = targetModel.Coordinate_x - agentModel.Coordinate_x;
                double direction_y = targetModel.Coordinate_y - agentModel.Coordinate_y;

                if (direction_x != 0)
                {
                    agentModel.Coordinate_x += Math.Sign(direction_x);
                }
                if (direction_y != 0)
                {
                    agentModel.Coordinate_y += Math.Sign(direction_y);
                }
                if (direction_x == 0 && direction_y == 0)
                {
                    targetModel.Status = TargetStatus.Eliminated;
                    missionModel.Status = MissionStatus.Completed;
                }

                context.Agents.Update(agentModel);
                context.Missions.Update(missionModel);
                context.Targets.Update(targetModel);
                await context.SaveChangesAsync();

            }
        }

        public async Task DeleteMissionByIdAsync(int id)
        {
            var deleteMission = await GetMissionById(id);
            if (deleteMission == null)
            {
                throw new Exception($"There is no mission with the id of {id}");
            }
            context.Missions.Remove(deleteMission);
            await context.SaveChangesAsync();
        }
    }
}
