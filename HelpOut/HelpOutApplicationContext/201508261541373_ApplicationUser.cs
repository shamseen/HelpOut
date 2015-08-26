namespace HelpOut.HelpOutApplicationContext
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ApplicationUser : DbMigration
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
                        User_UserID = c.Int(),
                        User_UserID1 = c.Int(),
                        Organization_Id = c.String(maxLength: 128),
                        ApplicationUser_Id = c.String(maxLength: 128),
                        ApplicationUser_Id1 = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.EventID)
                .ForeignKey("dbo.Users", t => t.User_UserID)
                .ForeignKey("dbo.Users", t => t.User_UserID1)
                .ForeignKey("dbo.AspNetUsers", t => t.Organization_Id)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id1)
                .Index(t => t.User_UserID)
                .Index(t => t.User_UserID1)
                .Index(t => t.Organization_Id)
                .Index(t => t.ApplicationUser_Id)
                .Index(t => t.ApplicationUser_Id1);
            
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
                .PrimaryKey(t => t.UserID)
                .ForeignKey("dbo.Events", t => t.Event_EventID)
                .Index(t => t.Event_EventID);
            
            AddColumn("dbo.AspNetUsers", "FullName", c => c.String());
            AddColumn("dbo.AspNetUsers", "Location", c => c.String());
            AddColumn("dbo.AspNetUsers", "Description", c => c.String());
            AddColumn("dbo.AspNetUsers", "Website", c => c.String());
            AlterColumn("dbo.AspNetUsers", "PhoneNumber", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Events", "ApplicationUser_Id1", "dbo.AspNetUsers");
            DropForeignKey("dbo.Events", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Events", "Organization_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Users", "Event_EventID", "dbo.Events");
            DropForeignKey("dbo.Events", "User_UserID1", "dbo.Users");
            DropForeignKey("dbo.Events", "User_UserID", "dbo.Users");
            DropIndex("dbo.Users", new[] { "Event_EventID" });
            DropIndex("dbo.Events", new[] { "ApplicationUser_Id1" });
            DropIndex("dbo.Events", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.Events", new[] { "Organization_Id" });
            DropIndex("dbo.Events", new[] { "User_UserID1" });
            DropIndex("dbo.Events", new[] { "User_UserID" });
            AlterColumn("dbo.AspNetUsers", "PhoneNumber", c => c.String());
            DropColumn("dbo.AspNetUsers", "Website");
            DropColumn("dbo.AspNetUsers", "Description");
            DropColumn("dbo.AspNetUsers", "Location");
            DropColumn("dbo.AspNetUsers", "FullName");
            DropTable("dbo.Users");
            DropTable("dbo.Events");
        }
    }
}
