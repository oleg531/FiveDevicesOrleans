namespace FiveDevicesOrleans.Utils
{
    using System;

    public static class Randomizer
    {
        private static readonly Random _randomizer;

        static Randomizer()
        {
            _randomizer = new Random();
        }

        public static int GetRandomTemperature(int minValue = 0, int maxValue = 100)
        {
            return GetRandomIntValue(minValue, maxValue);
        }

        public static int GetRandomDelayInSeconds(int minValue = 0, int maxValue = 5)
        {
            return GetRandomIntValue(minValue, maxValue);
        }

        private static int GetRandomIntValue(int minValue, int maxValue)
        {
            return _randomizer.Next(minValue, maxValue);
        }
    }
}
