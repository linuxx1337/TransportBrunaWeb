namespace TransportBrunaWeb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class HouseholdTransportationDBset : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.HouseholdTransportation",
                c => new
                    {
                        HouseholdTransportationID = c.Guid(nullable: false),
                        TransportationLogID = c.Guid(nullable: false),
                        FirstName = c.String(nullable: false),
                        LastName = c.String(nullable: false),
                        Address = c.String(nullable: false),
                        PostCode = c.String(nullable: false),
                        City = c.String(nullable: false),
                        Date = c.DateTime(nullable: false),
                        Note = c.String(),
                        Attachment = c.String(),
                        Description = c.String(),
                        DateCreated = c.DateTime(),
                        DateModified = c.DateTime(),
                        CreatedBy = c.Guid(nullable: false),
                        ModifiedBy = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.HouseholdTransportationID)
                .ForeignKey("dbo.TransportationLog", t => t.TransportationLogID, cascadeDelete: true)
                .Index(t => t.TransportationLogID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.HouseholdTransportation", "TransportationLogID", "dbo.TransportationLog");
            DropIndex("dbo.HouseholdTransportation", new[] { "TransportationLogID" });
            DropTable("dbo.HouseholdTransportation");
        }
    }
}
