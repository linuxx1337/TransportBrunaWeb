namespace TransportBrunaWeb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CustomersNullableForeignKey1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Customers", "CompanyID", "dbo.Company");
            DropIndex("dbo.Customers", new[] { "CompanyID" });
            AlterColumn("dbo.Customers", "CompanyID", c => c.Guid());
            CreateIndex("dbo.Customers", "CompanyID");
            AddForeignKey("dbo.Customers", "CompanyID", "dbo.Company", "CompanyID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Customers", "CompanyID", "dbo.Company");
            DropIndex("dbo.Customers", new[] { "CompanyID" });
            AlterColumn("dbo.Customers", "CompanyID", c => c.Guid(nullable: false));
            CreateIndex("dbo.Customers", "CompanyID");
            AddForeignKey("dbo.Customers", "CompanyID", "dbo.Company", "CompanyID", cascadeDelete: true);
        }
    }
}
