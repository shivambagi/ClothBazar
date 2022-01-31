namespace ClothBazar.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class added_NAmeAddress_to_IdentityModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "Address", c => c.String());
            DropColumn("dbo.AspNetUsers", "Adddress");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "Adddress", c => c.String());
            DropColumn("dbo.AspNetUsers", "Address");
        }
    }
}
