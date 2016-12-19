namespace FiveDevicesOrleans.Tests.Utils
{
    using System.Threading;
    using FiveDevicesOrleans.Utils;
    using FluentAssertions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class RandomizerTest
    {
		[TestMethod]
        public void GetRandomTemperature_WhenDefaultValues_ShouldReturnWithinDefaultBounds()
        {
            // Arrange
            var min = StaticConfiguration.TemperatureMinValue;
            var max = StaticConfiguration.TemperatureMaxValue;

            // Act
            var random = Randomizer.GetRandomTemperature();

            // Assert
            random.Should().BeLessOrEqualTo(max);
            random.Should().BeGreaterOrEqualTo(min);
        }

        [TestMethod]
        public void GetRandomTemperature_WhenCustomValues_ShouldReturnWithinCustomBounds()
        {
            // Arrange
            var min = 20;
            var max = 30;

            // Act
            var random = Randomizer.GetRandomTemperature(min, max);

            // Assert
            random.Should().BeLessOrEqualTo(max);
            random.Should().BeGreaterOrEqualTo(min);
        }

		[TestMethod]
        public void GetRandomDelayInSeconds_WhenDefaultValues_ShouldReturnWithinDefaultBounds()
        {
            // Arrange
            var min = StaticConfiguration.DelayInSecondsMinValue;
            var max = StaticConfiguration.DelayInSecondsMaxValue;

            // Act
            var random = Randomizer.GetRandomDelayInSeconds();

            // Assert
            random.Should().BeLessOrEqualTo(max);
            random.Should().BeGreaterOrEqualTo(min);
        }

        [TestMethod]
        public void GetRandomDelayInSeconds_WhenCustomValues_ShouldReturnWithinCustomBounds()
        {
            // Arrange
            var min = 20;
            var max = 30;

            // Act
            var random = Randomizer.GetRandomDelayInSeconds(min, max);

            // Assert
            random.Should().BeLessOrEqualTo(max);
            random.Should().BeGreaterOrEqualTo(min);
        }
    }
}
