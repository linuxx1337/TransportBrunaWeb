namespace TransportBrunaWeb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DrivingCostsDBset : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DrivingCosts",
                c => new
                    {
                        DrivingCostID = c.Guid(nullable: false),
                        CostID = c.Guid(nullable: false),
                        TransportationLogID = c.Guid(nullable: false),
                        Description = c.String(),
                        DateCreated = c.DateTime(),
                        DateModified = c.DateTime(),
                        CreatedBy = c.Guid(nullable: false),
                        ModifiedBy = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.DrivingCostID)
                .ForeignKey("dbo.Costs", t => t.CostID, cascadeDelete: true)
                .ForeignKey("dbo.TransportationLog", t => t.TransportationLogID, cascadeDelete: false)
                .Index(t => t.CostID)
                .Index(t => t.TransportationLogID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.DrivingCosts", "TransportationLogID", "dbo.TransportationLog");
            DropForeignKey("dbo.DrivingCosts", "CostID", "dbo.Costs");
            DropIndex("dbo.DrivingCosts", new[] { "TransportationLogID" });
            DropIndex("dbo.DrivingCosts", new[] { "CostID" });
            DropTable("dbo.DrivingCosts");
        }
    }
}
