namespace TransportBrunaWeb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ContainersVolumeFix : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Containers", "Volume", c => c.Double());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Containers", "Volume", c => c.Double(nullable: false));
        }
    }
}
