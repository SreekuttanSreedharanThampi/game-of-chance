using GameOfChance.Services;

namespace Tests.Service_Tests
{
    /// <summary>
    /// Unit tests for the UserManagementService class
    /// </summary>
    public class UserManagementServiceTests
    {
        private readonly UserManagementService _userManagementService;

        public UserManagementServiceTests()
        {
            // Initialize the UserManagementService
            _userManagementService = new UserManagementService();
        }

        /// <summary>
        /// Test should return a player when it exists in the dictionary
        /// </summary>
        [Fact]
        public void GetPlayer_ShouldReturnPlayer_WhenPlayerExists()
        {
            // Arrange
            // Create a player to add to the service
            var createdPlayer = _userManagementService.CreatePlayer();

            // Act
            var player = _userManagementService.GetPlayer(createdPlayer.PlayerId);

            // Assert
            Assert.NotNull(player);
            // PlayerId should match the created player
            Assert.Equal(createdPlayer.PlayerId, player.PlayerId);
            // Player should have the default balance
            Assert.Equal(10000, player.AccountBalance);
        }

        /// <summary>
        /// Test should throw KeyNotFoundException when player does not exist
        /// </summary>
        [Fact]
        public void GetPlayer_ShouldThrowKeyNotFoundException_WhenPlayerDoesNotExist()
        {
            // Arrange
            int nonExistentPlayerId = 999; // Use a non-existing PlayerId

            // Act & Assert
            var exception = Assert.Throws<KeyNotFoundException>(() =>
            _userManagementService.GetPlayer(nonExistentPlayerId));
            // Ensure the exception message is correct
            Assert.Equal("Player not found.", exception.Message); 
        }

        /// <summary>
        /// Test should create new player with default points
        /// </summary>
        [Fact]
        public void CreatePlayer_ShouldReturnNewPlayer_WithUniquePlayerId()
        {
            // Arrange
            // Create first player
            var player1 = _userManagementService.CreatePlayer();
            // Create second player
            var player2 = _userManagementService.CreatePlayer();

            // Act
            // Assert that PlayerId of second player is unique
            Assert.Equal(1, player1.PlayerId);
            Assert.Equal(2, player2.PlayerId);

            // Assert that the player balances are correct
            // Default account balance should be 10000
            Assert.Equal(10000, player1.AccountBalance);
            Assert.Equal(10000, player2.AccountBalance);
        }
    }
}
