namespace FiveDevicesOrleans.Receiver
{
    using System;
    using System.Linq;

    public static class TemperatureReceiverExtensions
    {
        public static double CalculateAverageTemperatureForLastCalcPeriod(this TemperatureReceiver receiver, long timeStampNow)
        {
            var temperatures =
                receiver.MessagesDictionary.Values.Where(
                        v =>
                            new TimeSpan(timeStampNow - v.TimeStamp).Seconds <=
                            StaticConfiguration.AvrgTemperaturePeriodCalcSeconds)
                    .ToList();
            return temperatures.Count > 0 ? temperatures.Average(v => v.Temperature) : 0;
        }

        public static void RemoveTemperatureOldValues(this TemperatureReceiver receiver, long timeStampNow)
        {
            var keysToRemove =
                receiver.MessagesDictionary.Where(
                        kvp =>
                            new TimeSpan(timeStampNow - kvp.Value.TimeStamp).Seconds >
                            StaticConfiguration.AvrgTemperaturePeriodCalcSeconds)
                    .Select(kvp => kvp.Key)
                    .ToList();
            DeviceMessage notUsedValue;
            keysToRemove.ForEach(k => receiver.MessagesDictionary.TryRemove(k, out notUsedValue));
        }
    }
}
