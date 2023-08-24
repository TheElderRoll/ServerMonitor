using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ServerMonitor.Models
{
    [Table("frames", Schema = "public")]
    public class Frame
    {
        [Key]
        public long Id { get; set; }
        [Column("cpu")]
        public float CpuUsagePercent { get; set; }
        [Column("ram")]
        public float MemoryUsagePercent { get; set; }
        [Column ("avatable_memory")]
        public float AvatableMemory { get; set; }
        [Column("disk")]
        public float DiskUsagePercent { get; set; }
        [Column("datetime")]
        public DateTime DateTime { get; set; }
        [Column("disk_reads")]
        public int DiskReads { get; set; }
        [Column("disk_writes")]
        public int DiskWrites { get; set; }
        [Column("disk_queue")]
        public int DiskQueue { get; set; }
        [Column("cpu_interrupts")]
        public int CpuInterrupts { get; set; }
    }
}