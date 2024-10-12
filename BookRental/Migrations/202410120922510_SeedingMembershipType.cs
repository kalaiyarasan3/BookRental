namespace BookRental.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
	using System.Web.Mvc.Ajax;

	public partial class SeedingMembershipType : DbMigration
    {
        public override void Up()
        {
            Sql("insert into MembershipTypes values ('Pay Per Rental',0,50,25),('Member',150,20,10),('Super Admin',0,0,0)"
				);
        }
        
        public override void Down()
        {
        }
    }
}
