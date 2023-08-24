using ServerMonitor.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServerMonitor.Domain
{
    public static class Format
    {
        public static Frame RoundPercents(Frame snapshot, int accuracy)
        {
            snapshot.CpuUsagePercent = (float)Math.Round(snapshot.CpuUsagePercent, accuracy);
            snapshot.MemoryUsagePercent = (float)Math.Round(snapshot.MemoryUsagePercent, accuracy);
            snapshot.DiskUsagePercent = (float)Math.Round(snapshot.DiskUsagePercent, accuracy);
            return snapshot;
        }


    }
}