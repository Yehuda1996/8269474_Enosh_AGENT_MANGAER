using AgentsRest.Dto;
using AgentsRest.Models;

namespace AgentsRest.Services
{
    public interface ITargetService
    {
        public Task<TargetModel> CreateTargetAsync(TargetDto target);
        public Task<TargetModel> GetTargetByIdAsync(int id);
        public Task<List<TargetModel>> GetAllTargetsAsync();
        public Task StartingCoordinatesForTargetByIdAsync(int id, CoordinatesDto coordinatesDto);
        public Task MoveTargetById(int id, MoveDto moveDto);
    }
}
