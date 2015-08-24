namespace HelpOut.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingSignups : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Events", "User_UserID", "dbo.Users");
            DropForeignKey("dbo.Users", "Event_EventID", "dbo.Events");
            DropForeignKey("dbo.Events", "User_UserID1", "dbo.Users");
            DropIndex("dbo.Events", new[] { "OrganizationID" });
            DropIndex("dbo.Events", new[] { "User_UserID" });
            DropIndex("dbo.Events", new[] { "User_UserID1" });
            DropIndex("dbo.Users", new[] { "Event_EventID" });
            DropColumn("dbo.Events", "OrganizationID");
            RenameColumn(table: "dbo.Events", name: "User_UserID1", newName: "OrganizationID");
            CreateTable(
                "dbo.Signups",
                c => new
                    {
                        EventID = c.Int(nullable: false),
                        UserID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.EventID, t.UserID })
                .ForeignKey("dbo.Events", t => t.EventID, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserID, cascadeDelete: false)
                .Index(t => t.EventID)
                .Index(t => t.UserID);
            
            AlterColumn("dbo.Events", "OrganizationID", c => c.Int(nullable: false));
            CreateIndex("dbo.Events", "OrganizationID");
            AddForeignKey("dbo.Events", "OrganizationID", "dbo.Users", "UserID", cascadeDelete: true);
            DropColumn("dbo.Events", "User_UserID");
            DropColumn("dbo.Users", "Event_EventID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Users", "Event_EventID", c => c.Int());
            AddColumn("dbo.Events", "User_UserID", c => c.Int());
            DropForeignKey("dbo.Events", "OrganizationID", "dbo.Users");
            DropForeignKey("dbo.Signups", "UserID", "dbo.Users");
            DropForeignKey("dbo.Signups", "EventID", "dbo.Events");
            DropIndex("dbo.Signups", new[] { "UserID" });
            DropIndex("dbo.Signups", new[] { "EventID" });
            DropIndex("dbo.Events", new[] { "OrganizationID" });
            AlterColumn("dbo.Events", "OrganizationID", c => c.Int());
            DropTable("dbo.Signups");
            RenameColumn(table: "dbo.Events", name: "OrganizationID", newName: "User_UserID1");
            AddColumn("dbo.Events", "OrganizationID", c => c.Int(nullable: false));
            CreateIndex("dbo.Users", "Event_EventID");
            CreateIndex("dbo.Events", "User_UserID1");
            CreateIndex("dbo.Events", "User_UserID");
            CreateIndex("dbo.Events", "OrganizationID");
            AddForeignKey("dbo.Events", "User_UserID1", "dbo.Users", "UserID");
            AddForeignKey("dbo.Users", "Event_EventID", "dbo.Events", "EventID");
            AddForeignKey("dbo.Events", "User_UserID", "dbo.Users", "UserID");
        }
    }
}
