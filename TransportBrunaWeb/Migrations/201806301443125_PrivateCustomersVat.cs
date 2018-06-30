namespace TransportBrunaWeb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PrivateCustomersVat : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.PrivateCustomer", "Vat", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.PrivateCustomer", "Vat", c => c.Int(nullable: false));
        }
    }
}
