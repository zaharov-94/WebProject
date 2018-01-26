namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class change : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Books",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Author = c.String(nullable: false),
                        YearOfPublishing = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.PublicationHouses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Address = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Brochures",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        TypeOfCover = c.String(nullable: false),
                        NumberOfPages = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Magazines",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Number = c.Int(nullable: false),
                        YearOfPublishing = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.PublicationHouseBooks",
                c => new
                    {
                        PublicationHouse_Id = c.Int(nullable: false),
                        Book_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.PublicationHouse_Id, t.Book_Id })
                .ForeignKey("dbo.PublicationHouses", t => t.PublicationHouse_Id, cascadeDelete: true)
                .ForeignKey("dbo.Books", t => t.Book_Id, cascadeDelete: true)
                .Index(t => t.PublicationHouse_Id)
                .Index(t => t.Book_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PublicationHouseBooks", "Book_Id", "dbo.Books");
            DropForeignKey("dbo.PublicationHouseBooks", "PublicationHouse_Id", "dbo.PublicationHouses");
            DropIndex("dbo.PublicationHouseBooks", new[] { "Book_Id" });
            DropIndex("dbo.PublicationHouseBooks", new[] { "PublicationHouse_Id" });
            DropTable("dbo.PublicationHouseBooks");
            DropTable("dbo.Magazines");
            DropTable("dbo.Brochures");
            DropTable("dbo.PublicationHouses");
            DropTable("dbo.Books");
        }
    }
}
