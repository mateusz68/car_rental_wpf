namespace kck_projekt.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class pola_uzytkownika : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "Name", c => c.String(maxLength: 50, storeType: "nvarchar"));
            AddColumn("dbo.Users", "Surname", c => c.String(maxLength: 50, storeType: "nvarchar"));
            AddColumn("dbo.Users", "Adres1", c => c.String(maxLength: 50, storeType: "nvarchar"));
            AddColumn("dbo.Users", "Adres2", c => c.String(maxLength: 50, storeType: "nvarchar"));
            AddColumn("dbo.Users", "Adres3", c => c.String(maxLength: 50, storeType: "nvarchar"));
            AddColumn("dbo.Users", "Phone", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "Phone");
            DropColumn("dbo.Users", "Adres3");
            DropColumn("dbo.Users", "Adres2");
            DropColumn("dbo.Users", "Adres1");
            DropColumn("dbo.Users", "Surname");
            DropColumn("dbo.Users", "Name");
        }
    }
}
