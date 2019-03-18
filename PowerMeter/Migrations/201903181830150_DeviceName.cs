namespace PowerMeter.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DeviceName : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "DeviceName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "DeviceName");
        }
    }
}
