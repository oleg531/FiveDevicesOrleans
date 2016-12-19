namespace FiveDevicesOrleans.Tests.Grain
{
    using System;
    using System.Threading.Tasks;
    using FiveDevicesOrleans.Grain;
    using FiveDevicesOrleans.Receiver;
    using FluentAssertions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Orleans.TestingHost;

    [TestClass]
    public class DevicesSiloTests :TestingSiloHost
    {

        [TestCleanup]
        public void TestCleanup()
        {
            // Optional.
            // By default, the next test class which uses TestingSiloHost will
            // cause a fresh Orleans silo environment to be created.
            StopAllSilos();
        }

        [TestMethod]
        public void EmitTemperatureGrain_ShouldEmitTemperatureMessage()
        {
            // Arrange
            var id = 0;            
            var deviceGrain = GrainFactory.GetGrain<IDeviceGrain>(id);
            var receiver = new TemperatureReceiver();
            var receiverObj = GrainFactory.CreateObjectReference<ITemperatureReceiver>(receiver).Result;
            deviceGrain.Subscribe(receiverObj).Wait();

            // Act
            deviceGrain.StartEmitTemperature();
            Task.Delay(TimeSpan.FromSeconds(StaticConfiguration.DelayInSecondsMaxValue)).Wait();

            // Assert
            receiver.MessagesDictionary.Count.Should().BeGreaterThan(0);
        }

    }
}
