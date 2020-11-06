namespace WebApplication1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using WebApplication1.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<WebApplication1.Models.CampContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            //ContextKey = "WebApplication1.Models.CampContext";
        }

        protected override void Seed(WebApplication1.Models.CampContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.

            /*
            if (!context.Camps.Any())
            {
                context.Camps.AddOrUpdate(x => x.CampId,
                  new Camp()
                  {
                      CampId = 1,
                      Moniker = "ATL2018",
                      Name = "Atlanta Code Camp",
                      EventDate = new DateTime(2018, 10, 18),
                      Location = new Location()
                      {
                          VenueName = "Atlanta Convention Center",
                          Address1 = "123 Main Street",
                          CityTown = "Atlanta",
                          StateProvince = "GA",
                          PostalCode = "12345",
                          Country = "USA"
                      },
                      Length = 1,
                      Talks = new Talk[]
                      {
                            new Talk
                            {
                              TalkId = 1,
                              Title = "Entity Framework From Scratch",
                              Abstract = "Entity Framework from scratch in an hour. Probably cover it all",
                              Level = 100,
                              Speaker = new Speaker
                              {
                                SpeakerId = 1,
                                FirstName = "Shawn",
                                LastName = "Wildermuth",
                                BlogUrl = "http://wildermuth.com",
                                Company = "Wilder Minds LLC",
                                CompanyUrl = "http://wilderminds.com",
                                GitHub = "shawnwildermuth",
                                Twitter = "shawnwildermuth"
                              }
                            },
                            new Talk
                            {
                              TalkId = 2,
                              Title = "Writing Sample Data Made Easy",
                              Abstract = "Thinking of good sample data examples is tiring.",
                              Level = 200,
                              Speaker = new Speaker
                              {
                                SpeakerId = 2,
                                FirstName = "Resa",
                                LastName = "Wildermuth",
                                BlogUrl = "http://shawnandresa.com",
                                Company = "Wilder Minds LLC",
                                CompanyUrl = "http://wilderminds.com",
                                GitHub = "resawildermuth",
                                Twitter = "resawildermuth"
                              }
                            }
                      }
                  }
                ); 
            }
            */
        }
    }
}
