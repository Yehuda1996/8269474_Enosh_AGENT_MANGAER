﻿using AgentsRest.Data;
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
                Name = target.name,
                Position = target.position,
                Image = target.photoUrl,
            };
            await context.Targets.AddAsync(newTarget);
            await context.SaveChangesAsync();
            return newTarget;
        }

        public async Task<TargetModel?> GetTargetByIdAsync(int id) =>
            await context.Targets.FirstOrDefaultAsync(x => x.Id == id)
            ?? throw new Exception($"There is no target by the id {id}");

        public async Task<List<TargetModel>> GetAllTargetsAsync() =>
            await context.Targets.ToListAsync();

        public async Task StartingCoordinatesForTargetByIdAsync(int id, CoordinatesDto coordinatesDto)
        {
            var target = await GetTargetByIdAsync(id);
            if (target == null)
            {
                throw new Exception($"There is no target with the id of {id}");
            }
            if (coordinatesDto.x < 0 || coordinatesDto.x > 1000 || coordinatesDto.y > 1000 || coordinatesDto.y < 0)
            {
                throw new Exception("One or more coordinates are out of range!!!");
            }
            target.Coordinate_x = coordinatesDto.x;
            target.Coordinate_y = coordinatesDto.y;
            context.Targets.Update(target);
            await context.SaveChangesAsync();
        }
        //see the comment by AgentsService
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

        public async Task MoveTargetById(int id, MoveDto moveDto)
        {
            var target = await GetTargetByIdAsync(id);
            var (x, y) = calcDirection[moveDto.Direction];
            await StartingCoordinatesForTargetByIdAsync(id, new()
            {
                x = x + target.Coordinate_x,
                y = y + target.Coordinate_y
            });
            target.Coordinate_x += x;
            target.Coordinate_y += y;
            if (target.Coordinate_x < 0 || target.Coordinate_x > 1000 || target.Coordinate_y > 1000 || target.Coordinate_y < 0)
            {
                throw new Exception("One or more coordinates are out of range!!!");
            }
            context.Targets.Update(target);
            await context.SaveChangesAsync();
        }

        public async Task DeleteTargetByIdAsync(int id)
        {
            var deleteTarget = await GetTargetByIdAsync(id);
            if (deleteTarget == null)
            {
                throw new Exception($"There is no target by the id {id}");
            }
            context.Targets.Remove(deleteTarget);
            await context.SaveChangesAsync();
        }
    }
}
