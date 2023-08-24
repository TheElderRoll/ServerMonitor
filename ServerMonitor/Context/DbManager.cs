using ServerMonitor.Domain;
using ServerMonitor.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Threading.Tasks;

namespace ServerMonitor.Context
{
    public class DbManager
    {
        private static DbManager instance;
        private System.Timers.Timer timer;
        private const int updatePeriod = 5000;

        public static DbManager GetInstance()
        {
            if (instance == null)
                instance = new DbManager();
            return instance;
        }

        private DbManager() 
        {
            
            timer = new System.Timers.Timer(updatePeriod);
            timer.Elapsed += (tmp1, tmp2) => SaveFrame(Watchdog.GetInstance().ToFrame());
            timer.AutoReset = true;
            timer.Enabled = true;
        }

        public void SaveFrame(Frame snapshot)
        {
            using (ServerContext context = new ServerContext())
            {
                context.Frames.Add(snapshot);
                context.SaveChanges();
            }
        }

        public async Task<List<Frame>> GetFramesByPeriod(DateTime? startTime, DateTime? endTime)
        {
            using (ServerContext context = new ServerContext())
            {
                return await context.Frames.Where(x => x.DateTime >= startTime &&
                                                    x.DateTime <= endTime).ToListAsync();
            }
        }

        public void Stop()
        {
            timer.Stop();
        }
    }
}