namespace BookRental.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddBookAndGenreClass : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Books",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ISBN = c.String(nullable: false),
                        Title = c.String(nullable: false),
                        Author = c.String(nullable: false),
                        Description = c.String(nullable: false),
                        ImageUrl = c.String(nullable: false),
                        Avaibility = c.Int(nullable: false),
                        Price = c.Double(nullable: false),
                        DateAdded = c.DateTime(nullable: false),
                        GenreId = c.Int(nullable: false),
                        Publisher = c.String(nullable: false),
                        PublicationDate = c.DateTime(nullable: false),
                        Pages = c.Int(nullable: false),
                        ProductDimensions = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Genres", t => t.GenreId, cascadeDelete: true)
                .Index(t => t.GenreId);
            
            CreateTable(
                "dbo.Genres",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Books", "GenreId", "dbo.Genres");
            DropIndex("dbo.Books", new[] { "GenreId" });
            DropTable("dbo.Genres");
            DropTable("dbo.Books");
        }
    }
}
