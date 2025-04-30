using GameOfChance.Models;

namespace GameOfChance.Services
{
    /// <summary>
    /// Game service for handling player bets, creating new players, and updating points
    /// </summary>
    public class GameService(IRandomNumberGenerator randomNumberGenerator,
        IUserManagementService userManagementService) : IGameService
    {
        /// <summary>
        /// Places a bet for the player and updates their points based on the game logic
        /// </summary>
        /// <param name="betRequest"></param>
        /// <returns>The player object</returns>
        public BetResponse PlaceBet(int playerId, int points, int number)
        {
            var player = userManagementService.GetPlayer(playerId);

            BetStatus status = BetStatus.Lost;
            string pointsChanged = "";

            // Check if the player has enough points to place the bet
            if (player.AccountBalance < points)
            {
                throw new InvalidOperationException("Not enough points to place the bet");
            }
            // Generate a random number between 0 and 9
            var randomNumber = randomNumberGenerator.Next(0, 10);

            // If the predicted number matches, the player wins
            if (number == randomNumber)
            {
                // Player wins 9 times the bet points
                player.AccountBalance += points * 9;
                status = BetStatus.Won;
                pointsChanged = $"+{points * 9}";
            }
            else
            {
                // Player loses, subtract bet points
                player.AccountBalance -= points;
                pointsChanged = $"-{points}";
            }

            var betResponse = new BetResponse
            {
                AccountBalance = player.AccountBalance,
                Status = status,
                Points = pointsChanged
            };

            return betResponse;
        }

        
    }
}
