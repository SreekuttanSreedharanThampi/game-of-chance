using GameOfChance.Models;
using GameOfChance.Services;
using Moq;

namespace Tests
{
    /// <summary>
    /// Unit tests for the GameService class
    /// </summary>
    public class GameServiceTests
    {
        private readonly IGameService _gameService;
        private readonly Mock<IRandomNumberGenerator> _mockRandomNumberGenerator;
        private readonly Mock<IUserManagementService> _mockUserManagementService;

        public GameServiceTests()
        {
            // Mocking the random number generator to always return 3 for consistency in the tests
            _mockRandomNumberGenerator = new Mock<IRandomNumberGenerator>();
            _mockRandomNumberGenerator.Setup(rng => rng.Next(It.IsAny<int>(), It.IsAny<int>())).Returns(3);

            // Mocking the user management service to return a predefined player
            _mockUserManagementService = new Mock<IUserManagementService>();
            _mockUserManagementService.Setup(s => s.GetPlayer(It.IsAny<int>())).Returns<int>(id => 
                new Player { PlayerId = id, AccountBalance = 10000 });

            // Initialize the GameService with mocked dependencies
            _gameService = new GameService(_mockRandomNumberGenerator.Object, _mockUserManagementService.Object);
        }

        /// <summary>
        /// Should add the points and return the player when a bet is placed and player wins
        /// </summary>
        [Fact]
        public void PlaceBet_ShouldAddPlayerPoints_WhenPlayerWins()
        {
            // Arrange
            var betRequest = new BetRequest {Points = 100, Number = 3 };

            // Act
            var betResponse = _gameService.PlaceBet(1, betRequest.Points, betRequest.Number);

            // Assert
            Assert.NotNull(betResponse);
            Assert.Equal(10900, betResponse.AccountBalance);
            Assert.Equal("+900", betResponse.Points);
        }

        /// <summary>
        /// Should subtract points when player loses the bet
        /// </summary>
        [Fact]
        public void PlaceBet_ShouldSubtractPlayerPoints_WhenPlayerLoses()
        {
            // Arrange
            // Number is not 3, so the player loses
            var betRequest = new BetRequest { Points = 100, Number = 2 };

            // Act
            var betResponse = _gameService.PlaceBet(1, betRequest.Points, betRequest.Number);

            // Assert
            Assert.NotNull(betResponse);
            // Player loses the bet (100 points are deducted)
            Assert.Equal(9900, betResponse.AccountBalance);
            Assert.Equal("-100", betResponse.Points);
        }

        /// <summary>
        /// Should throw exception when player does not have enough points
        /// </summary>
        [Fact]
        public void PlaceBet_ShouldThrowException_WhenNotEnoughPoints()
        {
            // Arrange
            // Player doesn't have enough points
            var betRequest = new BetRequest { Points = 20000, Number = 3 };

            // Act & Assert
            var exception = Assert.Throws<InvalidOperationException>(() => 
            _gameService.PlaceBet(1, betRequest.Points, betRequest.Number));
            Assert.Equal("Not enough points to place the bet", exception.Message);
        }

        /// <summary>
        /// Should return correct status as "won" when the player wins
        /// </summary>
        [Fact]
        public void PlaceBet_ShouldReturnWonStatus_WhenPlayerWins()
        {
            // Arrange
            var betRequest = new BetRequest { Points = 100, Number = 3 };

            // Act
            var betResponse = _gameService.PlaceBet(1, betRequest.Points, betRequest.Number);

            // Assert
            Assert.Equal(BetStatus.Won, betResponse.Status);
        }

        /// <summary>
        /// Should return correct status as "lost" when the player loses
        /// </summary>
        [Fact]
        public void PlaceBet_ShouldReturnLostStatus_WhenPlayerLoses()
        {
            // Arrange
            // Number is different from the random number
            var betRequest = new BetRequest { Points = 100, Number = 2 };

            // Act
            var betResponse = _gameService.PlaceBet(1, betRequest.Points, betRequest.Number);

            // Assert
            Assert.Equal(BetStatus.Lost, betResponse.Status);
        }

    }
}
