using AgentsRest.Dto;
using AgentsRest.Models;

namespace AgentsRest.Services
{
    public interface IAgentService
    {
        public Task<AgentModel> CreateAgentAsync(AgentDto agent);
        public Task<AgentModel> GetAgentByIdAsync(int id);
    }
}
