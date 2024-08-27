using AgentsRest.Data;
using AgentsRest.Dto;
using AgentsRest.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace AgentsRest.Services
{
    public class AgentService(ApplicationDbContext context) : IAgentService
    {
        public async Task<AgentModel> CreateAgentAsync(AgentDto agent)
        {
            AgentModel newAgent = new()
            {
                Nickname = agent.nickname,
                Image = agent.photoUrl
            };
            await context.Agents.AddAsync(newAgent);
            await context.SaveChangesAsync();
            return newAgent;
            
        }

        public async Task<AgentModel?> GetAgentByIdAsync(int id) =>
            await context.Agents.FirstOrDefaultAsync(x => x.Id == id)
            ?? throw new Exception($"There is no agent by the id {id}");

        public async Task<List<AgentModel>> GetAllAgentsAsync() =>
            await context.Agents.ToListAsync();


        public async Task StartingCoordinatesForAgentByIdAsync(int id, CoordinatesDto coordinatesDto)
        {
            var agent = await GetAgentByIdAsync(id);
            if (agent == null)
            {
                throw new Exception($"There is no agent with the id of {id}");
            }
            if (coordinatesDto.x < 0 || coordinatesDto.x > 1000 || coordinatesDto.y > 1000 || coordinatesDto.y < 0)
            {
                throw new Exception("One or more coordinates are out of range!!!");
            }
            //making sure that starting points and movement are in range placement
            agent.Coordinate_x = coordinatesDto.x;
            agent.Coordinate_y = coordinatesDto.y;
			if (agent.Coordinate_x < 0 || agent.Coordinate_x > 1000 || agent.Coordinate_y > 1000 || agent.Coordinate_y < 0)
			{
				throw new Exception("One or more coordinates are out of range!!!");
			}
			context.Agents.Update(agent);
            await context.SaveChangesAsync();
        }
        // creating a Dictionary to help determine direction and where to move to
        private readonly Dictionary<string, (int x, int y)> calcDirection = new()
        {
            {"n", (x: 0, y: 1) },
            {"s", (x: 0, y: -1) },
            {"e", (x: 1, y: 0) },
            {"w", (x: -1, y: 0) },
            {"nw", (x: -1, y: 1) },
            {"ne", (x: 1, y: 1) },
            {"sw", (x: -1, y: -1) },
            {"se", (x: 1, y: -1) }
        };
        public async Task MoveAgentById(int id, MoveDto moveDto)
        {
            var agent = await GetAgentByIdAsync(id);
            var (x, y) = calcDirection[moveDto.Direction];
            await StartingCoordinatesForAgentByIdAsync(id, new()
                  {
                      x = x + agent.Coordinate_x,
                      y = y + agent.Coordinate_y
                  });
            // will take the last coordinates and update them accordingly
            agent.Coordinate_x += x; 
            agent.Coordinate_y += y;
            if (agent.Coordinate_x < 0 || agent.Coordinate_x > 1000 || agent.Coordinate_y > 1000 || agent.Coordinate_y < 0)
            {
                throw new Exception("One or more coordinates are out of range!!!");
            }
            context.Agents.Update(agent);
            await context.SaveChangesAsync();
        }

        public async Task DeleteAgentByIdAsync(int id)
        {
            var deleteAgent = await GetAgentByIdAsync(id);
            if (deleteAgent == null)
            {
                throw new Exception($"There is no agent by the id {id}");
            }
            context.Agents.Remove(deleteAgent);
            await context.SaveChangesAsync();
        }
    }
}
