namespace PowerMeter.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class KwhPrice : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "KwhPrice", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "KwhPrice");
        }
    }
}
