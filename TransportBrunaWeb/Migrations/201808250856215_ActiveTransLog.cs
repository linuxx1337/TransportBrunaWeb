namespace TransportBrunaWeb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ActiveTransLog : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TransportationLog", "Active", c => c.Boolean(nullable: false, defaultValueSql:"0"));
        }
        
        public override void Down()
        {
            DropColumn("dbo.TransportationLog", "Active");
        }
    }
}
