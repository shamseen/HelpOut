namespace HelpOut.HelpOutApplicationContext
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RevertAddBirthday : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.AspNetUsers", "BirthDate");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "BirthDate", c => c.DateTime(nullable: false));
        }
    }
}
