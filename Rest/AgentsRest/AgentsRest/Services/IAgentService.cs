using AgentsRest.Dto;
using AgentsRest.Models;

namespace AgentsRest.Services
{
    public interface IAgentService
    {
        public Task<AgentModel> CreateAgentAsync(AgentDto agent);
        public Task<AgentModel> GetAgentByIdAsync(int id);
        public Task<List<AgentModel>> GetAllAgentsAsync();
        public Task StartingCoordinatesForAgentByIdAsync(int id, CoordinatesDto coordinatesDto);
        public Task MoveAgentById(int id, MoveDto moveDto);
        public Task DeleteAgentByIdAsync(int id);
    }
}
