namespace IlCicerone.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Partecipants : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Partecipants",
                c => new
                    {
                        PartecipantId = c.Int(nullable: false, identity: true),
                        UserId = c.String(maxLength: 128),
                        TourId = c.Int(nullable: false),
                        Confirmed = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.PartecipantId)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .ForeignKey("dbo.Tours", t => t.TourId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.TourId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Partecipants", "TourId", "dbo.Tours");
            DropForeignKey("dbo.Partecipants", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.Partecipants", new[] { "TourId" });
            DropIndex("dbo.Partecipants", new[] { "UserId" });
            DropTable("dbo.Partecipants");
        }
    }
}
