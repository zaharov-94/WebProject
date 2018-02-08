namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class userUpd : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.ClientProfiles", "Name");
            DropColumn("dbo.ClientProfiles", "Address");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ClientProfiles", "Address", c => c.String());
            AddColumn("dbo.ClientProfiles", "Name", c => c.String());
        }
    }
}
