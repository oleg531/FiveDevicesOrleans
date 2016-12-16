namespace FiveDevicesOrleans
{
    using Orleans;
    public interface ITemperatureReceiver : IGrainObserver
    {
        void ReceiveTemperature(DeviceMessage deviceMessage);
    }
}