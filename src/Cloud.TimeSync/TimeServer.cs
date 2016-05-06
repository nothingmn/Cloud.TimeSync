using System.Collections.Generic;

namespace Cloud.TimeSync
{
    public class TimeServer
    {
        public string Location { get; set; }
        public string HostName { get; set; }

        public static IList<TimeServer> TimeServers = new List<TimeServer>()
        {
            new TimeServer() {HostName = "pool.ntp.org", Location = "Global pool"},
            new TimeServer() {HostName = "nist1-macon.macon.ga.us", Location = "Macon, Georgia"},
            new TimeServer() {HostName = "nist.netservicesgroup.com", Location = "Southfield, Michigan"},
            new TimeServer() {HostName = "nisttime.carsoncity.k12.mi.us", Location = "Carson City, Michigan"},
            new TimeServer() {HostName = "nist1-lnk.binary.net", Location = "Lincoln, Nebraska"},
            new TimeServer() {HostName = "time.nist.gov", Location = "Multiple locations - global address for all servers"},
            new TimeServer() {HostName = "ntp-nist.ldsbc.net", Location = "LDSBC, Salt Lake City, Utah"},
            new TimeServer() {HostName = "nist-time-server.eoni.com", Location = "La Grande, Oregon"},
            new TimeServer() {HostName = "nist-time-server.eoni.com", Location = "La Grande, Oregon"},

            new TimeServer() {HostName = "time-a.timefreq.bldrdoc.gov", Location = "NIST, Boulder, Colorado"},
            new TimeServer() {HostName = "time-b.timefreq.bldrdoc.gov", Location = "NIST, Boulder, Colorado"},
            new TimeServer() {HostName = "time-c.timefreq.bldrdoc.gov", Location = "NIST, Boulder, Colorado"},
            new TimeServer() {HostName = "utcnist.colorado.edu", Location = "University of Colorado, Boulder"},
            new TimeServer() {HostName = "utcnist2.colorado.edu", Location = "University of Colorado, Boulder"},
            new TimeServer() {HostName = "time.nist.gov", Location = "NCAR, Boulder, Colorado"},
            new TimeServer() {HostName = "time-nw.nist.gov", Location = "Microsoft, Redmond, Washington"},

            new TimeServer() {HostName = "time.windows.com", Location = "Microsoft, Redmond, Washington"},
        };
    }
}