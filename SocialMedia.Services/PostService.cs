using SocialMedia.Data;
using SocialMedia.Models;
using SocialMedia.Models.Comment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.Services
{
    public class PostService
    {
        private readonly Guid _userId;

        public PostService(Guid userId)
        {
            _userId = userId;
        }

        public bool CreatePost(PostCreate model)
        {
            var entity =
                new Post()
                {
                    AuthorId = _userId,
                    Title = model.Title,
                    Text = model.Text
                };

            using (var ctx = new ApplicationDbContext())
            {
                ctx.Posts.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }

        public IEnumerable<PostListItem> GetPosts()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                        .Posts
                        .Where(e => e.AuthorId == _userId)
                        .Select(
                            e =>
                                new PostListItem
                                {
                                    PostId = e.PostId,
                                    Title = e.Title,
                                    Comments = e.Comments.Select(c => new CommentListItem
                                    {
                                        CommentId = c.CommentId,
                                        Text = c.Text

                                    }).ToList()
                                }
                        );

                return query.ToArray();
            }
        }

        //Get Post Comments
        public IEnumerable<CommentListItem> GetPostComments(int postId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                        .Comments
                        .Where(e => e.AuthorId == _userId && e.PostId == postId)
                        .Select(
                            e =>
                                new CommentListItem
                                {
                                    CommentId = e.CommentId,
                                    Text = e.Text
                                }
                        );

                return query.ToArray();
            }
        }
    }
}
