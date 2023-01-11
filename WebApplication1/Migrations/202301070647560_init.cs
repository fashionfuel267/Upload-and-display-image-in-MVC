namespace WebApplication1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Patients",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Password = c.String(),
                        About = c.String(),
                        hobby = c.String(),
                        gender = c.String(),
                        country = c.String(),
                        SCountry = c.String(),
                        Bdate = c.DateTime(nullable: false),
                        isActive = c.Boolean(nullable: false),
                        profilePath = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Patients");
        }
    }
}
