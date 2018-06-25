namespace TransportBrunaWeb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CargoTypes",
                c => new
                    {
                        CargoID = c.Guid(nullable: false),
                        Name = c.String(nullable: false),
                        Description = c.String(),
                        DateCreated = c.DateTime(),
                        DateModified = c.DateTime(),
                        CreatedBy = c.Guid(nullable: false),
                        ModifiedBy = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.CargoID);
            
            CreateTable(
                "dbo.Company",
                c => new
                    {
                        CompanyID = c.Guid(nullable: false),
                        FullName = c.String(nullable: false),
                        Address = c.String(nullable: false),
                        Phone = c.String(nullable: false),
                        Email = c.String(nullable: false),
                        Vat = c.Int(nullable: false),
                        Note = c.String(),
                        Description = c.String(),
                        DateCreated = c.DateTime(),
                        DateModified = c.DateTime(),
                        CreatedBy = c.Guid(nullable: false),
                        ModifiedBy = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.CompanyID);
            
            CreateTable(
                "dbo.Containers",
                c => new
                    {
                        ContainerID = c.Guid(nullable: false),
                        ContainerTypeID = c.Guid(nullable: false),
                        CompanyID = c.Guid(nullable: false),
                        Name = c.String(nullable: false),
                        Label = c.String(nullable: false),
                        Volume = c.Double(nullable: false),
                        Note = c.String(),
                        Description = c.String(),
                        DateCreated = c.DateTime(),
                        DateModified = c.DateTime(),
                        CreatedBy = c.Guid(nullable: false),
                        ModifiedBy = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.ContainerID)
                .ForeignKey("dbo.Company", t => t.CompanyID, cascadeDelete: true)
                .ForeignKey("dbo.ContainerTypes", t => t.ContainerTypeID, cascadeDelete: true)
                .Index(t => t.ContainerTypeID)
                .Index(t => t.CompanyID);
            
            CreateTable(
                "dbo.ContainerTypes",
                c => new
                    {
                        ContainerTypeID = c.Guid(nullable: false),
                        Name = c.String(nullable: false),
                        Description = c.String(),
                        DateCreated = c.DateTime(),
                        DateModified = c.DateTime(),
                        CreatedBy = c.Guid(nullable: false),
                        ModifiedBy = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.ContainerTypeID);
            
            CreateTable(
                "dbo.CostTypes",
                c => new
                    {
                        CostTypeID = c.Guid(nullable: false),
                        Name = c.String(nullable: false),
                        Description = c.String(),
                        DateCreated = c.DateTime(),
                        DateModified = c.DateTime(),
                        CreatedBy = c.Guid(nullable: false),
                        ModifiedBy = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.CostTypeID);
            
            CreateTable(
                "dbo.PrivateCustomer",
                c => new
                    {
                        PrivateCustomerID = c.Guid(nullable: false),
                        FullName = c.String(nullable: false),
                        Address = c.String(nullable: false),
                        Phone = c.String(nullable: false),
                        Email = c.String(),
                        Vat = c.Int(nullable: false),
                        Note = c.String(),
                        Description = c.String(),
                        DateCreated = c.DateTime(),
                        DateModified = c.DateTime(),
                        CreatedBy = c.Guid(nullable: false),
                        ModifiedBy = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.PrivateCustomerID);
            
            CreateTable(
                "dbo.TransportationStatusTypes",
                c => new
                    {
                        TransportationTypeStatusID = c.Guid(nullable: false),
                        Name = c.String(nullable: false),
                        Description = c.String(),
                        DateCreated = c.DateTime(),
                        DateModified = c.DateTime(),
                        CreatedBy = c.Guid(nullable: false),
                        ModifiedBy = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.TransportationTypeStatusID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Containers", "ContainerTypeID", "dbo.ContainerTypes");
            DropForeignKey("dbo.Containers", "CompanyID", "dbo.Company");
            DropIndex("dbo.Containers", new[] { "CompanyID" });
            DropIndex("dbo.Containers", new[] { "ContainerTypeID" });
            DropTable("dbo.TransportationStatusTypes");
            DropTable("dbo.PrivateCustomer");
            DropTable("dbo.CostTypes");
            DropTable("dbo.ContainerTypes");
            DropTable("dbo.Containers");
            DropTable("dbo.Company");
            DropTable("dbo.CargoTypes");
        }
    }
}
