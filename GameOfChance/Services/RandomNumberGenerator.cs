namespace GameOfChance.Services
{
    public class RandomNumberGenerator : IRandomNumberGenerator
    {
        private readonly Random _random;
        public RandomNumberGenerator()
        {
            _random = new Random();
        }
        /// <summary>
        /// Generates a random number between the specified minimum and maximum values.
        /// </summary>
        /// <param name="minValue"></param>
        /// <param name="maxValue"></param>
        /// <returns>A random number<returns>
        public int Next(int minValue, int maxValue)
        {
            return _random.Next(minValue, maxValue);
        }
    }
}
