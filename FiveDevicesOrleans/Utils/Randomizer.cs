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

        public static int GetRandomTemperature(int minValue = StaticConfiguration.TemperatureMinValue,
            int maxValue = StaticConfiguration.TemperatureMaxValue)
        {
            return GetRandomIntValue(minValue, maxValue);
        }

        public static int GetRandomDelayInSeconds(int minValue = StaticConfiguration.DelayInSecondsMinValue,
            int maxValue = StaticConfiguration.DelayInSecondsMaxValue)
        {
            return GetRandomIntValue(minValue, maxValue);
        }

        private static int GetRandomIntValue(int minValue, int maxValue)
        {
            return _randomizer.Next(minValue, maxValue);
        }
    }
}
