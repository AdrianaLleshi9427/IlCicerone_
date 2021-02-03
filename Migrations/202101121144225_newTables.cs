namespace IlCicerone.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class newTables : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ArticleCollections",
                c => new
                    {
                        ArticleCollectionID = c.Int(nullable: false, identity: true),
                        UserId = c.String(maxLength: 128),
                        ArticleCollectionTitle = c.String(),
                        ArticleCollectionDetails = c.String(),
                        CreatedCol_at = c.DateTime(nullable: false),
                        UpdatedCol_at = c.DateTime(),
                        ArticleCollectionDate = c.DateTime(nullable: false),
                        ArticleColOwner = c.String(),
                    })
                .PrimaryKey(t => t.ArticleCollectionID)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Languages",
                c => new
                    {
                        LanguageId = c.Int(nullable: false, identity: true),
                        LanguageName = c.String(),
                        Code = c.String(),
                    })
                .PrimaryKey(t => t.LanguageId);
            
            CreateTable(
                "dbo.TourCollections",
                c => new
                    {
                        TourCollectionID = c.Int(nullable: false, identity: true),
                        UserId = c.String(maxLength: 128),
                        TourCollectionTitle = c.String(nullable: false),
                        TourCollectionDetails = c.String(nullable: false),
                        CreatedCol_at = c.DateTime(nullable: false),
                        TourCollectionDate = c.DateTime(nullable: false),
                        EndCol_date = c.DateTime(nullable: false),
                        UpdatedCol_at = c.DateTime(),
                        TourColOwner = c.String(),
                    })
                .PrimaryKey(t => t.TourCollectionID)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TourCollections", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.ArticleCollections", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.TourCollections", new[] { "UserId" });
            DropIndex("dbo.ArticleCollections", new[] { "UserId" });
            DropTable("dbo.TourCollections");
            DropTable("dbo.Languages");
            DropTable("dbo.ArticleCollections");
        }
    }
}
