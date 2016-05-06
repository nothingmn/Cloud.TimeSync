using System;

namespace Cloud.TimeSync
{
    public interface INetworkDateTimeOffset
    {
        DateTimeOffset NetworkUtcNow();
        DateTimeOffset ToNetworkUtcNow(DateTimeOffset dateTimeOffset);
    }
}