using SocialMedia.Data;
using SocialMedia.Models;
using SocialMedia.Models.Comment;
using SocialMedia.Models.Post;
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
                        .Where(post => post.AuthorId == _userId)
                        .Select(
                            post =>
                                new PostListItem
                                {
                                    PostId = post.PostId,
                                    Title = post.Title,
                                    Text = post.Text,
                                    TotalLikes = ctx.Likes.Where(like => like.PostId == post.PostId).Count(),
                                    IsLiked = (ctx.Likes.Where(like => like.PostId == post.PostId).Count() > 0) ? true : false,

                                    Comments = post.Comments.Select(c => new CommentListItem
                                    {
                                        CommentId = c.CommentId,
                                        Text = c.Text

                                    }).ToList()
                                }
                        ); ;

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

        public bool UpdatePost(PostEdit model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx.Posts
                    .Single(post => post.PostId == model.PostId && post.AuthorId == _userId);

                entity.Title = model.Title;
                entity.Text = model.Text;

                return ctx.SaveChanges() == 1;
            }
        }

        public bool DeletePost(int postId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx.Posts.Single(post => post.PostId == postId && post.AuthorId == _userId);

                ctx.Posts.Remove(entity);

                return ctx.SaveChanges() == 1;
            }
        }
    }
}
