using System;

namespace Cloud.TimeSync
{
    public class NetworkDateTimeOffsetInstance : INetworkDateTimeOffset
    {
        public DateTimeOffset NetworkUtcNow()
        {
            return NetworkDateTimeOffset.NetworkUtcNow();
        }

        public DateTimeOffset ToNetworkUtcNow(DateTimeOffset dateTimeOffset)
        {
            return dateTimeOffset.ToNetworkUtcNow();
        }
    }
}