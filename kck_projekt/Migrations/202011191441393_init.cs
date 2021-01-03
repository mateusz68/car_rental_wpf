namespace kck_projekt.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Cars",
                c => new
                    {
                        CarId = c.Int(nullable: false, identity: true),
                        CarRegNumb = c.String(maxLength: 50, storeType: "nvarchar"),
                        CarColor = c.String(maxLength: 50, storeType: "nvarchar"),
                        CarPower = c.Int(nullable: false),
                        CarDayPrince = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CarBail = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CarStatus = c.Int(nullable: false),
                        CarEngineType = c.Int(nullable: false),
                        CarGearboxType = c.Int(nullable: false),
                        Model_ModelId = c.Int(),
                    })
                .PrimaryKey(t => t.CarId)
                .ForeignKey("dbo.CarModels", t => t.Model_ModelId)
                .Index(t => t.Model_ModelId);
            
            CreateTable(
                "dbo.CarModels",
                c => new
                    {
                        ModelId = c.Int(nullable: false, identity: true),
                        ModelName = c.String(maxLength: 50, storeType: "nvarchar"),
                        Mark_MarkId = c.Int(),
                    })
                .PrimaryKey(t => t.ModelId)
                .ForeignKey("dbo.CarMarks", t => t.Mark_MarkId)
                .Index(t => t.Mark_MarkId);
            
            CreateTable(
                "dbo.CarMarks",
                c => new
                    {
                        MarkId = c.Int(nullable: false, identity: true),
                        MarkName = c.String(maxLength: 50, storeType: "nvarchar"),
                    })
                .PrimaryKey(t => t.MarkId);
            
            CreateTable(
                "dbo.Reservations",
                c => new
                    {
                        ReservationId = c.Int(nullable: false, identity: true),
                        DateFrom = c.DateTime(nullable: false, precision: 0),
                        DateTo = c.DateTime(nullable: false, precision: 0),
                        Comments = c.String(maxLength: 200, storeType: "nvarchar"),
                        Status = c.Int(nullable: false),
                        Car_CarId = c.Int(),
                        User_UserId = c.Int(),
                    })
                .PrimaryKey(t => t.ReservationId)
                .ForeignKey("dbo.Cars", t => t.Car_CarId)
                .ForeignKey("dbo.Users", t => t.User_UserId)
                .Index(t => t.Car_CarId)
                .Index(t => t.User_UserId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        UserName = c.String(nullable: false, maxLength: 50, storeType: "nvarchar"),
                        UserPassword = c.String(nullable: false, maxLength: 200, storeType: "nvarchar"),
                        Salt = c.String(nullable: false, maxLength: 10, storeType: "nvarchar"),
                        Email = c.String(maxLength: 50, storeType: "nvarchar"),
                        Rola = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Reservations", "User_UserId", "dbo.Users");
            DropForeignKey("dbo.Reservations", "Car_CarId", "dbo.Cars");
            DropForeignKey("dbo.Cars", "Model_ModelId", "dbo.CarModels");
            DropForeignKey("dbo.CarModels", "Mark_MarkId", "dbo.CarMarks");
            DropIndex("dbo.Reservations", new[] { "User_UserId" });
            DropIndex("dbo.Reservations", new[] { "Car_CarId" });
            DropIndex("dbo.CarModels", new[] { "Mark_MarkId" });
            DropIndex("dbo.Cars", new[] { "Model_ModelId" });
            DropTable("dbo.Users");
            DropTable("dbo.Reservations");
            DropTable("dbo.CarMarks");
            DropTable("dbo.CarModels");
            DropTable("dbo.Cars");
        }
    }
}
