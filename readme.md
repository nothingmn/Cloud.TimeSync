Cloud.TimeSync
=

A basic / minimal way to get your system, and/or application in sync via a NTP time server.  


The need arose when working with multiple sets of cloud servers (vms, worker roles, etc) and the local time on each machine is not completely in sync.  As messages cross machine boundries you want to be able to stamp the message, if these times are not in sync the recording timings become nonsense.

Default is to NOT update the system time and provide an easy way to get UtcNow via a "NetworkDateTimeOffset", and to only update its internal delta every hour.


