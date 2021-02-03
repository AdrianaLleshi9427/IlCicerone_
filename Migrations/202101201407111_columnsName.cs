namespace IlCicerone.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class columnsName : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Partecipants", "Name", c => c.String());
            AddColumn("dbo.Partecipants", "Surname", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Partecipants", "Surname");
            DropColumn("dbo.Partecipants", "Name");
        }
    }
}
