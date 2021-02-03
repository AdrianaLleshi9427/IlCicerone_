namespace IlCicerone.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class columnAddress : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Partecipants", "Address", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Partecipants", "Address");
        }
    }
}
