namespace TransportBrunaWeb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CustomersDBset : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        CustomerID = c.Guid(nullable: false),
                        CompanyID = c.Guid(nullable: false),
                        PrivateCustomerID = c.Guid(nullable: false),
                        Description = c.String(),
                        DateCreated = c.DateTime(),
                        DateModified = c.DateTime(),
                        CreatedBy = c.Guid(nullable: false),
                        ModifiedBy = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.CustomerID)
                .ForeignKey("dbo.Company", t => t.CompanyID, cascadeDelete: true)
                .ForeignKey("dbo.PrivateCustomer", t => t.PrivateCustomerID, cascadeDelete: true)
                .Index(t => t.CompanyID)
                .Index(t => t.PrivateCustomerID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Customers", "PrivateCustomerID", "dbo.PrivateCustomer");
            DropForeignKey("dbo.Customers", "CompanyID", "dbo.Company");
            DropIndex("dbo.Customers", new[] { "PrivateCustomerID" });
            DropIndex("dbo.Customers", new[] { "CompanyID" });
            DropTable("dbo.Customers");
        }
    }
}
