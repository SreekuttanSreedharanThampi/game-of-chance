using GameOfChance.Services;
using Microsoft.AspNetCore.Http;
using Moq;

namespace Tests.Service_Tests
{
    /// <summary>
    /// Unit tests for the SessionService class
    /// </summary>
    public class SessionServiceTests
    {
        private readonly Mock<IHttpContextAccessor> _mockHttpContextAccessor;
        private readonly Mock<ISession> _mockSession;
        private readonly SessionService _sessionService;

        public SessionServiceTests()
        {
            // Mock HttpContext and Session
            _mockHttpContextAccessor = new Mock<IHttpContextAccessor>();
            _mockSession = new Mock<ISession>();

            // Setup the HttpContext to return the mocked session
            _mockHttpContextAccessor.Setup(h => h.HttpContext.Session).Returns(_mockSession.Object);

            // Initialize the SessionService with the mocked IHttpContextAccessor
            _sessionService = new SessionService(_mockHttpContextAccessor.Object);
        }

        /// <summary>
        /// Test should return null if session is not available
        /// </summary>
        [Fact]
        public void GetPlayerIdFromSession_ShouldReturnNull_WhenSessionIsNotAvailable()
        {
            // Arrange
            // Mock session to be null
            _mockHttpContextAccessor.Setup(h => h.HttpContext.Session).Returns((ISession)null);

            // Act
            var result = _sessionService.GetPlayerIdFromSession();

            // Assert
            // Should return null when session is not available
            Assert.Null(result);
        }

        /// <summary>
        /// Test should throw InvalidOperationException when session is not available while storing PlayerId
        /// </summary>
        [Fact]
        public void StorePlayerIdInSession_ShouldThrowException_WhenSessionIsNotAvailable()
        {
            // Arrange: Mock session to be null
            _mockHttpContextAccessor.Setup(h => h.HttpContext.Session).Returns((ISession)null);

            // Act & Assert
            var exception = Assert.Throws<InvalidOperationException>(() =>
            _sessionService.StorePlayerIdInSession(1));
            // Ensure the exception message is correct
            Assert.Equal("Session is not available.", exception.Message);
        }
    }
}
