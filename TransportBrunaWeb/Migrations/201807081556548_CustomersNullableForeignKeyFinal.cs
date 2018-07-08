namespace TransportBrunaWeb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CustomersNullableForeignKeyFinal : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Customers", "PrivateCustomerID", "dbo.PrivateCustomer");
            DropIndex("dbo.Customers", new[] { "PrivateCustomerID" });
            AlterColumn("dbo.Customers", "PrivateCustomerID", c => c.Guid());
            CreateIndex("dbo.Customers", "PrivateCustomerID");
            AddForeignKey("dbo.Customers", "PrivateCustomerID", "dbo.PrivateCustomer", "PrivateCustomerID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Customers", "PrivateCustomerID", "dbo.PrivateCustomer");
            DropIndex("dbo.Customers", new[] { "PrivateCustomerID" });
            AlterColumn("dbo.Customers", "PrivateCustomerID", c => c.Guid(nullable: false));
            CreateIndex("dbo.Customers", "PrivateCustomerID");
            AddForeignKey("dbo.Customers", "PrivateCustomerID", "dbo.PrivateCustomer", "PrivateCustomerID", cascadeDelete: true);
        }
    }
}
