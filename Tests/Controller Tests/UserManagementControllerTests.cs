using GameOfChance.Controllers;
using GameOfChance.Models;
using GameOfChance.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Tests.Controller_Test
{
    /// <summary>
    /// Unit tests for the UserManagementController class
    /// </summary>
    public class UserManagementControllerTests
    {
        private readonly UserManagementController _userManagementController;
        private readonly Mock<IUserManagementService> _mockUserManagementService;
        private readonly Mock<ISessionService> _mockSessionService;

        public UserManagementControllerTests()
        {
            _mockUserManagementService = new Mock<IUserManagementService>();
            _mockSessionService = new Mock<ISessionService>();

            // Initialize the UserManagementController with mocked dependencies
            _userManagementController = new UserManagementController(
                _mockUserManagementService.Object,
                _mockSessionService.Object
            );
        }

        /// <summary>
        /// Should return Ok when player is created successfully
        /// </summary>
        [Fact]
        public void CreatePlayer_ShouldReturnOk_WhenPlayerIsCreated()
        {
            // Arrange
            var newPlayer = new Player { PlayerId = 1, AccountBalance = 10000 };
            _mockUserManagementService.Setup(s => s.CreatePlayer()).Returns(newPlayer);
            _mockSessionService.Setup(s => s.StorePlayerIdInSession(newPlayer.PlayerId));

            // Act
            var result = _userManagementController.CreatePlayer();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var response = okResult.Value as PlayerResponse;

            Assert.NotNull(response);
            Assert.Equal(newPlayer.PlayerId, response.PlayerId);
            Assert.Equal(newPlayer.AccountBalance, response.AccountBalance);
        }

        /// <summary>
        /// Should call session service to store the player id when player is created
        /// </summary>
        [Fact]
        public void CreatePlayer_ShouldCallStorePlayerIdInSession_WhenPlayerIsCreated()
        {
            // Arrange
            var newPlayer = new Player { PlayerId = 1, AccountBalance = 10000 };
            _mockUserManagementService.Setup(s => s.CreatePlayer()).Returns(newPlayer);

            // Act
            _userManagementController.CreatePlayer();

            // Assert
            _mockSessionService.Verify(s => s.StorePlayerIdInSession(newPlayer.PlayerId), Times.Once);
        }
    }

}
