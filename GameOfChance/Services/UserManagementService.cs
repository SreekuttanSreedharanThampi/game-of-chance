using GameOfChance.Models;

namespace GameOfChance.Services
{
    /// <summary>
    /// Service for managing user accounts and player information.
    /// </summary>
    public class UserManagementService : IUserManagementService
    {
        // In-memory storage for players
        private readonly Dictionary<int, Player> _players = new Dictionary<int, Player>();

        private int _nextPlayerId = 1; //Simple counter for generating new PlayerIds

        /// <summary>
        /// Retrieves a player by their ID, or creates a new player with new id and default points if not found
        /// </summary>
        /// <param name="playerId"></param>
        /// <returns>The player object</returns>
        // Retrieve a player by PlayerId
        public Player GetPlayer(int playerId)
        {
            // Check if the player exists in the dictionary
            if (_players.ContainsKey(playerId))
            {
                return _players[playerId];
            }
            // If player is not found, throw an exception
            throw new KeyNotFoundException("Player not found.");
        }

        /// <summary>
        /// Creates a new player with a unique PlayerId and default points.
        /// </summary>
        /// <returns>The newly created player</returns>
        public Player CreatePlayer()
        {
            // Assign a new PlayerId and default points
            var player = new Player { PlayerId = _nextPlayerId++, AccountBalance = 10000 };
            // Add the new player to the in-memory dictionary
            _players[player.PlayerId] = player;
            return player;
        }
    }
}
