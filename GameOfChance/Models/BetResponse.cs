namespace GameOfChance.Models
{
    /// <summary>
    /// Represents the response after a bet is placed in the game.
    /// </summary>
    public class BetResponse
    {
        /// <summary>
        /// The updated account balance after the bet
        /// </summary>
        public int AccountBalance { get; set; }
        /// <summary>
        /// The status of the bet. "Won" or "Lost"
        /// </summary>
        public BetStatus Status { get; set; }
        /// <summary>
        /// The change in points, prefixed with "+" or "-" depending on the outcome of the bet
        /// </summary>
        public string Points { get; set; } = string.Empty;

    }
}
