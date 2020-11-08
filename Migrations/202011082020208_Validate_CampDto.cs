namespace WebApplication1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Validate_CampDto : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Camps", "Name", c => c.String(nullable: false));
            AlterColumn("dbo.Camps", "Moniker", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Camps", "Moniker", c => c.String());
            AlterColumn("dbo.Camps", "Name", c => c.String());
        }
    }
}
