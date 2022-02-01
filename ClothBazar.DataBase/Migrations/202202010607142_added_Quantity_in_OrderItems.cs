namespace ClothBazar.DataBase.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class added_Quantity_in_OrderItems : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.OrderItems", "Order_ID", "dbo.Orders");
            DropIndex("dbo.OrderItems", new[] { "Order_ID" });
            RenameColumn(table: "dbo.OrderItems", name: "Order_ID", newName: "OrderID");
            AddColumn("dbo.OrderItems", "Quantity", c => c.Int(nullable: false));
            AlterColumn("dbo.OrderItems", "OrderID", c => c.Int(nullable: false));
            CreateIndex("dbo.OrderItems", "OrderID");
            AddForeignKey("dbo.OrderItems", "OrderID", "dbo.Orders", "ID", cascadeDelete: true);
            DropColumn("dbo.OrderItems", "UserID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.OrderItems", "UserID", c => c.Int(nullable: false));
            DropForeignKey("dbo.OrderItems", "OrderID", "dbo.Orders");
            DropIndex("dbo.OrderItems", new[] { "OrderID" });
            AlterColumn("dbo.OrderItems", "OrderID", c => c.Int());
            DropColumn("dbo.OrderItems", "Quantity");
            RenameColumn(table: "dbo.OrderItems", name: "OrderID", newName: "Order_ID");
            CreateIndex("dbo.OrderItems", "Order_ID");
            AddForeignKey("dbo.OrderItems", "Order_ID", "dbo.Orders", "ID");
        }
    }
}
