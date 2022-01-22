namespace ClothBazar.DataBase.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class added_configclass : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Configs",
                c => new
                    {
                        Key = c.String(nullable: false, maxLength: 128),
                        Value = c.String(),
                    })
                .PrimaryKey(t => t.Key);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Configs");
        }
    }
}
