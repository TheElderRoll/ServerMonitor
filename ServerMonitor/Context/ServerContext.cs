using ServerMonitor.Models;
using System.Data.Entity;
using System.Data.Entity.Core.Common;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace ServerMonitor.Context
{
    public class ServerContext : DbContext
    {
        public DbSet<Frame> Frames { get; set; }
        public ServerContext() : base("ServerMonitor") { }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();   
        }
    }
}