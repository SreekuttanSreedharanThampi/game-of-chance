namespace GameOfChance.Services
{
    /// <summary>
    /// Interface for session service.
    /// </summary>
    public interface ISessionService
    {
        /// <summary>
        /// Get the PlayerId from the session.
        /// </summary>
        /// <returns>PlayerId if exists</returns>
        int? GetPlayerIdFromSession();
        /// <summary>
        /// Store the PlayerId in the session.
        /// </summary>
        /// <param name="playerId"></param>
        void StorePlayerIdInSession(int playerId);
    }
}
