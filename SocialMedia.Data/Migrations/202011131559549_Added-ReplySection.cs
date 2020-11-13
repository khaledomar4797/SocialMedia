namespace SocialMedia.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedReplySection : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Reply",
                c => new
                    {
                        ReplyId = c.Int(nullable: false, identity: true),
                        Text = c.String(nullable: false),
                        AuthorId = c.Guid(nullable: false),
                        CommentId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ReplyId)
                .ForeignKey("dbo.Comment", t => t.CommentId, cascadeDelete: true)
                .Index(t => t.CommentId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Reply", "CommentId", "dbo.Comment");
            DropIndex("dbo.Reply", new[] { "CommentId" });
            DropTable("dbo.Reply");
        }
    }
}
