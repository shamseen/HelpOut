namespace HelpOut.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
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
                        OrganizationID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.EventID)
                .ForeignKey("dbo.Users", t => t.OrganizationID, cascadeDelete: true)
                .Index(t => t.OrganizationID);
            
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
                    })
                .PrimaryKey(t => t.UserID);
            
            CreateTable(
                "dbo.Signups",
                c => new
                    {
                        EventID = c.Int(nullable: false),
                        UserID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.EventID, t.UserID })
                .ForeignKey("dbo.Events", t => t.EventID, cascadeDelete: false)
                .ForeignKey("dbo.Users", t => t.UserID, cascadeDelete: true)
                .Index(t => t.EventID)
                .Index(t => t.UserID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Signups", "UserID", "dbo.Users");
            DropForeignKey("dbo.Signups", "EventID", "dbo.Events");
            DropForeignKey("dbo.Events", "OrganizationID", "dbo.Users");
            DropIndex("dbo.Signups", new[] { "UserID" });
            DropIndex("dbo.Signups", new[] { "EventID" });
            DropIndex("dbo.Events", new[] { "OrganizationID" });
            DropTable("dbo.Signups");
            DropTable("dbo.Users");
            DropTable("dbo.Events");
        }
    }
}
