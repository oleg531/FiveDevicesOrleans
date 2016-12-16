namespace FiveDevicesOrleans
{
    using System.Threading.Tasks;
    using Orleans;

    public interface IDeviceGrain : IGrainWithIntegerKey
    {
        Task EmmitTemperature();
        Task Subscribe(ITemperatureReceiver observer);
        Task UnSubscribe(ITemperatureReceiver observer);
    }
}
