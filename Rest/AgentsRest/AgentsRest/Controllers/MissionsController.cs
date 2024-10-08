﻿using AgentsRest.Dto;
using AgentsRest.Models;
using AgentsRest.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AgentsRest.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class MissionsController(IMissionService missionService) : ControllerBase
    {
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> CreateMission(MissionDto missionDto)
        {
            try
            {
                var newMission = await missionService.CreateMissionAsync(missionDto);
                return Created();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> GetAllMissions() =>
            Ok(await missionService.GetAllMissionsAsync());

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateStatus(int id, MissionModel missionModel) =>
            Ok(await missionService.UpdateMissionStatusAsync(id, missionModel));

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteMission(int id) => 
            Ok(missionService.DeleteMissionByIdAsync(id));

    }
}
