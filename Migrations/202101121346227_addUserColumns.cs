namespace IlCicerone.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addUserColumns : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "Name", c => c.String());
            AddColumn("dbo.AspNetUsers", "Surname", c => c.String());
            AddColumn("dbo.AspNetUsers", "CategoryId", c => c.Int());
            AddColumn("dbo.AspNetUsers", "CityId", c => c.Int());
            AddColumn("dbo.Articles", "ArticleColId", c => c.Int(nullable: false));
            AddColumn("dbo.Articles", "LanguageId", c => c.Int());
            AddColumn("dbo.Articles", "ArticleBody", c => c.String());
            AddColumn("dbo.Articles", "ArticleOwner", c => c.String());
            AddColumn("dbo.Articles", "Status", c => c.String());
            AddColumn("dbo.Articles", "ParentArticleId", c => c.Int(nullable: false));
            AddColumn("dbo.Articles", "ArticleCollection_ArticleCollectionID", c => c.Int());
            AddColumn("dbo.Tours", "TourColId", c => c.Int(nullable: false));
            AddColumn("dbo.Tours", "LanguageId", c => c.Int());
            AddColumn("dbo.Tours", "TourOwner", c => c.String());
            AddColumn("dbo.Tours", "ParentTourId", c => c.String());
            AddColumn("dbo.Tours", "TourCollection_TourCollectionID", c => c.Int());
            CreateIndex("dbo.AspNetUsers", "CategoryId");
            CreateIndex("dbo.AspNetUsers", "CityId");
            CreateIndex("dbo.Articles", "LanguageId");
            CreateIndex("dbo.Articles", "ArticleCollection_ArticleCollectionID");
            CreateIndex("dbo.Tours", "LanguageId");
            CreateIndex("dbo.Tours", "TourCollection_TourCollectionID");
            AddForeignKey("dbo.AspNetUsers", "CategoryId", "dbo.Categories", "CategoryId");
            AddForeignKey("dbo.AspNetUsers", "CityId", "dbo.Categories", "CategoryId");
            AddForeignKey("dbo.Articles", "ArticleCollection_ArticleCollectionID", "dbo.ArticleCollections", "ArticleCollectionID");
            AddForeignKey("dbo.Articles", "LanguageId", "dbo.Languages", "LanguageId");
            AddForeignKey("dbo.Tours", "LanguageId", "dbo.Languages", "LanguageId");
            AddForeignKey("dbo.Tours", "TourCollection_TourCollectionID", "dbo.TourCollections", "TourCollectionID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Tours", "TourCollection_TourCollectionID", "dbo.TourCollections");
            DropForeignKey("dbo.Tours", "LanguageId", "dbo.Languages");
            DropForeignKey("dbo.Articles", "LanguageId", "dbo.Languages");
            DropForeignKey("dbo.Articles", "ArticleCollection_ArticleCollectionID", "dbo.ArticleCollections");
            DropForeignKey("dbo.AspNetUsers", "CityId", "dbo.Categories");
            DropForeignKey("dbo.AspNetUsers", "CategoryId", "dbo.Categories");
            DropIndex("dbo.Tours", new[] { "TourCollection_TourCollectionID" });
            DropIndex("dbo.Tours", new[] { "LanguageId" });
            DropIndex("dbo.Articles", new[] { "ArticleCollection_ArticleCollectionID" });
            DropIndex("dbo.Articles", new[] { "LanguageId" });
            DropIndex("dbo.AspNetUsers", new[] { "CityId" });
            DropIndex("dbo.AspNetUsers", new[] { "CategoryId" });
            DropColumn("dbo.Tours", "TourCollection_TourCollectionID");
            DropColumn("dbo.Tours", "ParentTourId");
            DropColumn("dbo.Tours", "TourOwner");
            DropColumn("dbo.Tours", "LanguageId");
            DropColumn("dbo.Tours", "TourColId");
            DropColumn("dbo.Articles", "ArticleCollection_ArticleCollectionID");
            DropColumn("dbo.Articles", "ParentArticleId");
            DropColumn("dbo.Articles", "Status");
            DropColumn("dbo.Articles", "ArticleOwner");
            DropColumn("dbo.Articles", "ArticleBody");
            DropColumn("dbo.Articles", "LanguageId");
            DropColumn("dbo.Articles", "ArticleColId");
            DropColumn("dbo.AspNetUsers", "CityId");
            DropColumn("dbo.AspNetUsers", "CategoryId");
            DropColumn("dbo.AspNetUsers", "Surname");
            DropColumn("dbo.AspNetUsers", "Name");
        }
    }
}
