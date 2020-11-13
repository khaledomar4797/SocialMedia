namespace SocialMedia.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedLikesCount : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Post", "IsLiked", c => c.Boolean(nullable: false));
            AddColumn("dbo.Post", "TotalLikes", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Post", "TotalLikes");
            DropColumn("dbo.Post", "IsLiked");
        }
    }
}
