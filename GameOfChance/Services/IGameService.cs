using GameOfChance.Models;

namespace GameOfChance.Services
{
    /// <summary>
    /// Defines the operations for managing a game and player bets.
    /// </summary>
    public interface IGameService
    {
        /// <summary>
        /// Places a bet for the player, updating their points based on whether they win or lose
        /// </summary>
        /// <param name="playerId"></param>
        /// <param name="points"></param>
        /// <param name="number"></param>
        /// <returns>The updated player object</returns>
        BetResponse PlaceBet(int playerId, int points, int number);
    }
}