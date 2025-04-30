namespace GameOfChance.Models
{
    /// <summary>
    /// Represents a player in the game.
    /// </summary>
    public class Player
    {
        /// <summary>
        /// The unique identifier for the player.
        /// </summary>
        public int PlayerId { get; set; }

        /// <summary>
        /// Gets or sets the current point of the player.
        /// </summary>
        public int AccountBalance { get; set; } 
    }
}
