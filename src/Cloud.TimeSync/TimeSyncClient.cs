using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Cloud.TimeSync
{

    public static class TimeSyncClient
    {
        static TimeSyncClient()
        {
            switch (TimeSyncClientSettings.Mode)
            {
                case Mode.MultipleServerAveraging:
                    _timer = new Timer(RefreshMultipleServers, null, 0, TimeSyncClientSettings.RefreshInterval);
                    break;
                case Mode.MultipleServerFirstWins:
                    _timer = new Timer(RefreshMultipleServersFirstWins, null, 0, TimeSyncClientSettings.RefreshInterval);
                    break;
                default:
                    _timer = new Timer(RefreshSingleServer, null, 0, TimeSyncClientSettings.RefreshInterval);
                    break;
            }
        }

        public static string TimeServer { get; set; } = "pool.ntp.org";
        public static bool Running = true;
        public static bool Adjusted = false;

        private static System.Threading.Timer _timer;

        private static List<TimeServer> BlackList = new List<TimeServer>();

        private static void RefreshMultipleServers(object state)
        {
            _timer.Change(Timeout.Infinite, Timeout.Infinite);

            List<TimeSpan> timings = new List<TimeSpan>();

            foreach (var server in TimeSync.TimeServer.TimeServers)
            {
                if (!BlackList.Contains(server))
                {
                    try
                    {
                        var client = new NTPClient(server.HostName);
                        client.Connect(false);
                        var subDelta = DateTimeOffset.UtcNow - client.TransmitTimestamp;
                        timings.Add(subDelta);
                    }
                    catch (Exception e)
                    {
                        BlackList.Add(server);
                    }
                }
            }

            var avg = new TimeSpan((long) timings.Average(span => span.Ticks));

            Delta = avg;
            Adjusted = true;
            if (Running)
            {
                _timer.Change(TimeSyncClientSettings.RefreshInterval, TimeSyncClientSettings.RefreshInterval);
            }
        }

        private static void RefreshMultipleServersFirstWins(object state)
        {
            _timer.Change(Timeout.Infinite, Timeout.Infinite);

            foreach (var server in TimeSync.TimeServer.TimeServers)
            {
                if (!BlackList.Contains(server))
                {
                    try
                    {
                        var client = new NTPClient(server.HostName);
                        client.Connect(TimeSyncClientSettings.AdjustSystemTime);
                        Delta = DateTimeOffset.UtcNow - client.TransmitTimestamp;
                        break;
                    }
                    catch (Exception e)
                    {
                        BlackList.Add(server);
                    }
                }
            }
            Adjusted = true;
            if (Running)
            {
                _timer.Change(TimeSyncClientSettings.RefreshInterval, TimeSyncClientSettings.RefreshInterval);
            }
        }

        private static void RefreshSingleServer(object state)
        {
            _timer.Change(Timeout.Infinite, Timeout.Infinite);
            var ntp = new NTPClient(TimeServer);
            ntp.Connect(TimeSyncClientSettings.AdjustSystemTime);

            var transmit = ntp.TransmitTimestamp;
            Delta = DateTimeOffset.UtcNow - transmit;

            Adjusted = true;
            if (Running)
            {
                _timer.Change(TimeSyncClientSettings.RefreshInterval, TimeSyncClientSettings.RefreshInterval);
            }
        }

        public static TimeSpan Delta { get; private set; } = TimeSpan.Zero;
    }
}