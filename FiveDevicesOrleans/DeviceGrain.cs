using System.Threading.Tasks;

namespace FiveDevicesOrleans
{
    using Orleans;

    public class DeviceGrain : Grain, IDeviceGrain
    {
        private ObserverSubscriptionManager<ITemperatureReceiver> _subscriptionManager;

        public override async Task OnActivateAsync()
        {
            _subscriptionManager = new ObserverSubscriptionManager<ITemperatureReceiver>();
            await base.OnActivateAsync();
        }

        public async Task Subscribe(ITemperatureReceiver observer)
        {
            _subscriptionManager.Subscribe(observer);
            await TaskDone.Done;
        }

        public async Task UnSubscribe(ITemperatureReceiver observer)
        {
            _subscriptionManager.Unsubscribe(observer);
            await TaskDone.Done;
        }

        public Task EmmitTemperature()
        {
            _subscriptionManager.Notify(receiver =>
            {
                receiver.ReceiveTemperature(new DeviceMessage
                {
                    DeviceId = this.GetPrimaryKeyString(),
                    Temperature = 123
                });
            });
            return TaskDone.Done;
        }
    }
}
