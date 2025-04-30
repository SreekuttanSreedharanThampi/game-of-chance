namespace GameOfChance.Services
{
    public interface IRandomNumberGenerator
    {
        /// <summary>
        /// Generates a random number between the specified minimum and maximum values.
        /// </summary>
        /// <param name="minValue"></param>
        /// <param name="maxValue"></param>
        /// <returns>A random number<returns>
        int Next(int minValue, int maxValue);
    }
}
