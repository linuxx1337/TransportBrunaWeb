namespace TransportBrunaWeb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TransportationLogDBset : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TransportationLog",
                c => new
                    {
                        TransportationLogID = c.Guid(nullable: false),
                        ContainerID = c.Guid(nullable: false),
                        VehicleID = c.Guid(nullable: false),
                        CargoID = c.Guid(nullable: false),
                        CustomerID = c.Guid(nullable: false),
                        CostID = c.Guid(nullable: false),
                        Date = c.DateTime(nullable: false),
                        Location = c.String(nullable: false),
                        Note = c.String(),
                        Description = c.String(),
                        DateCreated = c.DateTime(),
                        DateModified = c.DateTime(),
                        CreatedBy = c.Guid(nullable: false),
                        ModifiedBy = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.TransportationLogID)
                .ForeignKey("dbo.CargoTypes", t => t.CargoID, cascadeDelete: true)
                .ForeignKey("dbo.Containers", t => t.ContainerID, cascadeDelete: true)
                .ForeignKey("dbo.Costs", t => t.CostID, cascadeDelete: true)
                .ForeignKey("dbo.Customers", t => t.CustomerID, cascadeDelete: false) // zakaj mora bit false?
                .ForeignKey("dbo.Vehicles", t => t.VehicleID, cascadeDelete: false) // enako kot zgoraj
                .Index(t => t.ContainerID)
                .Index(t => t.VehicleID)
                .Index(t => t.CargoID)
                .Index(t => t.CustomerID)
                .Index(t => t.CostID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TransportationLog", "VehicleID", "dbo.Vehicles");
            DropForeignKey("dbo.TransportationLog", "CustomerID", "dbo.Customers");
            DropForeignKey("dbo.TransportationLog", "CostID", "dbo.Costs");
            DropForeignKey("dbo.TransportationLog", "ContainerID", "dbo.Containers");
            DropForeignKey("dbo.TransportationLog", "CargoID", "dbo.CargoTypes");
            DropIndex("dbo.TransportationLog", new[] { "CostID" });
            DropIndex("dbo.TransportationLog", new[] { "CustomerID" });
            DropIndex("dbo.TransportationLog", new[] { "CargoID" });
            DropIndex("dbo.TransportationLog", new[] { "VehicleID" });
            DropIndex("dbo.TransportationLog", new[] { "ContainerID" });
            DropTable("dbo.TransportationLog");
        }
    }
}
