namespace GameOfChance.Models
{
    /// <summary>
    /// Represents the response for player-related operations.
    /// </summary>
    public class PlayerResponse
    {
        /// <summary>
        /// PlayerId of the player
        /// </summary>
        public int PlayerId { get; set; }
        /// <summary>
        /// Account balance of the player
        /// </summary>
        public decimal AccountBalance { get; set; }
    }
}
