namespace GameOfChance.Services
{
    /// <summary>
    /// Implementation for bet validation service.
    /// </summary>
    public class BetValidationService : IBetValidationService
    {
        /// <summary>
        /// Validates the points entered by the player.
        /// </summary>
        /// <param name="points"></param>
        /// <returns>True or False</returns>
        public bool IsValidPoints(int points)
        {
            // Validate that the points entered are greater than 0
            if (points <= 0)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Validates the predicted number.
        /// </summary>
        /// <param name="predictedNumber"></param>
        /// <returns>True or False</returns>
        public bool IsValidPredictedNumber(int predictedNumber)
        {
            // Validate the predicted number (should be between 0 and 9)
            if (predictedNumber < 0 || predictedNumber > 9)
            {
                return false;
            }

            return true;
        }
    }
}
