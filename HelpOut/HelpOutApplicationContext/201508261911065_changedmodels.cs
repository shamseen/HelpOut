namespace HelpOut.HelpOutApplicationContext
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changedmodels : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Events",
                c => new
                    {
                        EventID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        DateTime = c.DateTime(nullable: false),
                        Location = c.String(),
                        Description = c.String(),
                        OrganizationID = c.String(maxLength: 128),

                    })
                .PrimaryKey(t => t.EventID)

                .ForeignKey("dbo.AspNetUsers", t => t.OrganizationID)
                .Index(t => t.OrganizationID);
               
            
            AddColumn("dbo.AspNetUsers", "Event_EventID", c => c.Int());
            CreateIndex("dbo.AspNetUsers", "Event_EventID");
            AddForeignKey("dbo.AspNetUsers", "Event_EventID", "dbo.Events", "EventID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Events", "OrganizationID", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUsers", "Event_EventID", "dbo.Events");
           
            DropIndex("dbo.AspNetUsers", new[] { "Event_EventID" });
          
            DropIndex("dbo.Events", new[] { "OrganizationID" });
            DropColumn("dbo.AspNetUsers", "Event_EventID");
            DropTable("dbo.Events");
        }
    }
}
