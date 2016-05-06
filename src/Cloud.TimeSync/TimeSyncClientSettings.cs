using System;

namespace Cloud.TimeSync
{

    public enum Mode
    {
        //can update system time
        SingleServer,
        //will not update system time, even if set
        MultipleServerAveraging,
        //can update system time
        MultipleServerFirstWins
    }
    public static class TimeSyncClientSettings
    {
        public static Mode Mode { get; set; } = Mode.MultipleServerFirstWins;

        public static long RefreshInterval = (long)TimeSpan.FromHours(1).TotalMilliseconds;
        public static bool AdjustSystemTime { get; set; } = false;
    }
}