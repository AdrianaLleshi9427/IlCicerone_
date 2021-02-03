namespace IlCicerone.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class columnPhoneNumber : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Partecipants", "PhoneNumber", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Partecipants", "PhoneNumber");
        }
    }
}
