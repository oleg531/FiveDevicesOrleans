namespace FiveDevicesOrleans.Grain
{
    using System.Threading.Tasks;
    using Orleans;
    using Receiver;

    public interface IDeviceGrain : IGrainWithIntegerKey
    {
        Task EmmitTemperature();
        Task Subscribe(ITemperatureReceiver observer);
        Task UnSubscribe(ITemperatureReceiver observer);
    }
}
