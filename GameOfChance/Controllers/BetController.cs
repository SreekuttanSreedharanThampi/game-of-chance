using GameOfChance.Models;
using GameOfChance.Services;
using Microsoft.AspNetCore.Mvc;

namespace GameOfChance.Controllers
{
    /// <summary>
    /// Controller for managing game-related operations, such as placing bets.
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class BetController(IGameService gameService,
        ISessionService sessionService,
        IBetValidationService betValidationService) : ControllerBase
    {

        /// <summary>
        /// Place a bet
        /// </summary>
        /// <param name="betRequest"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult PlaceBet([FromBody] BetRequest betRequest)
        {
           
            // Retrieve the PlayerId from the session
            var playerId = sessionService.GetPlayerIdFromSession();

            if (!playerId.HasValue)
            {
                return Unauthorized("Player not logged in. Please create a new player.");
            }
            // Validate the predicted number (should be between 0 and 9)
            if (!betValidationService.IsValidPredictedNumber(betRequest.Number))
            {
                return BadRequest("Predicted number must be between 0 and 9.");
            }
            // Validate that the points entered are greater than 0
            if (!betValidationService.IsValidPoints(betRequest.Points))
            {
                return BadRequest("Bet points must be greater than 0.");
            }

            try
            {
                var result = gameService.PlaceBet(playerId.Value, betRequest.Points, betRequest.Number);
                return Ok(result);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
