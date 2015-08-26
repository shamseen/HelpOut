namespace HelpOut.HelpOutApplicationContext
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Keys : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Events", "User_UserID", "dbo.Users");
            DropForeignKey("dbo.Events", "User_UserID1", "dbo.Users");
            DropIndex("dbo.Events", new[] { "User_UserID" });
            DropIndex("dbo.Events", new[] { "User_UserID1" });
            DropIndex("dbo.Users", new[] { "Event_EventID" });
            AddColumn("dbo.AspNetUsers", "Event_EventID", c => c.Int());
            CreateIndex("dbo.AspNetUsers", "Event_EventID");
            DropColumn("dbo.Events", "User_UserID");
            DropColumn("dbo.Events", "User_UserID1");
            DropTable("dbo.Users");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UserID = c.Int(nullable: false, identity: true),
                        Email = c.String(),
                        Password = c.String(),
                        FullName = c.String(),
                        Location = c.String(),
                        PhoneNumber = c.String(nullable: false),
                        Description = c.String(),
                        Website = c.String(),
                        Event_EventID = c.Int(),
                    })
                .PrimaryKey(t => t.UserID);
            
            AddColumn("dbo.Events", "User_UserID1", c => c.Int());
            AddColumn("dbo.Events", "User_UserID", c => c.Int());
            DropIndex("dbo.AspNetUsers", new[] { "Event_EventID" });
            DropColumn("dbo.AspNetUsers", "Event_EventID");
            CreateIndex("dbo.Users", "Event_EventID");
            CreateIndex("dbo.Events", "User_UserID1");
            CreateIndex("dbo.Events", "User_UserID");
            AddForeignKey("dbo.Events", "User_UserID1", "dbo.Users", "UserID");
            AddForeignKey("dbo.Events", "User_UserID", "dbo.Users", "UserID");
        }
    }
}
