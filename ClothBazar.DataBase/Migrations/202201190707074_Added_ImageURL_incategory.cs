namespace ClothBazar.DataBase.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Added_ImageURL_incategory : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Categories", "ImageURL", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Categories", "ImageURL");
        }
    }
}
