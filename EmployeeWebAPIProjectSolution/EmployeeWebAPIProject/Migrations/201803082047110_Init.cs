namespace EmployeeWebAPIProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Init : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Departments", "employee_Id", "dbo.Employees");
            DropIndex("dbo.Departments", new[] { "employee_Id" });
            RenameColumn(table: "dbo.Departments", name: "employee_Id", newName: "EmployeeId");
            AlterColumn("dbo.Departments", "EmployeeId", c => c.Int(nullable: false));
            CreateIndex("dbo.Departments", "EmployeeId");
            AddForeignKey("dbo.Departments", "EmployeeId", "dbo.Employees", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Departments", "EmployeeId", "dbo.Employees");
            DropIndex("dbo.Departments", new[] { "EmployeeId" });
            AlterColumn("dbo.Departments", "EmployeeId", c => c.Int());
            RenameColumn(table: "dbo.Departments", name: "EmployeeId", newName: "employee_Id");
            CreateIndex("dbo.Departments", "employee_Id");
            AddForeignKey("dbo.Departments", "employee_Id", "dbo.Employees", "Id");
        }
    }
}
