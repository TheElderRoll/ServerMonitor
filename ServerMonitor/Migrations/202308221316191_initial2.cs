namespace ServerMonitor.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial2 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "public.frames",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        cpu = c.Single(nullable: false),
                        ram = c.Single(nullable: false),
                        avatable_memory = c.Single(nullable: false),
                        disk = c.Single(nullable: false),
                        datetime = c.DateTime(nullable: false),
                        disk_reads = c.Int(nullable: false),
                        disk_writes = c.Int(nullable: false),
                        disk_queue = c.Int(nullable: false),
                        cpu_interrupts = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("public.frames");
        }
    }
}
