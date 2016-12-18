namespace FiveDevicesOrleans.Receiver
{
    using System;
    using System.Collections.Concurrent;

    public class TemperatureReceiver : ITemperatureReceiver
    {
        public ConcurrentDictionary<string, DeviceMessage> MessagesDictionary { get; }

        public TemperatureReceiver()
        {
            MessagesDictionary = new ConcurrentDictionary<string, DeviceMessage>();
        }

        public void ReceiveTemperature(DeviceMessage deviceMessage)
        {
            var key = $"{deviceMessage.DeviceId}_{deviceMessage.TimeStamp}";
            MessagesDictionary.TryAdd(key, deviceMessage);
            if (deviceMessage.Temperature > StaticConfiguration.TemperatureThreshold)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine(
                    $"Threshold reached! DeviceId: {deviceMessage.DeviceId} , Temperature: {deviceMessage.Temperature}, TimeStamp: {new DateTime(deviceMessage.TimeStamp):G}");
                Console.ResetColor();
            }
        }
    }
}
