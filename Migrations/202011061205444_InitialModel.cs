namespace WebApplication1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Camps",
                c => new
                    {
                        CampId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Moniker = c.String(),
                        EventDate = c.DateTime(nullable: false),
                        Length = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CampId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Camps");
        }
    }
}
