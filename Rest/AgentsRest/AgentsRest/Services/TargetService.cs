using AgentsRest.Data;
using AgentsRest.Dto;
using AgentsRest.Models;
using Microsoft.EntityFrameworkCore;

namespace AgentsRest.Services
{
    public class TargetService(ApplicationDbContext context) : ITargetService
    {
        public async Task<TargetModel> CreateTargetAsync(TargetDto target)
        {
            TargetModel newTarget = new()
            {
                Name = target.Name,
                Position = target.Position,
                Image = target.Photo_Url,
            };
            await context.Targets.AddAsync(newTarget);
            await context.SaveChangesAsync();
            return newTarget;
        }

        public async Task<TargetModel?> GetTargetByIdAsync(int id) =>
            await context.Targets.FirstOrDefaultAsync(x => x.Id == id); 
    }
}
