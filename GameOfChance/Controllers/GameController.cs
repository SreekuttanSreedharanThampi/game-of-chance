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
    public class GameController(IGameService gameService,
        ISessionService sessionService,
        IBetValidationService betValidationService) : ControllerBase
    {
        private readonly IGameService _gameService = gameService;
        private readonly ISessionService _sessionService = sessionService;
        private readonly IBetValidationService _betValidationService = betValidationService;

        /// <summary>
        /// Place a bet
        /// </summary>
        /// <param name="betRequest"></param>
        /// <returns></returns>
        [HttpPost("place-bet")]
        public IActionResult PlaceBet([FromBody] BetRequest betRequest)
        {
           
            // Retrieve the PlayerId from the session
            var playerId = _sessionService.GetPlayerIdFromSession();

            if (!playerId.HasValue)
            {
                return Unauthorized("Player not logged in. Please create a new player.");
            }
            // Validate the predicted number (should be between 0 and 9)
            if (!_betValidationService.IsValidPredictedNumber(betRequest.Number))
            {
                return BadRequest("Predicted number must be between 0 and 9.");
            }
            // Validate that the points entered are greater than 0
            if (!_betValidationService.IsValidPoints(betRequest.Points))
            {
                return BadRequest("Bet points must be greater than 0.");
            }

            try
            {
                var result = _gameService.PlaceBet(playerId.Value, betRequest.Points, betRequest.Number);
                return Ok(result);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
