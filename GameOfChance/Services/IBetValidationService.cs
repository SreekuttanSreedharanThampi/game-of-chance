namespace GameOfChance.Services
{
    /// <summary>
    /// Interface for bet validation service.
    /// </summary>
    public interface IBetValidationService
    {
        /// <summary>
        /// Validates the predicted number.
        /// </summary>
        /// <param name="predictedNumber"></param>
        /// <returns>True or False</returns>
        bool IsValidPredictedNumber(int predictedNumber);
        /// <summary>
        /// Validates the points entered by the player.
        /// </summary>
        /// <param name="points"></param>
        /// <returns>True or False</returns>
        bool IsValidPoints(int points);
    }
}
