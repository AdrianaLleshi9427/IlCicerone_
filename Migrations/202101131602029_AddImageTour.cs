namespace IlCicerone.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddImageTour : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TourCollections", "CollectionImage", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.TourCollections", "CollectionImage");
        }
    }
}
