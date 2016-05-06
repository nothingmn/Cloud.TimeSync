using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Cloud.TimeSync.Tests
{
    public class BasicTests
    {

        [Fact]
        public async Task UpdateSystemTime()
        {
            //for Admin priviledge level apps, just automatically update the system time
            TimeSyncClientSettings.AdjustSystemTime = true;
            await NTPTest();

        }

        /// <summary>
        /// These tests are more for sample usage, difficult to test when you dont know the expected values
        /// </summary>
        /// <returns></returns>

        [Fact]
        public async Task NTPTestViaInterface()
        {
            INetworkDateTimeOffset instance = new NetworkDateTimeOffsetInstance();
            
            var dto = DateTimeOffset.UtcNow;
            Debug.WriteLine($"Before:\t{dto}");

            var adjusted = instance.ToNetworkUtcNow(dto);
            Debug.WriteLine($"After adjusted:\t{adjusted}");

            var alreadyAdjusted = instance.NetworkUtcNow();
            Debug.WriteLine($"Already adjusted:\t{alreadyAdjusted}");

        }


        [Fact]
        public async Task NTPTest()
        {
                var dto = DateTimeOffset.UtcNow;
                Debug.WriteLine($"Before:\t{dto}");

                var adjusted = dto.ToNetworkUtcNow();
                Debug.WriteLine($"After adjusted:\t{adjusted}");

                var alreadyAdjusted = NetworkDateTimeOffset.NetworkUtcNow();
                Debug.WriteLine($"Already adjusted:\t{alreadyAdjusted}");

        }

        [Fact]
        public async Task TestTimeServers()
        {
            List<double> timings = new List<double>();
            foreach (var server in Cloud.TimeSync.TimeServer.TimeServers)
            {
                try
                {
                    var client = new NTPClient(server.HostName);
                    client.Connect(false);
                    var transmit = client.TransmitTimestamp;
                    var Delta = DateTimeOffset.UtcNow - transmit;
                    timings.Add(Delta.TotalMilliseconds);
                    Assert.NotEqual(TimeSpan.Zero, Delta);
                    Debug.WriteLine($"{Delta.TotalMilliseconds}");
                }
                catch (Exception e)
                {
                    Debug.WriteLine($"BAD SERVER:{server.HostName} {e}");
                }
            }
            Debug.WriteLine($"Min:{timings.Min()}, Max:{timings.Max()}, Average:{timings.Average()}");
        }

    }
}