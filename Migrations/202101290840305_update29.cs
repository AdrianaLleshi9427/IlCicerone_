namespace IlCicerone.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update29 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "CountryId", c => c.Int());
            AddColumn("dbo.AspNetUsers", "LanguageId", c => c.Int());
            AddColumn("dbo.AspNetUsers", "CityId", c => c.Int());
            AddColumn("dbo.AspNetUsers", "Gender", c => c.String());
            AddColumn("dbo.AspNetUsers", "Birthday", c => c.DateTime(nullable: false));
            AddColumn("dbo.AspNetUsers", "YearlyTours", c => c.Int(nullable: false));
            AddColumn("dbo.AspNetUsers", "DistancePreferences", c => c.Int(nullable: false));
            AddColumn("dbo.AspNetUsers", "GroupSize", c => c.Int(nullable: false));
            AddColumn("dbo.AspNetUsers", "CityOrNature", c => c.Boolean(nullable: false));
            AddColumn("dbo.AspNetUsers", "SeaOrMountain", c => c.Boolean(nullable: false));
            AddColumn("dbo.AspNetUsers", "UseOwnVehicle", c => c.Boolean(nullable: false));
            AddColumn("dbo.AspNetUsers", "PreferedTrasport", c => c.String());
            AddColumn("dbo.AspNetUsers", "BudgetLimit", c => c.Single());
            AddColumn("dbo.AspNetUsers", "TourDuration", c => c.Int(nullable: false));
            CreateIndex("dbo.AspNetUsers", "CountryId");
            CreateIndex("dbo.AspNetUsers", "LanguageId");
            CreateIndex("dbo.AspNetUsers", "CityId");
            AddForeignKey("dbo.AspNetUsers", "CityId", "dbo.Cities", "CityId");
            AddForeignKey("dbo.AspNetUsers", "CountryId", "dbo.Countries", "CountryId");
            AddForeignKey("dbo.AspNetUsers", "LanguageId", "dbo.Languages", "LanguageId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUsers", "LanguageId", "dbo.Languages");
            DropForeignKey("dbo.AspNetUsers", "CountryId", "dbo.Countries");
            DropForeignKey("dbo.AspNetUsers", "CityId", "dbo.Cities");
            DropIndex("dbo.AspNetUsers", new[] { "CityId" });
            DropIndex("dbo.AspNetUsers", new[] { "LanguageId" });
            DropIndex("dbo.AspNetUsers", new[] { "CountryId" });
            DropColumn("dbo.AspNetUsers", "TourDuration");
            DropColumn("dbo.AspNetUsers", "BudgetLimit");
            DropColumn("dbo.AspNetUsers", "PreferedTrasport");
            DropColumn("dbo.AspNetUsers", "UseOwnVehicle");
            DropColumn("dbo.AspNetUsers", "SeaOrMountain");
            DropColumn("dbo.AspNetUsers", "CityOrNature");
            DropColumn("dbo.AspNetUsers", "GroupSize");
            DropColumn("dbo.AspNetUsers", "DistancePreferences");
            DropColumn("dbo.AspNetUsers", "YearlyTours");
            DropColumn("dbo.AspNetUsers", "Birthday");
            DropColumn("dbo.AspNetUsers", "Gender");
            DropColumn("dbo.AspNetUsers", "CityId");
            DropColumn("dbo.AspNetUsers", "LanguageId");
            DropColumn("dbo.AspNetUsers", "CountryId");
        }
    }
}
