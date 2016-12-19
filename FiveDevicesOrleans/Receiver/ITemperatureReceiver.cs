namespace FiveDevicesOrleans.Receiver
{
    using System.Collections.Concurrent;
    using Orleans;

    public interface ITemperatureReceiver : IGrainObserver
    {
        void ReceiveTemperature(DeviceMessage deviceMessage);        
    }
}