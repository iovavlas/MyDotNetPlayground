namespace WebApplication1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Seed_the_Database_with_some_sample_data : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO Locations (VenueName, Address1, CityTown, StateProvince, PostalCode, Country) VALUES ('Kalymnos Convention Center', 'Pothia', 'Kalymnos', '12', '85200', 'GR')");
        }

        public override void Down()
        {
        }
    }
}
