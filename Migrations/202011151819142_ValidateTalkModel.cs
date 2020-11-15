namespace WebApplication1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ValidateTalkModel : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Talks", "Title", c => c.String(nullable: false));
            AlterColumn("dbo.Talks", "Abstract", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Talks", "Abstract", c => c.String());
            AlterColumn("dbo.Talks", "Title", c => c.String());
        }
    }
}
