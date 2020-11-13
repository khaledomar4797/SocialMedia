using SocialMedia.Data;
using SocialMedia.Models.Comment;
using SocialMedia.Models.Reply;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.Services
{
    public class CommentService
    {
        private readonly Guid _userId;

        public CommentService(Guid userId)
        {
            _userId = userId;
        }

        public bool CreateComment(CommentCreate model)
        {
            var entity =
                new Comment()
                {
                    AuthorId = _userId,
                    Text = model.Text,
                    PostId = model.PostId
                };

            using (var ctx = new ApplicationDbContext())
            {
                ctx.Comments.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }

        public IEnumerable<CommentListItem> GetComments()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                        .Comments
                        .Where(e => e.AuthorId == _userId)
                        .Select(
                            e =>
                                new CommentListItem
                                {
                                    CommentId = e.CommentId,
                                    Text = e.Text,
                                    Replies = e.Replies.Select(r => new ReplyListItem 
                                    { 
                                        ReplyId = r.ReplyId,
                                        Text = r.Text
                                    }).ToList()
                                }
                        );

                return query.ToArray();
            }
        }

        //Get Comment Replies
        public IEnumerable<ReplyListItem> GetCommentReplies(int commentId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                        .Replies
                        .Where(e => e.AuthorId == _userId && e.CommentId == commentId)
                        .Select(
                            e =>
                                new ReplyListItem
                                {
                                    ReplyId = e.ReplyId,
                                    Text = e.Text
                                }
                        );

                return query.ToArray();
            }
        }

    }
}
