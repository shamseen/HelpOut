namespace HelpOut.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EventsCascadeDeleteFalse : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Signups", "EventID", "dbo.Events");
            DropForeignKey("dbo.Signups", "UserID", "dbo.Users");
            DropForeignKey("dbo.Events", "OrganizationID", "dbo.Users");
            DropIndex("dbo.Signups", new[] { "EventID" });
            DropIndex("dbo.Signups", new[] { "UserID" });
            AddColumn("dbo.Events", "User_UserID", c => c.Int());
            AddColumn("dbo.Events", "User_UserID1", c => c.Int());
            AddColumn("dbo.Users", "Event_EventID", c => c.Int());
            CreateIndex("dbo.Events", "User_UserID");
            CreateIndex("dbo.Events", "User_UserID1");
            CreateIndex("dbo.Users", "Event_EventID");
            AddForeignKey("dbo.Events", "User_UserID", "dbo.Users", "UserID");
            AddForeignKey("dbo.Users", "Event_EventID", "dbo.Events", "EventID");
            AddForeignKey("dbo.Events", "User_UserID1", "dbo.Users", "UserID");
            DropTable("dbo.Signups");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Signups",
                c => new
                    {
                        EventID = c.Int(nullable: false),
                        UserID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.EventID, t.UserID });
            
            DropForeignKey("dbo.Events", "User_UserID1", "dbo.Users");
            DropForeignKey("dbo.Users", "Event_EventID", "dbo.Events");
            DropForeignKey("dbo.Events", "User_UserID", "dbo.Users");
            DropIndex("dbo.Users", new[] { "Event_EventID" });
            DropIndex("dbo.Events", new[] { "User_UserID1" });
            DropIndex("dbo.Events", new[] { "User_UserID" });
            DropColumn("dbo.Users", "Event_EventID");
            DropColumn("dbo.Events", "User_UserID1");
            DropColumn("dbo.Events", "User_UserID");
            CreateIndex("dbo.Signups", "UserID");
            CreateIndex("dbo.Signups", "EventID");
            AddForeignKey("dbo.Events", "OrganizationID", "dbo.Users", "UserID", cascadeDelete: true);
            AddForeignKey("dbo.Signups", "UserID", "dbo.Users", "UserID", cascadeDelete: true);
            AddForeignKey("dbo.Signups", "EventID", "dbo.Events", "EventID", cascadeDelete: true);
        }
    }
}
