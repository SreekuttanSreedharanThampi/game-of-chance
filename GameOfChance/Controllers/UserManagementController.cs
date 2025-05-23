﻿using GameOfChance.Models;
using GameOfChance.Services;
using Microsoft.AspNetCore.Mvc;

namespace GameOfChance.Controllers
{
    /// <summary>
    /// Controller for managing user-related operations, such as creating players.
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class UserManagementController(IUserManagementService userManagementService,
        ISessionService sessionService) : Controller
    {

        /// <summary>
        /// Create a new player
        /// </summary>
        /// <returns>A new player with PlayerId and Account Balance.</returns>
        [HttpPost]
        public IActionResult CreatePlayer()
        {
            try
            {
                // Create a new player using the game service
                var player = userManagementService.CreatePlayer();

                // Store the PlayerId in session
                sessionService.StorePlayerIdInSession(player.PlayerId);

                var response = new PlayerResponse
                {
                    PlayerId = player.PlayerId,
                    AccountBalance = player.AccountBalance
                };

                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
