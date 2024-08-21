using AgentsRest.Dto;
using AgentsRest.Models;

namespace AgentsRest.Services
{
    public interface ITargetService
    {
        public Task<TargetModel> CreateTargetAsync(TargetDto target);
        public Task<TargetModel> GetTargetByIdAsync(int id);
    }
}
