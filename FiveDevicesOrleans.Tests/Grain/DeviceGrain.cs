namespace FiveDevicesOrleans.Tests.Grain
{
    using System;
    using System.Threading.Tasks;
    using FiveDevicesOrleans.Grain;
    using FiveDevicesOrleans.Receiver;
    using FiveDevicesOrleans.Utils;
    using Orleans;

    //Dublicate implementation for test passing
    public class DeviceGrain : Grain, IDeviceGrain
    {
        private ObserverSubscriptionManager<ITemperatureReceiver> _subscriptionManager;
        private bool _emitTemperature;

        public override async Task OnActivateAsync()
        {
            _subscriptionManager = new ObserverSubscriptionManager<ITemperatureReceiver>();
            _emitTemperature = false;
            await base.OnActivateAsync();
        }

        public Task StopEmitTemperature()
        {
            _emitTemperature = false;
            return TaskDone.Done;
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

        public Task StartEmitTemperature()
        {
            _emitTemperature = true;

            while (_emitTemperature)
            {
                var delaySeconds = Randomizer.GetRandomDelayInSeconds();
                Task.Delay(TimeSpan.FromSeconds(delaySeconds)).Wait();
                _subscriptionManager.Notify(receiver =>
                {
                    receiver.ReceiveTemperature(new DeviceMessage
                    {
                        DeviceId = this.GetPrimaryKeyLong().ToString(),
                        Temperature = Randomizer.GetRandomTemperature(),
                        TimeStamp = DateTime.Now.Ticks
                    });
                });
            }

            return TaskDone.Done;
        }
    }
}
