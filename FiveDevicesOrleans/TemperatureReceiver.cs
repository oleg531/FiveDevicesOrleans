﻿using System;

namespace FiveDevicesOrleans
{
    public class TemperatureReceiver : ITemperatureReceiver
    {
        public void ReceiveTemperature(DeviceMessage deviceMessage)
        {
            Console.WriteLine($"DeviceId: {deviceMessage.DeviceId} , Temperature: {deviceMessage.Temperature}");
        }
    }
}