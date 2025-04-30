using GameOfChance.Controllers;
using GameOfChance.Models;
using GameOfChance.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Tests.Controller_Test
{
    /// <summary>
    /// Unit tests for the GameController class
    /// </summary>
    public class GameControllerTests
    {
        private readonly GameController _gameController;
        private readonly Mock<IGameService> _mockGameService;
        private readonly Mock<ISessionService> _mockSessionService;
        private readonly Mock<IBetValidationService> _mockBetValidationService;
        public GameControllerTests()
        {
            _mockGameService = new Mock<IGameService>();
            _mockSessionService = new Mock<ISessionService>();
            _mockBetValidationService = new Mock<IBetValidationService>();
            _gameController = new GameController(_mockGameService.Object,
                _mockSessionService.Object, _mockBetValidationService.Object);
        }

        /// <summary>
        /// Test to check if the PlaceBet method returns Ok when the bet is successful
        /// </summary>
        [Fact]
        public void PlaceBet_ShouldReturnOk_WhenBetIsSuccessful()
        {
            // Arrange
            var betRequest = new BetRequest { Points = 100, Number = 3 };
            var betResponse = new BetResponse { AccountBalance = 10900, Points = "+900", Status = BetStatus.Won };

            _mockSessionService.Setup(x => x.GetPlayerIdFromSession()).Returns(1);
            _mockBetValidationService.Setup(x => x.IsValidPredictedNumber(betRequest.Number)).Returns(true);
            _mockBetValidationService.Setup(x => x.IsValidPoints(betRequest.Points)).Returns(true);
            _mockGameService.Setup(x => x.PlaceBet(1, betRequest.Points, betRequest.Number)).Returns(betResponse);

            // Act
            var result = _gameController.PlaceBet(betRequest);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(betResponse, okResult.Value);
        }

        /// <summary>
        /// Test to check if the PlaceBet method returns BadRequest when the bet fails
        /// </summary>
        [Fact]
        public void PlaceBet_ShouldReturnBadRequest_WhenBetFails()
        {
            // Arrange
            var betRequest = new BetRequest { Points = 100, Number = 3 };

            _mockSessionService.Setup(x => x.GetPlayerIdFromSession()).Returns(1);
            _mockBetValidationService.Setup(x => x.IsValidPredictedNumber(betRequest.Number)).Returns(true);
            _mockBetValidationService.Setup(x => x.IsValidPoints(betRequest.Points)).Returns(true);
            _mockGameService.Setup(x => x.PlaceBet(1, betRequest.Points, betRequest.Number))
                .Throws(new InvalidOperationException("Not enough points to place the bet"));

            // Act
            var result = _gameController.PlaceBet(betRequest);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Not enough points to place the bet", badRequestResult.Value);
        }

        /// <summary>
        /// Test to check if the PlaceBet method returns Unauthorized when the player is not logged in
        /// </summary>
        [Fact]
        public void PlaceBet_ShouldReturnUnauthorized_WhenPlayerIsNotLoggedIn()
        {
            // Arrange
            var betRequest = new BetRequest { Points = 100, Number = 3 };

            _mockSessionService.Setup(x => x.GetPlayerIdFromSession()).Returns((int?)null);

            // Act
            var result = _gameController.PlaceBet(betRequest);

            // Assert
            var unauthorizedResult = Assert.IsType<UnauthorizedObjectResult>(result);
            Assert.Equal("Player not logged in. Please create a new player.", unauthorizedResult.Value);
        }

        /// <summary>
        /// Test to check if the PlaceBet method returns BadRequest when the predicted number is invalid
        /// </summary>
        [Fact]
        public void PlaceBet_ShouldReturnBadRequest_WhenPredictedNumberIsInvalid()
        {
            // Arrange
            var betRequest = new BetRequest { Points = 100, Number = 15 };  // Invalid number (outside 0-9 range)

            _mockSessionService.Setup(x => x.GetPlayerIdFromSession()).Returns(1);
            _mockBetValidationService.Setup(x => x.IsValidPredictedNumber(betRequest.Number)).Returns(false);
            _mockBetValidationService.Setup(x => x.IsValidPoints(betRequest.Points)).Returns(true);

            // Act
            var result = _gameController.PlaceBet(betRequest);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Predicted number must be between 0 and 9.", badRequestResult.Value);
        }

        /// <summary>
        /// Test to check if the PlaceBet method returns BadRequest when the points is invalid
        /// </summary>
        [Fact]
        public void PlaceBet_ShouldReturnBadRequest_WhenBetPointsAreInvalid()
        {
            // Arrange
            // Invalid points (<= 0)
            var betRequest = new BetRequest { Points = 0, Number = 3 };  

            _mockSessionService.Setup(x => x.GetPlayerIdFromSession()).Returns(1);
            _mockBetValidationService.Setup(x => x.IsValidPredictedNumber(betRequest.Number)).Returns(true);
            _mockBetValidationService.Setup(x => x.IsValidPoints(betRequest.Points)).Returns(false);

            // Act
            var result = _gameController.PlaceBet(betRequest);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Bet points must be greater than 0.", badRequestResult.Value);
        }

    }
}
