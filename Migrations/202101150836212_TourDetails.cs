namespace IlCicerone.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TourDetails : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Tours", "Story", c => c.String());
            AddColumn("dbo.Tours", "ArtandCulture", c => c.String());
            AddColumn("dbo.Tours", "EntertainmentandRecreation", c => c.String());
            AddColumn("dbo.Tours", "Whattovisitandexcursions", c => c.String());
            AddColumn("dbo.Tours", "Gastronomy", c => c.String());
            AddColumn("dbo.Tours", "Howandwhentoarrive", c => c.String());
            DropColumn("dbo.Tours", "Itinerary");
            DropColumn("dbo.Tours", "MettingLocation");
            DropColumn("dbo.Tours", "Transport");
            DropColumn("dbo.Tours", "Included");
            DropColumn("dbo.Tours", "EstimatedCach");
            DropColumn("dbo.Tours", "Extras");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Tours", "Extras", c => c.String());
            AddColumn("dbo.Tours", "EstimatedCach", c => c.String());
            AddColumn("dbo.Tours", "Included", c => c.String());
            AddColumn("dbo.Tours", "Transport", c => c.String());
            AddColumn("dbo.Tours", "MettingLocation", c => c.String());
            AddColumn("dbo.Tours", "Itinerary", c => c.String());
            DropColumn("dbo.Tours", "Howandwhentoarrive");
            DropColumn("dbo.Tours", "Gastronomy");
            DropColumn("dbo.Tours", "Whattovisitandexcursions");
            DropColumn("dbo.Tours", "EntertainmentandRecreation");
            DropColumn("dbo.Tours", "ArtandCulture");
            DropColumn("dbo.Tours", "Story");
        }
    }
}
