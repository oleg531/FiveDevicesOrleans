using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FiveDevicesOrleans
{
    using Orleans;

    public class Program
    {
        public static void Main(string[] args)
        {
            GrainClient.Initialize();

            var deviceGrain = GrainClient.GrainFactory.GetGrain<IDeviceGrain>(0);
            var receiver = new TemperatureReceiver();
            var receiverObj = GrainClient.GrainFactory.CreateObjectReference<ITemperatureReceiver>(receiver).Result;
            deviceGrain.Subscribe(receiverObj).Wait();

            Console.ReadKey();
        }
    }
}
