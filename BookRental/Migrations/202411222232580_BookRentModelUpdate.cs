namespace BookRental.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class BookRentModelUpdate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.BookRents", "AdditionalCharge", c => c.Double());
        }
        
        public override void Down()
        {
            DropColumn("dbo.BookRents", "AdditionalCharge");
        }
    }
}
