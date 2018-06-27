namespace TransportBrunaWeb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CostsDBset : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Costs",
                c => new
                    {
                        CostID = c.Guid(nullable: false),
                        CostTypeID = c.Guid(nullable: false),
                        Amount = c.Double(nullable: false),
                        Date = c.DateTime(nullable: false),
                        Note = c.String(),
                        Description = c.String(),
                        DateCreated = c.DateTime(),
                        DateModified = c.DateTime(),
                        CreatedBy = c.Guid(nullable: false),
                        ModifiedBy = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.CostID)
                .ForeignKey("dbo.CostTypes", t => t.CostTypeID, cascadeDelete: true)
                .Index(t => t.CostTypeID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Costs", "CostTypeID", "dbo.CostTypes");
            DropIndex("dbo.Costs", new[] { "CostTypeID" });
            DropTable("dbo.Costs");
        }
    }
}
