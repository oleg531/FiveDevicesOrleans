namespace FiveDevicesOrleans.Tests.Receiver
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using FiveDevicesOrleans.Receiver;
    using FluentAssertions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class TemperatureReceiverExtensions
    {
        [TestMethod]
        public void CalculateAverageTemperatureForLastCalcPeriod_WhenValuesPresent_ShouldReturnAverageTemperature()
        {
            // Arrange
            var tr = new TemperatureReceiver();
            var timeStampNow = DateTime.Now.Ticks;

            var dm1 = new DeviceMessage
            {
                DeviceId = "123",
                TimeStamp = timeStampNow,
                Temperature = 50
            };
            tr.ReceiveTemperature(dm1);
            var dm2 = new DeviceMessage
            {
                DeviceId = dm1.DeviceId + "123",
                TimeStamp = timeStampNow,
                Temperature = 60
            };
            tr.ReceiveTemperature(dm2);

            // Act
            var averageTemp = tr.CalculateAverageTemperatureForLastCalcPeriod(timeStampNow);

            // Assert
            averageTemp.ShouldBeEquivalentTo(new List<double> { dm1.Temperature, dm2.Temperature }.Average());
        }

        [TestMethod]
        public void CalculateAverageTemperatureForLastCalcPeriod_WhenNoActualValuesPresent_ShouldReturnZero()
        {
            // Arrange
            var tr = new TemperatureReceiver();
            var timeStampNow = DateTime.Now.Ticks;

            var dm = new DeviceMessage
            {
                DeviceId = "123",
                TimeStamp = new TimeSpan(timeStampNow).Subtract(TimeSpan.FromSeconds(StaticConfiguration.AvrgTemperaturePeriodCalcSeconds + 1)).Ticks,
                Temperature = 50
            };
            tr.ReceiveTemperature(dm);

            // Act
            var averageTemp = tr.CalculateAverageTemperatureForLastCalcPeriod(timeStampNow);

            // Assert
            averageTemp.ShouldBeEquivalentTo(0);
        }

        [TestMethod]
        public void CalculateAverageTemperatureForLastCalcPeriod_WhenNoValuesPresent_ShouldReturnZero()
        {
            // Arrange
            var tr = new TemperatureReceiver();
            var timeStampNow = DateTime.Now.Ticks;

            // Act
            var averageTemp = tr.CalculateAverageTemperatureForLastCalcPeriod(timeStampNow);

            // Assert
            averageTemp.ShouldBeEquivalentTo(0);
        }
    }
}
