namespace TransportBrunaWeb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class galerija : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Galleries",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Title = c.String(),
                    WebImageId = c.Int(nullable: false),
                    IsActive = c.Boolean(nullable: false),
                    OrderNo = c.Int(),
                })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.WebFiles",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Data = c.Binary(),
                    IsActive = c.Boolean(nullable: false),
                    UpdateDate = c.DateTime(nullable: false),
                    FileName = c.String(),
                    FileExt = c.String(),
                    FileLength = c.Int(nullable: false),
                    ContentType = c.String(),
                })
                .PrimaryKey(t => t.Id);

        }
        
        public override void Down()
        {
            DropTable("dbo.WebFiles");
            DropTable("dbo.Galleries");
        }
    }
}
