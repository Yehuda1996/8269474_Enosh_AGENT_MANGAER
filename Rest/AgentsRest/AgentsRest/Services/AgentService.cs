using AgentsRest.Data;
using AgentsRest.Dto;
using AgentsRest.Models;
using Microsoft.EntityFrameworkCore;

namespace AgentsRest.Services
{
    public class AgentService(ApplicationDbContext context) : IAgentService
    {
        public async Task<AgentModel> CreateAgentAsync(AgentDto agent)
        {
            AgentModel newAgent = new()
            {
                Nickname = agent.Nickname,
                Image = agent.Photo_Url
            };
            await context.Agents.AddAsync(newAgent);
            await context.SaveChangesAsync();
            return newAgent;
            
        }

        public async Task<AgentModel?> GetAgentByIdAsync(int id) =>
            await context.Agents.FirstOrDefaultAsync(x => x.Id == id);
    }
}
