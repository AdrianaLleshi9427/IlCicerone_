namespace IlCicerone.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class edit : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.AspNetUsers", "CategoryId", "dbo.Categories");
            DropForeignKey("dbo.AspNetUsers", "CityId", "dbo.Categories");
            DropIndex("dbo.AspNetUsers", new[] { "CategoryId" });
            DropIndex("dbo.AspNetUsers", new[] { "CityId" });
            DropColumn("dbo.AspNetUsers", "CategoryId");
            DropColumn("dbo.AspNetUsers", "CityId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "CityId", c => c.Int(nullable: false));
            AddColumn("dbo.AspNetUsers", "CategoryId", c => c.Int(nullable: false));
            CreateIndex("dbo.AspNetUsers", "CityId");
            CreateIndex("dbo.AspNetUsers", "CategoryId");
            AddForeignKey("dbo.AspNetUsers", "CityId", "dbo.Categories", "CategoryId", cascadeDelete: true);
            AddForeignKey("dbo.AspNetUsers", "CategoryId", "dbo.Categories", "CategoryId", cascadeDelete: true);
        }
    }
}
