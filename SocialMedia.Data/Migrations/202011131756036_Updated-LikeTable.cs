namespace SocialMedia.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatedLikeTable : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Like", name: "LikePost_PostId", newName: "PostId");
            RenameIndex(table: "dbo.Like", name: "IX_LikePost_PostId", newName: "IX_PostId");
            DropPrimaryKey("dbo.Like");
            AddColumn("dbo.Like", "LikeId", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.Like", "LikeId");
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.Like");
            DropColumn("dbo.Like", "LikeId");
            AddPrimaryKey("dbo.Like", "Liker");
            RenameIndex(table: "dbo.Like", name: "IX_PostId", newName: "IX_LikePost_PostId");
            RenameColumn(table: "dbo.Like", name: "PostId", newName: "LikePost_PostId");
        }
    }
}
