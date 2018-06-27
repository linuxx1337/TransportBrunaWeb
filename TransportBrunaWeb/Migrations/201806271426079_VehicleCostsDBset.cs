namespace TransportBrunaWeb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class VehicleCostsDBset : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.VehicleCosts",
                c => new
                    {
                        VehicleCostID = c.Guid(nullable: false),
                        VehicleID = c.Guid(nullable: false),
                        CostID = c.Guid(nullable: false),
                        Description = c.String(),
                        DateCreated = c.DateTime(),
                        DateModified = c.DateTime(),
                        CreatedBy = c.Guid(nullable: false),
                        ModifiedBy = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.VehicleCostID)
                .ForeignKey("dbo.Costs", t => t.CostID, cascadeDelete: true)
                .ForeignKey("dbo.Vehicles", t => t.VehicleID, cascadeDelete: false) // VPRAŠAJ TOLE??
                .Index(t => t.VehicleID)
                .Index(t => t.CostID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.VehicleCosts", "VehicleID", "dbo.Vehicles");
            DropForeignKey("dbo.VehicleCosts", "CostID", "dbo.Costs");
            DropIndex("dbo.VehicleCosts", new[] { "CostID" });
            DropIndex("dbo.VehicleCosts", new[] { "VehicleID" });
            DropTable("dbo.VehicleCosts");
        }
    }
}
