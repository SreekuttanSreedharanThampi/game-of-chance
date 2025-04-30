namespace GameOfChance.Services
{
    /// <summary>
    /// Session service for managing player sessions.
    /// </summary>
    public class SessionService : ISessionService
    {
        private readonly IHttpContextAccessor httpContextAccessor;

        public SessionService(IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
        }

        /// <summary>
        /// Get the PlayerId from the session.
        /// </summary>
        /// <returns>Player id</returns>
        public int? GetPlayerIdFromSession()
        {
            // Ensure HttpContext and Session are not null before accessing them
            if (httpContextAccessor.HttpContext?.Session == null)
            {
                return null;
            }

            var sessionId = httpContextAccessor.HttpContext.Session.Id;
            var playerSessionKey = $"Player_{sessionId}";

            // Retrieve PlayerId from session using the unique session key
            return httpContextAccessor.HttpContext.Session.GetInt32(playerSessionKey);
        }

        /// <summary>
        /// Store the PlayerId in the session.
        /// </summary>
        /// <param name="playerId"></param>
        /// <exception cref="InvalidOperationException"></exception>
        public void StorePlayerIdInSession(int playerId)
        {
            if (httpContextAccessor.HttpContext?.Session == null)
            {
                throw new InvalidOperationException("Session is not available.");
            }

            var sessionId = httpContextAccessor.HttpContext.Session.Id;
            var playerSessionKey = $"Player_{sessionId}";

            // Store PlayerId in the session using the unique key
            httpContextAccessor.HttpContext.Session.SetInt32(playerSessionKey, playerId);
        }
    }
}
