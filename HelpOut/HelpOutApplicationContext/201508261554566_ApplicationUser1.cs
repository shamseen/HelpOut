namespace HelpOut.HelpOutApplicationContext
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ApplicationUser1 : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Events", name: "Organization_Id", newName: "OrganizationID");
            RenameIndex(table: "dbo.Events", name: "IX_Organization_Id", newName: "IX_OrganizationID");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Events", name: "IX_OrganizationID", newName: "IX_Organization_Id");
            RenameColumn(table: "dbo.Events", name: "OrganizationID", newName: "Organization_Id");
        }
    }
}
