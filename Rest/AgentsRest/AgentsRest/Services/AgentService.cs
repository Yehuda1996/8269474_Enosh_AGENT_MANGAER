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
            await context.Agents.FirstOrDefaultAsync(x => x.Id == id);

        public async Task<List<AgentModel>> GetAllAgentsAsync() =>
            await context.Agents.ToListAsync();


        public async Task StartingCoordinatesForAgentByIdAsync(int id, CoordinatesDto coordinatesDto)
        {
            var agent = await GetAgentByIdAsync(id);
            if (agent.Id != id)
            {
                throw new Exception($"There is no agent with the id of {id}");
            }
            if (coordinatesDto.x < 0 || coordinatesDto.x > 1000 || coordinatesDto.y > 1000 || coordinatesDto.y < 0)
            {
                throw new Exception("One or more coordinates are out of range!!!");
            }
            agent.Coordinate_x = coordinatesDto.x;
            agent.Coordinate_y = coordinatesDto.y;
            await context.Agents.AddAsync(agent);
            await context.SaveChangesAsync();
        }

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
            agent.Coordinate_x += x;
            agent.Coordinate_y += y;
            await context.Agents.AddAsync(agent);
            await context.SaveChangesAsync();
        }
    }
}
