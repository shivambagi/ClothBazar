namespace ClothBazar.DataBase.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class BaseInsurance_Gender_Frequency_INsuranceProducts_ULIPS : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Frequencies",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        FrequencyName = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Genders",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        GenderName = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.InsuranceProducts",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ProductType = c.String(),
                        ProductName = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.ULIPs",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Goal = c.String(),
                        PPT = c.Int(nullable: false),
                        Term = c.Int(nullable: false),
                        SumAssured = c.Double(nullable: false),
                        ModalPremium = c.Double(nullable: false),
                        AnnualPremium = c.Double(nullable: false),
                        Name = c.String(),
                        DateOfBirth = c.DateTime(nullable: false),
                        Smoker = c.Boolean(nullable: false),
                        Frequency_ID = c.Int(nullable: false),
                        Gender_ID = c.Int(),
                        InsuranceProduct_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Frequencies", t => t.Frequency_ID, cascadeDelete: true)
                .ForeignKey("dbo.Genders", t => t.Gender_ID)
                .ForeignKey("dbo.InsuranceProducts", t => t.InsuranceProduct_ID)
                .Index(t => t.Frequency_ID)
                .Index(t => t.Gender_ID)
                .Index(t => t.InsuranceProduct_ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ULIPs", "InsuranceProduct_ID", "dbo.InsuranceProducts");
            DropForeignKey("dbo.ULIPs", "Gender_ID", "dbo.Genders");
            DropForeignKey("dbo.ULIPs", "Frequency_ID", "dbo.Frequencies");
            DropIndex("dbo.ULIPs", new[] { "InsuranceProduct_ID" });
            DropIndex("dbo.ULIPs", new[] { "Gender_ID" });
            DropIndex("dbo.ULIPs", new[] { "Frequency_ID" });
            DropTable("dbo.ULIPs");
            DropTable("dbo.InsuranceProducts");
            DropTable("dbo.Genders");
            DropTable("dbo.Frequencies");
        }
    }
}
