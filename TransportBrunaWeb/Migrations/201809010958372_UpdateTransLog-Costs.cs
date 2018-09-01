namespace TransportBrunaWeb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateTransLogCosts : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.TransportationLog", "CostID", "dbo.Costs");
            DropIndex("dbo.TransportationLog", new[] { "CostID" });
            DropColumn("dbo.TransportationLog", "CostID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.TransportationLog", "CostID", c => c.Guid());
            CreateIndex("dbo.TransportationLog", "CostID");
            AddForeignKey("dbo.TransportationLog", "CostID", "dbo.Costs", "CostID");
        }
    }
}
