namespace SocialMedia.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedLikeSection : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Like",
                c => new
                    {
                        Liker = c.Guid(nullable: false),
                        LikePost_PostId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Liker)
                .ForeignKey("dbo.Post", t => t.LikePost_PostId, cascadeDelete: true)
                .Index(t => t.LikePost_PostId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Like", "LikePost_PostId", "dbo.Post");
            DropIndex("dbo.Like", new[] { "LikePost_PostId" });
            DropTable("dbo.Like");
        }
    }
}
