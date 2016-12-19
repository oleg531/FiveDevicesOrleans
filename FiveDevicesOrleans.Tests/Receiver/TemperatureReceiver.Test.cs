namespace FiveDevicesOrleans.Tests.Receiver
{
    using System;
    using System.Linq;
    using System.IO;
    using FiveDevicesOrleans.Receiver;
    using FluentAssertions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class TemperatureReceiverTests
    {
        [TestMethod]
        public void ReceiveTemperature_WhenReceiveTemperature_ShouldAddToMessagesDictionary()
        {
            // Arrange
            var tr = new TemperatureReceiver();
            var currentTimeStamp = DateTime.Now.Ticks;
            var deviceMessage = new DeviceMessage
            {
                DeviceId = "123",
                Temperature = 50,
                TimeStamp = currentTimeStamp
            };

            // Act
            tr.ReceiveTemperature(deviceMessage);


            // Assert
            tr.MessagesDictionary.Should().NotBeNull();
            tr.MessagesDictionary.Should().NotBeEmpty();
            tr.MessagesDictionary.First().Value.ShouldBeEquivalentTo(deviceMessage);
        }

        [TestMethod]
        public void ReceiveTemperature_WhenReceiveTresholdTemperature_WriteToOutput()
        {
            // Arrange
            var tr = new TemperatureReceiver();
            var currentTimeStamp = DateTime.Now.Ticks;
            var deviceMessage = new DeviceMessage
            {
                DeviceId = "123",
                Temperature = StaticConfiguration.TemperatureThreshold + 1,
                TimeStamp = currentTimeStamp
            };

            // Act
            string consoleOutput;
            using (var sw = new StringWriter())
            {
                Console.SetOut(sw);
                tr.ReceiveTemperature(deviceMessage);
                consoleOutput = sw.ToString();
            }


            // Assert            
            consoleOutput.ShouldBeEquivalentTo(
                $"Threshold reached! DeviceId: {deviceMessage.DeviceId} , Temperature: {deviceMessage.Temperature}, TimeStamp: {new DateTime(deviceMessage.TimeStamp):G}\r\n");
        }
    }
}
