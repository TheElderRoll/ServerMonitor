using ServerMonitor.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using WebGrease.Css.Extensions;

namespace ServerMonitor.Domain
{
    public class Watchdog  {
        private static Watchdog instance;
        private static System.Timers.Timer timer;
        private const int updatePeriod = 1000;
        private Dictionary<string, WPair<PerformanceCounter, float>> PerformanceCounters;

        private Watchdog()
        {
            timer = new System.Timers.Timer(updatePeriod);
            timer.Elapsed += (tmp1, tmp2) => ReadMetrics();
            timer.AutoReset = true;
            
            InitializeWatchdog();
            timer.Enabled = true;
        }

        public static Watchdog GetInstance()
        {
            if(instance == null)
                instance = new Watchdog();
            return instance;
        }

        public Watchdog ReadMetrics()
        {
            lock (PerformanceCounters)
            {
                PerformanceCounters.Keys.ForEach(delegate (string key)
                {
                    PerformanceCounters[key].Second = PerformanceCounters[key].First.NextValue();
                });
                return this;
            }
        }

        private void InitializeWatchdog()
        {
            PerformanceCounters = new Dictionary<string, WPair<PerformanceCounter, float>>() {
                { "CpuUsage", new WPair<PerformanceCounter, float>(new PerformanceCounter("Processor", "% Processor Time", "_Total"), 0f)},
                { "AvatableMemory", new WPair<PerformanceCounter, float>(new PerformanceCounter("Memory", "Available MBytes"), 0f)},
                { "DiskUsage", new WPair<PerformanceCounter, float>(new PerformanceCounter("PhysicalDisk", "% Disk Time", "_Total"), 0f)},
                { "DiskReads", new WPair<PerformanceCounter, float>(new PerformanceCounter("PhysicalDisk", "Disk Read Bytes/sec", "_Total"), 0f)},
                { "DiskWrites", new WPair<PerformanceCounter, float>(new PerformanceCounter("PhysicalDisk", "Disk Write Bytes/sec", "_Total"),0f)},
                { "CpuInterrupts", new WPair<PerformanceCounter, float>(new PerformanceCounter("Processor", "% Interrupt Time", "_Total"), 0f)},
                { "DiskQueueLength", new WPair<PerformanceCounter, float>(new PerformanceCounter("PhysicalDisk", "Avg. Disk Queue Length", "_Total"),0f)},
            };

            PerformanceCounters.Keys.ForEach(delegate (string key)
            {
                PerformanceCounters[key].First.NextValue();
            });

        }

        public Frame ToFrame()
        {
            lock (PerformanceCounters) {

                if (PerformanceCounters.Count == 0)
                    return new Frame();

                return new Frame()
                {
                    CpuUsagePercent = PerformanceCounters["CpuUsage"].Second,
                    AvatableMemory = PerformanceCounters["AvatableMemory"].Second,
                    DiskUsagePercent = PerformanceCounters["DiskUsage"].Second,
                    DiskReads = Convert.ToInt32(PerformanceCounters["DiskReads"].Second),
                    DiskWrites = Convert.ToInt32(PerformanceCounters["DiskWrites"].Second),
                    CpuInterrupts = Convert.ToInt32(PerformanceCounters["CpuInterrupts"].Second),
                    DiskQueue = Convert.ToInt32(PerformanceCounters["DiskQueueLength"].Second),
                    MemoryUsagePercent = GetMemoryUsagePercent(PerformanceCounters["AvatableMemory"].Second),
                    DateTime = DateTime.Now
                };
            }
        }

        private float GetMemoryUsagePercent(float avatableMemory)
        {
            ulong allMemory = BytesToMbytes(new Microsoft.VisualBasic.Devices.ComputerInfo().TotalPhysicalMemory);
            float usedMemory =  allMemory - avatableMemory;
            return usedMemory / (allMemory / 100);
        }

        private ulong BytesToMbytes(ulong number)
        {
            return number / 1024 / 1024;
        }

        public void Stop()
        {
            timer.Stop();
        }

        ~Watchdog() {
            PerformanceCounters.Keys.ForEach(delegate (string key)
            {
                PerformanceCounters[key].First.Dispose();
            });
        }
    }
}