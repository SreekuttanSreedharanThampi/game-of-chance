using GameOfChance.Models;

namespace GameOfChance.Services
{
    /// <summary>
    /// Service for managing user accounts and player information.
    /// </summary>
    public interface IUserManagementService
    {
        /// <summary>
        /// Retrieves a player based on the provided player ID
        /// If the player doesn't exist, a new player with default points is created
        /// </summary>
        /// <param name="playerId">The unique identifier of the player</param>
        /// <returns>The player object</returns>
        Player GetPlayer(int playerId);
        /// <summary>
        /// Creates a new player with a unique PlayerId and default points.
        /// </summary>
        /// <returns>The created player object</returns>
        Player CreatePlayer();

    }
}
