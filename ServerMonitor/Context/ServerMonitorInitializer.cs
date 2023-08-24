using ServerMonitor.Models;
using System;
using System.Collections.Generic;


namespace ServerMonitor.Context
{
    public class ServerMonitorInitializer: System.Data.Entity.DropCreateDatabaseIfModelChanges<ServerContext>
    {
        protected override void Seed(ServerContext context)
        {
            var frames = new List<Frame>()
            {
                new Frame(){CpuUsagePercent = 47.5f, MemoryUsagePercent = 68f, DiskUsagePercent= 100f,
                            DateTime = DateTime.Now, DiskQueue = 1200, DiskReads = 600, DiskWrites = 400, CpuInterrupts = 104 },
                new Frame(){CpuUsagePercent = 74.5f, MemoryUsagePercent = 38f, DiskUsagePercent = 82f,
                            DateTime = DateTime.Now, DiskQueue = 1000, DiskReads = 200, DiskWrites = 100, CpuInterrupts = 204 }
            };

            frames.ForEach(frame => { context.Frames.Add(frame); });
            context.SaveChanges();
        }
    }
}