using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cloud.TimeSync
{
    public static class NetworkDateTimeOffset
    {
        static NetworkDateTimeOffset()
        {
            //how often will we update our internal delta
            TimeSyncClientSettings.RefreshInterval = TimeSpan.FromHours(1).Milliseconds;
            //set our algorightm to use
            TimeSyncClientSettings.Mode = Mode.MultipleServerFirstWins;
            //start it up!
            TimeSyncClient.Running = true;
        }

        public static DateTimeOffset NetworkUtcNow()
        {
            while (!TimeSyncClient.Adjusted)
            {
                System.Threading.Thread.Sleep(50);
            }
            if (TimeSyncClient.Delta == TimeSpan.Zero) return DateTimeOffset.UtcNow;

            var now = DateTimeOffset.UtcNow;
            return now.Add(TimeSyncClient.Delta);
        }

        public static DateTimeOffset ToNetworkUtcNow(this DateTimeOffset dateTimeOffset)
        {
            while (!TimeSyncClient.Adjusted)
            {
                System.Threading.Thread.Sleep(50);
            }
            if (TimeSyncClient.Delta == TimeSpan.Zero) return dateTimeOffset;

            return dateTimeOffset.Add(TimeSyncClient.Delta);
        }
    }
}
