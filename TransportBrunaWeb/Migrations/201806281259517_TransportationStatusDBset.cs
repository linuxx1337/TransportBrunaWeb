namespace TransportBrunaWeb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TransportationStatusDBset : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TransportationStatus",
                c => new
                    {
                        TransportationStatusID = c.Guid(nullable: false),
                        TransportationTypeStatusID = c.Guid(nullable: false),
                        TransportationLogID = c.Guid(nullable: false),
                        Date = c.DateTime(nullable: false),
                        Note = c.String(),
                        Description = c.String(),
                        DateCreated = c.DateTime(),
                        DateModified = c.DateTime(),
                        CreatedBy = c.Guid(nullable: false),
                        ModifiedBy = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.TransportationStatusID)
                .ForeignKey("dbo.TransportationLog", t => t.TransportationLogID, cascadeDelete: true)
                .ForeignKey("dbo.TransportationStatusTypes", t => t.TransportationTypeStatusID, cascadeDelete: true)
                .Index(t => t.TransportationTypeStatusID)
                .Index(t => t.TransportationLogID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TransportationStatus", "TransportationTypeStatusID", "dbo.TransportationStatusTypes");
            DropForeignKey("dbo.TransportationStatus", "TransportationLogID", "dbo.TransportationLog");
            DropIndex("dbo.TransportationStatus", new[] { "TransportationLogID" });
            DropIndex("dbo.TransportationStatus", new[] { "TransportationTypeStatusID" });
            DropTable("dbo.TransportationStatus");
        }
    }
}
