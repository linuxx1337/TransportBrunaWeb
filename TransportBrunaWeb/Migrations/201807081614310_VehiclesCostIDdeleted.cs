namespace TransportBrunaWeb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class VehiclesCostIDdeleted : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Vehicles", "CostID", "dbo.Costs");
            DropIndex("dbo.Vehicles", new[] { "CostID" });
            DropColumn("dbo.Vehicles", "CostID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Vehicles", "CostID", c => c.Guid(nullable: false));
            CreateIndex("dbo.Vehicles", "CostID");
            AddForeignKey("dbo.Vehicles", "CostID", "dbo.Costs", "CostID", cascadeDelete: true);
        }
    }
}
