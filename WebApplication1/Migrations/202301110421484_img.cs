namespace WebApplication1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class img : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Patients", "ImagePath", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Patients", "ImagePath");
        }
    }
}
