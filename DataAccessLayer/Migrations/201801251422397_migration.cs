namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migration : DbMigration
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
                        PublicationHouseId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.PublicationHouses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Address = c.String(nullable: false),
                        BookId = c.Int(nullable: false),
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
                "dbo.BookPublicationHouses",
                c => new
                    {
                        Book_Id = c.Int(nullable: false),
                        PublicationHouse_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Book_Id, t.PublicationHouse_Id })
                .ForeignKey("dbo.Books", t => t.Book_Id, cascadeDelete: true)
                .ForeignKey("dbo.PublicationHouses", t => t.PublicationHouse_Id, cascadeDelete: true)
                .Index(t => t.Book_Id)
                .Index(t => t.PublicationHouse_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.BookPublicationHouses", "PublicationHouse_Id", "dbo.PublicationHouses");
            DropForeignKey("dbo.BookPublicationHouses", "Book_Id", "dbo.Books");
            DropIndex("dbo.BookPublicationHouses", new[] { "PublicationHouse_Id" });
            DropIndex("dbo.BookPublicationHouses", new[] { "Book_Id" });
            DropTable("dbo.BookPublicationHouses");
            DropTable("dbo.Magazines");
            DropTable("dbo.Brochures");
            DropTable("dbo.PublicationHouses");
            DropTable("dbo.Books");
        }
    }
}
