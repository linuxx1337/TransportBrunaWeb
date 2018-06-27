namespace TransportBrunaWeb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class VehiclesDBset : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Vehicles",
                c => new
                    {
                        VehicleID = c.Guid(nullable: false),
                        CompanyID = c.Guid(nullable: false),
                        CostID = c.Guid(nullable: false),
                        Name = c.String(nullable: false),
                        RegPlate = c.String(nullable: false),
                        Brand = c.String(nullable: false),
                        Vin = c.String(nullable: false),
                        Gvw = c.Int(nullable: false),
                        MassCargo = c.Int(nullable: false),
                        Type = c.String(nullable: false),
                        DateReg = c.DateTime(nullable: false),
                        DateMot = c.DateTime(nullable: false),
                        Note = c.String(),
                        Description = c.String(),
                        DateCreated = c.DateTime(),
                        DateModified = c.DateTime(),
                        CreatedBy = c.Guid(nullable: false),
                        ModifiedBy = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.VehicleID)
                .ForeignKey("dbo.Company", t => t.CompanyID, cascadeDelete: true)
                .ForeignKey("dbo.Costs", t => t.CostID, cascadeDelete: true)
                .Index(t => t.CompanyID)
                .Index(t => t.CostID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Vehicles", "CostID", "dbo.Costs");
            DropForeignKey("dbo.Vehicles", "CompanyID", "dbo.Company");
            DropIndex("dbo.Vehicles", new[] { "CostID" });
            DropIndex("dbo.Vehicles", new[] { "CompanyID" });
            DropTable("dbo.Vehicles");
        }
    }
}
