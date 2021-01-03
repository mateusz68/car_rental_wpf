namespace kck_projekt.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dodany_koszt : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Reservations", "Cost", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Reservations", "Cost");
        }
    }
}
