namespace IlCicerone.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update30 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.AspNetUsers", "Birthday");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "Birthday", c => c.DateTime(nullable: false));
        }
    }
}
