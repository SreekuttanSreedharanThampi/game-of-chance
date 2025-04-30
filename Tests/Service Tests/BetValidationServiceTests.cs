using GameOfChance.Services;

namespace Tests.Service_Tests
{
    /// <summary>
    /// Unit tests for the BetValidationService class
    /// </summary>
    public class BetValidationServiceTests
    {
        private readonly BetValidationService _betValidationService;

        public BetValidationServiceTests()
        {
            // Initialize the BetValidationService
            _betValidationService = new BetValidationService();
        }

        /// <summary>
        /// Test should return true when points are greater than 0
        /// </summary>
        [Fact]
        public void IsValidPoints_ShouldReturnTrue_WhenPointsAreGreaterThanZero()
        {
            // Arrange
            var points = 100;

            // Act
            var result = _betValidationService.IsValidPoints(points);

            // Assert
            // Should return true since points are greater than 0
            Assert.True(result);
        }

        /// <summary>
        /// Test should return false when points are less than or equal to 0
        /// </summary>
        [Fact]
        public void IsValidPoints_ShouldReturnFalse_WhenPointsAreLessThanOrEqualToZero()
        {
            // Arrange
            var points = 0;

            // Act
            var result = _betValidationService.IsValidPoints(points);

            // Assert
            // Should return false since points are less than or equal to 0
            Assert.False(result);
        }

        /// <summary>
        /// Test should return true when predicted number is between 0 and 9 (inclusive)
        /// </summary>
        [Fact]
        public void IsValidPredictedNumber_ShouldReturnTrue_WhenNumberIsValid()
        {
            // Arrange
            var predictedNumber = 5;

            // Act
            var result = _betValidationService.IsValidPredictedNumber(predictedNumber);

            // Assert
            // Should return true since the predicted number is between 0 and 9
            Assert.True(result);  
        }

        /// <summary>
        /// Test should return false when predicted number is less than 0
        /// </summary>
        [Fact]
        public void IsValidPredictedNumber_ShouldReturnFalse_WhenNumberIsLessThanZero()
        {
            // Arrange
            var predictedNumber = -1;

            // Act
            var result = _betValidationService.IsValidPredictedNumber(predictedNumber);

            // Assert
            // Should return false since the predicted number is less than 0
            Assert.False(result);
        }

        /// <summary>
        /// Test should return false when predicted number is greater than 9
        /// </summary>
        [Fact]
        public void IsValidPredictedNumber_ShouldReturnFalse_WhenNumberIsGreaterThanNine()
        {
            // Arrange
            var predictedNumber = 10;

            // Act
            var result = _betValidationService.IsValidPredictedNumber(predictedNumber);

            // Assert
            // Should return false since the predicted number is greater than 9
            Assert.False(result);  
        }
    }
}
