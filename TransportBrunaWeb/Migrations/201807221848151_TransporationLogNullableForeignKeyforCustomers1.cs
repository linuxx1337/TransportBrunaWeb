namespace TransportBrunaWeb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TransporationLogNullableForeignKeyforCustomers1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.TransportationLog", "CostID", "dbo.Costs");
            DropIndex("dbo.TransportationLog", new[] { "CostID" });
            AlterColumn("dbo.TransportationLog", "CostID", c => c.Guid());
            CreateIndex("dbo.TransportationLog", "CostID");
            AddForeignKey("dbo.TransportationLog", "CostID", "dbo.Costs", "CostID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TransportationLog", "CostID", "dbo.Costs");
            DropIndex("dbo.TransportationLog", new[] { "CostID" });
            AlterColumn("dbo.TransportationLog", "CostID", c => c.Guid(nullable: false));
            CreateIndex("dbo.TransportationLog", "CostID");
            AddForeignKey("dbo.TransportationLog", "CostID", "dbo.Costs", "CostID", cascadeDelete: true);
        }
    }
}
