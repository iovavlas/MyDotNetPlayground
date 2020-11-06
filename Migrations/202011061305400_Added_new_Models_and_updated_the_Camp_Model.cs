namespace WebApplication1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Added_new_Models_and_updated_the_Camp_Model : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Locations",
                c => new
                    {
                        LocationId = c.Int(nullable: false, identity: true),
                        VenueName = c.String(),
                        Address1 = c.String(),
                        Address2 = c.String(),
                        Address3 = c.String(),
                        CityTown = c.String(),
                        StateProvince = c.String(),
                        PostalCode = c.String(),
                        Country = c.String(),
                    })
                .PrimaryKey(t => t.LocationId);
            
            CreateTable(
                "dbo.Talks",
                c => new
                    {
                        TalkId = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Abstract = c.String(),
                        Level = c.Int(nullable: false),
                        Camp_CampId = c.Int(),
                        Speaker_SpeakerId = c.Int(),
                    })
                .PrimaryKey(t => t.TalkId)
                .ForeignKey("dbo.Camps", t => t.Camp_CampId)
                .ForeignKey("dbo.Speakers", t => t.Speaker_SpeakerId)
                .Index(t => t.Camp_CampId)
                .Index(t => t.Speaker_SpeakerId);
            
            CreateTable(
                "dbo.Speakers",
                c => new
                    {
                        SpeakerId = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                        MiddleName = c.String(),
                        Company = c.String(),
                        CompanyUrl = c.String(),
                        BlogUrl = c.String(),
                        Twitter = c.String(),
                        GitHub = c.String(),
                    })
                .PrimaryKey(t => t.SpeakerId);
            
            AddColumn("dbo.Camps", "Location_LocationId", c => c.Int());
            CreateIndex("dbo.Camps", "Location_LocationId");
            AddForeignKey("dbo.Camps", "Location_LocationId", "dbo.Locations", "LocationId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Talks", "Speaker_SpeakerId", "dbo.Speakers");
            DropForeignKey("dbo.Talks", "Camp_CampId", "dbo.Camps");
            DropForeignKey("dbo.Camps", "Location_LocationId", "dbo.Locations");
            DropIndex("dbo.Talks", new[] { "Speaker_SpeakerId" });
            DropIndex("dbo.Talks", new[] { "Camp_CampId" });
            DropIndex("dbo.Camps", new[] { "Location_LocationId" });
            DropColumn("dbo.Camps", "Location_LocationId");
            DropTable("dbo.Speakers");
            DropTable("dbo.Talks");
            DropTable("dbo.Locations");
        }
    }
}
