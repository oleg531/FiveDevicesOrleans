namespace FiveDevicesOrleans
{
    using System;
    using System.Threading.Tasks;
    using Orleans;
    using Orleans.Runtime.Configuration;
    using System.Linq;
    using Grain;
    using Receiver;

    /// <summary>
    /// Orleans test silo host
    /// </summary>
    public class Program
    {
        static void Main(string[] args)
        {
            // The Orleans silo environment is initialized in its own app domain in order to more
            // closely emulate the distributed situation, when the client and the server cannot
            // pass data via shared memory.
            AppDomain hostDomain = AppDomain.CreateDomain("OrleansHost", null, new AppDomainSetup
            {
                AppDomainInitializer = InitSilo,
                AppDomainInitializerArguments = args,
            });

            var config = ClientConfiguration.LocalhostSilo();
            GrainClient.Initialize(config);

            //Start devices - grains
            var grains =
                Enumerable.Range(0, StaticConfiguration.DeviceCount)
                    .Select(x => GrainClient.GrainFactory.GetGrain<IDeviceGrain>(x))
                    .ToList();
            var receiver = new TemperatureReceiver();
            var receiverObj = GrainClient.GrainFactory.CreateObjectReference<ITemperatureReceiver>(receiver).Result;
            var subscribeTasks = grains.Select((g, i) => g.Subscribe(receiverObj));
            Task.WhenAll(subscribeTasks);
            grains.ForEach(g => g.StartEmitTemperature());

            Console.WriteLine("Orleans Silo is running.\nPress Enter to terminate...");

            while (!Console.KeyAvailable)
            {
                Task.Delay(TimeSpan.FromSeconds(StaticConfiguration.AvrgTemperatureCalcSeconds)).Wait();

                var timeStampNow = DateTime.Now.Ticks;
                var averageTemperature = receiver.CalculateAverageTemperatureForLastCalcPeriod(timeStampNow);

                //remove old values
                Task.Run(() => { receiver.RemoveTemperatureOldValues(timeStampNow); });

                Console.WriteLine(
                    $"AverageTemperature: {averageTemperature:F}, TimeStamp: {timeStampNow}, Second: {new DateTime(timeStampNow).Second}, CountDictionary: {receiver.MessagesDictionary.Count}");
            }            

            hostDomain.DoCallBack(ShutdownSilo);
        }

        static void InitSilo(string[] args)
        {
            hostWrapper = new OrleansHostWrapper(args);

            if (!hostWrapper.Run())
            {
                Console.Error.WriteLine("Failed to initialize Orleans silo");
            }
        }

        static void ShutdownSilo()
        {
            if (hostWrapper != null)
            {
                hostWrapper.Dispose();
                GC.SuppressFinalize(hostWrapper);
            }
        }

        private static OrleansHostWrapper hostWrapper;
    }
}
