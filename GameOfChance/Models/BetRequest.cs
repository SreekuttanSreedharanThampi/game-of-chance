namespace GameOfChance.Models
{
    /// <summary>
    /// Represents a request to place a bet in the game.
    /// </summary>
    public class BetRequest
    {
        /// <summary>
        /// The number of points player wants to bet.
        /// </summary>
        public int Points { get; set; }
        /// <summary>
        /// The number the player wants to bet on.
        /// </summary>
        public int Number { get; set; }
    }
}
